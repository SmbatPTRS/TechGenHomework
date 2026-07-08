# PagedQuery

A generic, reusable pattern for **filtering, sorting, and paginating** any collection in C# — written once, works for any type (`Employee`, `Product`, or anything else).

This README is written so that if I forget how this works six months from now, I can read it top to bottom and fully understand it again — not just "what the code does" but "why it's built this way."

---

## 1. The Problem This Solves

Imagine a screen that can only display 3 items at a time. If the backend has 10,000 employees, we don't want to send all 10,000 every time — too slow, too wasteful.

Instead, the frontend asks for something like:

> "Give me page 1, 2 items per page, only employees in **HR**, sorted by **salary**, highest first."

The backend needs to do three jobs, in order:

1. **Filter** — keep only the items that match a rule (e.g., `Department == "HR"`)
2. **Sort** — order them by some property (e.g., `Salary`, descending)
3. **Paginate** — cut out just the slice of items for the requested page

This project does exactly that — generically, so it works for any type of item, not just `Employee`.

---

## 2. Project Structure

```
PagedQuery/
├── PagedResult.cs           → the "answer" object returned to the caller
├── QueryOptions.cs          → the "request" object (filter, page, pageSize)
├── QueryOptions{T,TKey}.cs  → extends the request object, adds sorting
├── QueryExecutor.cs         → the engine: filter → sort → paginate
├── Employee.cs               → example data type used in the demo
└── Program.cs                 → runnable demo showing it all in action
```

Think of it as: **request in → engine processes → answer out.**

```
QueryOptions<T>  ──┐
                    ├──▶  QueryExecutor.Execute(...)  ──▶  PagedResult<T>
IEnumerable<T>   ──┘
   (raw data)
```

---

## 3. What Is `<T>`? (Generics, quickly)

`T` is a placeholder for "a type decided later." Writing `PagedResult<T>` once means it works for `PagedResult<Employee>`, `PagedResult<Product>`, anything — without duplicating the class for every type.

`TKey` (seen in `QueryOptions<T, TKey>`) is a **second, separate placeholder** — it represents the type of the *property we sort by* (e.g., `decimal` for `Salary`, `string` for `Name`). It's different from `T` because the item type and the sort-value type aren't always the same thing.

---

## 4. `QueryOptions<T>` — describing the request (no sorting)

```csharp
public class QueryOptions<T>
{
    // The rule that decides which items to keep.
    // Func<T, bool> = "a function that takes one T, returns true/false."
    public Func<T, bool>? FilterRule { get; init; }

    // Reverse the final order? (only meaningful when sorting is also used)
    public bool Descending { get; init; }

    // Which page to return. Defaults to 1.
    public int Page { get; init; } = 1;

    // How many items per page. Defaults to 10.
    public int PageSize { get; init; } = 10;
}
```

- **`Func<T, bool>? FilterRule`** — a delegate (a variable that holds a function). We supply the actual rule later, as a lambda:
  ```csharp
  FilterRule = e => e.Department == "HR"
  ```
  `e` is one item going in, `e.Department == "HR"` is the true/false answer coming out. It's nullable (`?`) because sometimes we don't want to filter at all — `null` cleanly means "keep everything."

- **`init`** — can only be set at creation time (inside the `{ }` initializer), then it's locked. Prevents the request object from being silently changed later.

---

## 5. `QueryOptions<T, TKey>` — adding sorting on top

```csharp
public sealed class QueryOptions<T, TKey> : QueryOptions<T>
    where TKey : IComparable<TKey>
{
    // Extracts the value to sort by, from one item.
    // Func<T, TKey> = "takes a T, returns a TKey."
    public Func<T, TKey>? KeyReturner { get; init; }
}
```

- **`: QueryOptions<T>`** — inherits `FilterRule`, `Descending`, `Page`, `PageSize` for free, and adds `KeyReturner` on top.

- **Why a separate class instead of putting `KeyReturner` in the base class?** Because it needs a *second* generic type (`TKey`), which the base `QueryOptions<T>` doesn't have. If we don't need sorting, we just use `QueryOptions<T>` — simple, no unnecessary generic parameter to think about. If we DO need sorting, we use `QueryOptions<T, TKey>` instead.

- **`where TKey : IComparable<TKey>`** — a *generic constraint*. It means: "whatever type `TKey` ends up being, it must know how to compare itself to another instance of the same type." This is required because later we call `.CompareTo()` on it — a method that only exists on comparable types (`int`, `decimal`, `string`, `DateTime`, etc. all qualify automatically).

