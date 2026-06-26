# Mini Rule Engine

A console application built in C# that validates different entity types using configurable rules and two validation modes: **fail-fast** and **collect-all**.

---

## Overview

The rule engine allows you to define validation rules for different entity types and run them against a set of entities. Rules are registered once and reused across any number of validations. The engine supports two modes:

- **Fail-Fast** — stops at the first broken rule and reports it immediately
- **Collect-All** — goes through every rule, collects all violations, and reports them all at once

---

## Project Structure

```
MiniRuleEngine/
├── IEntity.cs                  # Base interface for all entities
├── EmployeeEntity.cs           # Employee entity with domain fields
├── OrderEntity.cs              # Order entity with domain fields
├── Rule.cs                     # Represents a single validation rule
├── RuleEngine.cs               # Core engine — stores rules and runs validations
├── RuleRegistration.cs         # Registers all rules into the engine
├── RuleViolationException.cs   # Exception for a single broken rule
├── EntityValidationException.cs # Exception for an entity with multiple violations
└── Program.cs                  # Entry point
```

---

## Entities

### `EmployeeEntity`
Represents an employee record.

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Unique identifier |
| `FullName` | `string` | Full name of the employee |
| `Salary` | `decimal` | Monthly salary |
| `VacationDays` | `int` | Number of vacation days |

### `OrderEntity`
Represents a customer order.

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Unique identifier |
| `CustomerName` | `string` | Name of the customer |
| `Amount` | `decimal` | Order total amount |
| `CountryCode` | `string` | Two-letter country code (e.g. "AM", "US") |
| `ItemCount` | `int` | Number of items in the order |

---

## Validation Rules

### Employee Rules
- `FullName` must not be empty or whitespace
- `Salary` must be between 0 and 1,000,000
- `VacationDays` must be between 0 and 30

### Order Rules
- `CustomerName` must not be empty or whitespace
- `Amount` must be greater than zero
- `CountryCode` must be exactly 2 characters
- `ItemCount` must be at least 1

---

## How It Works

### 1. The delegate
```csharp
delegate void RuleCheck(IEntity entity);
```
Each rule holds one of these. When called, it either passes silently or throws a `RuleViolationException`.

### 2. Adding rules
```csharp
engine.AddRule(new Rule("Employee must have a name", "Employee",
    delegate(IEntity entity)
    {
        EmployeeEntity emp = (EmployeeEntity)entity;
        if (string.IsNullOrWhiteSpace(emp.FullName))
            throw new RuleViolationException("Employee must have a name", "FullName cannot be empty.");
    }
));
```

### 3. Fail-Fast validation
Stops and throws on the first violated rule.
```csharp
engine.ValidateFailFast(entity);
```

### 4. Collect-All validation
Runs every applicable rule, collects all violations, throws once at the end.
```csharp
engine.ValidateCollectAll(entity);
```

---

## Exceptions

### `RuleViolationException`
Thrown when a single rule is violated.

| Property | Description |
|---|---|
| `RuleName` | Name of the rule that was violated |
| `Message` | Human-readable description of the violation |

### `EntityValidationException`
Thrown by collect-all mode when one or more rules are violated.

| Property | Description |
|---|---|
| `Entity` | The entity that failed validation |
| `Violations` | Array of all `RuleViolationException`s collected |
| `Message` | Format: `"Employee #2 has 2 validation error(s)."` |

---

## Sample Output

```
=== Fail-Fast Mode ===
Employee #1 — VALID
[Employee must have a name]: FullName cannot be empty.
Order #20 — VALID
[OrderMustHaveAName]: Order does not have a name.

=== Collect-All Mode ===
Employee #1 — VALID
Employee #2 has 2 validation error(s).
  - [Employee must have a name]: FullName cannot be empty.
  - [SalaryBounds]: Salary must be between 0 and 1000000.
Order #20 — VALID
Order #30 has 1 validation error(s).
  - [OrderMustHaveAName]: Order does not have a name.
```

---

## Key Design Decisions

**Array-based storage with manual resizing** — the `RuleEngine` uses a raw `Rule[]` array that doubles in capacity when full, implemented with `Array.Resize`. This satisfies the homework requirement of not using `List<T>` for rule storage.

**Rule filtering by entity type** — each `Rule` knows its `TargetEntityType`. The engine calls `rule.AppliesTo(entity)` before running any check, so employee rules never run against orders and vice versa.

**Two independent validation modes** — fail-fast and collect-all share the same rule loop logic but differ in how they handle exceptions. Fail-fast rethrows immediately; collect-all accumulates into a `List<RuleViolationException>` and throws once at the end.

---

## Requirements

- .NET 10
- No external dependencies