- **`KeyReturner`** — supplied as a lambda too:
  ```csharp
  KeyReturner = e => e.Salary
  ```
  Given one employee, it just returns their salary. It does NOT sort anything itself — it only extracts a value to compare by. Sorting a whole list is a separate job, done by `QueryExecutor.Sort`.

---

## 6. `PagedResult<T>` — the response object

```csharp
public sealed class PagedResult<T>
{
    public PagedResult(IReadOnlyList<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public IReadOnlyList<T> Items { get; }   // the actual data for this page
    public int Page { get; }                  // which page this is
    public int PageSize { get; }               // how many items per page
    public int TotalCount { get; }             // total items that matched (before slicing into pages)

    // Calculated live, not stored — always in sync with TotalCount/PageSize
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
```

**Why does the frontend need all these fields, not just `Items`?**

| Field | Why the frontend needs it |
|---|---|
| `Items` | The actual rows/cards to display |
| `Page` | To highlight the active page button |
| `PageSize` | To know how many rows to expect |
| `TotalCount` | To show "47 results found", and to know when data runs out |
| `TotalPages` | To build page number buttons and disable "Next" on the last page |

**Why only `get` (no `init`, no `set`)?** This object is a *computed snapshot* — it's only ever built in one place (`QueryExecutor.Paginate`), never assembled by hand elsewhere. Locking it via constructor-only assignment makes that intention explicit: "this is a finished result, not something you fill in."

---

## 7. `QueryExecutor` — the engine

A `static class` (never instantiated — it's just a toolbox of related methods). Contains two public entry points and several private helpers.

### Step 1 — `Filter<T>`

```csharp
private static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool>? filter)
{
    foreach (T item in source)
    {
        if (filter is null || filter(item))
            yield return item;
    }
}
```

Walks through the source, keeping only items that pass the rule (or everything, if no rule was given).

**About `yield return`:** this does NOT run immediately when called. It sets up a "recipe" — a set of instructions for producing values one at a time, only when something actually iterates over it (`foreach`, or `.ToList()`, etc.). Before iteration happens, no filtering has actually occurred yet, and no collection exists in memory.

### Step 2 — `Materialize<T>`

```csharp
private static IReadOnlyList<T> Materialize<T>(IEnumerable<T> source)
{
    var items = new List<T>();
    foreach (T item in source)
    {
        items.Add(item);
    }
    return items;
}
```

This is the moment the lazy "recipe" from `Filter` actually runs, and its results get locked into a real, fixed `List<T>` in memory.

**Why is this required, not optional?** Sorting needs to compare items against each other — item[0] vs item[3], swap, compare again, etc. That requires a real, complete collection sitting in memory, with every item at a known position. An un-materialized `IEnumerable<T>` can only hand out the *next* value on demand — it has no concept of "all the items, ready to compare." You cannot sort something that doesn't exist yet as a concrete collection. `Materialize` is the mandatory bridge between "a lazy plan" and "an actual list you can operate on."

### Step 3 — `Sort<T, TKey>`

```csharp
private static IReadOnlyList<T> Sort<T, TKey>(IReadOnlyList<T> items, Func<T, TKey> keyReturner, bool descending)
    where TKey : IComparable<TKey>
{
    // items is IReadOnlyList<T> — we're not allowed to mutate it directly.
    // So we copy it into a plain array, which we fully own and can rearrange.
    var array = new T[items.Count];
    for (int i = 0; i < items.Count; i++)
    {
        array[i] = items[i];
    }

    // Array.Sort needs a custom rule for comparing two items — we supply a lambda.
    Array.Sort(array, (a, b) =>
    {
        TKey keyA = keyReturner(a);   // e.g., extract salary of employee a
        TKey keyB = keyReturner(b);   // e.g., extract salary of employee b

        int comparison = keyA.CompareTo(keyB);
        // negative → keyA comes first (ascending)
        // positive → keyA comes after (ascending)

        return descending ? -comparison : comparison;
        // flipping the sign reverses the order → turns ascending into descending
    });

    return array;
}
```

- **Why do we need `Sort` if `keyReturner` already exists?** `keyReturner` only does one tiny job: given ONE item, return the value to compare by (like a ruler that measures one thing). It has no idea how to compare pairs or rearrange a list. `Sort` is the part that repeatedly calls `keyReturner`, compares pairs of items, and physically rearranges the whole collection into order.

- **Why copy into `array` first?** `items` is typed as `IReadOnlyList<T>` — the compiler won't let us call mutating operations on it, even if the real object underneath happens to be a `List<T>`. Copying into a fresh `T[]` gives us something we're fully allowed to rearrange.

### Step 4 — `Paginate<T>`

```csharp
private static PagedResult<T> Paginate<T>(IReadOnlyList<T> items, int page, int pageSize)
{
    page = Math.Max(1, page);           // never allow page below 1
    pageSize = Math.Max(1, pageSize);   // never allow pageSize below 1

    int skip = (page - 1) * pageSize;   // how many items to skip before this page starts
    int totalCount = items.Count;

    var pageItems = new List<T>(Math.Min(pageSize, Math.Max(0, totalCount - skip)));
    // ↑ just pre-sizes the list for efficiency — not a hard limit, just a sensible starting capacity

    for (int i = skip; i < items.Count && pageItems.Count < pageSize; i++)
    {
        pageItems.Add(items[i]);
    }
    // Stop condition 1: i < items.Count        → ran out of data
    // Stop condition 2: pageItems.Count < pageSize → collected enough for this page
    // Both must hold to keep looping — whichever fails first stops the loop.

    return new PagedResult<T>(pageItems, page, pageSize, totalCount);
}
```

**Example trace** — sorted list `[Carla(4000), Eva(3500), Ann(3000)]`, `page = 1`, `pageSize = 2`:

```
skip = (1-1) * 2 = 0
i=0 → add Carla   (pageItems.Count = 1)
i=1 → add Eva     (pageItems.Count = 2) → pageSize reached, stop
```

Result: `Items = [Carla, Eva]`, `TotalCount = 3`, `TotalPages = 2`.

### The two `Execute` methods

```csharp
public static PagedResult<T> Execute<T>(IEnumerable<T> source, QueryOptions<T> options)
{
    IReadOnlyList<T> filtered = Materialize(Filter(source, options.FilterRule));
    return Paginate(filtered, options.Page, options.PageSize);
}

public static PagedResult<T> Execute<T, TKey>(IEnumerable<T> source, QueryOptions<T, TKey> options)
    where TKey : IComparable<TKey>
{
    IReadOnlyList<T> filtered = Materialize(Filter(source, options.FilterRule));

    if (options.KeyReturner is not null)
    {
        filtered = Sort(filtered, options.KeyReturner, options.Descending);
    }

    return Paginate(filtered, options.Page, options.PageSize);
}
```

Two methods, same name, different parameter types — **method overloading**. C# automatically picks the right one based on whether you pass a `QueryOptions<T>` (no sorting) or a `QueryOptions<T, TKey>` (with sorting).

---

## 8. Full Example — Everything Together

```csharp
public class Employee
{
    public string Name { get; init; } = "";
    public string Department { get; init; } = "";
    public decimal Salary { get; init; }
}

var employees = new List<Employee>
{
    new() { Name = "Ann",   Department = "HR", Salary = 3000 },
    new() { Name = "Bob",   Department = "IT", Salary = 5000 },
    new() { Name = "Carla", Department = "HR", Salary = 4000 },
    new() { Name = "Dan",   Department = "IT", Salary = 6000 },
    new() { Name = "Eva",   Department = "HR", Salary = 3500 },
};

// THE REQUEST: HR employees, sorted by salary, highest first, page 1, 2 per page
var options = new QueryOptions<Employee, decimal>
{
    filterRule  = e => e.Department == "HR",
    keyReturner = e => e.Salary,
    descending  = true,
    page        = 1,
    pageSize    = 2
};

var result = QueryExecutor.Execute(employees, options);

// THE ANSWER:
// result.Items      = [Carla(4000), Eva(3500)]
// result.Page       = 1
// result.PageSize   = 2
// result.TotalCount = 3   (Ann, Carla, Eva are the 3 HR employees)
// result.TotalPages = 2
```

---

## Mental Model for myself (if I forget everything else)

1. **`QueryOptions`** = the *request*: what to filter by, sort by, and which page.
2. **`QueryExecutor`** = the *engine*: Filter → Materialize (freeze into a real list) → Sort (if needed) → Paginate.
3. **`PagedResult`** = the *response*: the page's items, plus enough metadata to build pagination UI.
4. **`Filter` returns a lazy `IEnumerable`** — nothing happens until iterated.
5. **`Materialize` is mandatory before sorting** — you cannot sort something that isn't a real, complete collection yet.
6. **Two `QueryOptions` classes and two `Execute` methods exist** so that sorting-related complexity (`TKey`) is only introduced when actually needed.
