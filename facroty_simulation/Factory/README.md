# Factory Production & Logistics Simulation System

A tick-based concurrent factory pipeline simulation built in C# (.NET 10), where items flow through production, quality control, storage, and transport stages.

---

## System Overview

```
Machines → Order Line → Quality Checker → Storage → Transport → Stock
```

Each simulation **tick** represents one unit of time. Every component acts on each tick, creating a realistic pipeline where stages operate at different speeds and capacities.

---

## Components

| Component | Role |
|---|---|
| `Machine` | Produces items at configurable intervals and pushes them into the Order Line |
| `OrderLine` | Bounded queue between production and processing — rejects items when full |
| `QualityChecker` | Pulls items from the Order Line, inspects them over multiple ticks, passes or fails each one |
| `Storage` | Holds passed items grouped by type, waiting for transport |
| `Transport` | Arrives periodically, collects items from Storage and delivers them to Stock |
| `Stock` | Final destination — stores all successfully delivered items |

---

## Project Structure

```
Factory/
├── Components/
│   ├── ITickable.cs
│   ├── Machine.cs
│   ├── OrderLine.cs
│   ├── QualityChecker.cs
│   ├── Stock.cs
│   ├── Storage.cs
│   └── Transport.cs
├── Config/
│   └── SimulationConfig.cs
├── Models/
│   ├── Item.cs
│   └── ItemType.cs
└── Program.cs
```

---

## Configuration

All simulation parameters are centralized in `SimulationConfig.cs`. No magic numbers anywhere else.

```csharp
SimulationConfig config = SimulationConfig.WithDefaults();
```

| Parameter | Default | Description |
|---|---|---|
| `TotalTicks` | 35 | How long the simulation runs |
| `OrderLineCapacity` | 5 | Max items waiting for inspection |
| `QualityPassPercentage` | 80 | % chance an item passes inspection |
| `MinQualityCheckTicks` | 1 | Minimum ticks to inspect one item |
| `MaxQualityCheckTicks` | 3 | Maximum ticks to inspect one item |
| `TransportArrivalInterval` | 4 | Transport arrives every N ticks |
| `TransportCapacityPerArrival` | 6 | Max items carried per trip |
| `MachineAInterval` | 1 | Machine A produces every N ticks |
| `MachineBInterval` | 2 | Machine B produces every N ticks |
| `MachineCInterval` | 3 | Machine C produces every N ticks |

---

## How to Run

```bash
git clone https://github.com/SmbatPTRS/TechGenHomework.git
cd TechGenHomework/Factory
dotnet run
```

---

## Sample Output

```
--- Tick 1 ---
Machine 1: produced item 100 of type A

--- Tick 4 ---
Machine 1: produced item 103 of type A
Item 100 PASSED
Item 103 added to storage under type A
Transport has arrived
Item 100 added to stock under type A

=== Simulation Complete ===
Stock
- Type: A | Total Items: 14
- Type: B | Total Items: 1
- Type: C | Total Items: 1
```

---

## Design Decisions

- **Custom linked-list queue** for `OrderLine` — built from scratch using a `Node` class to practice data structures
- **Tick-based architecture** — all components implement `ITickable` and are driven by a single loop in `Program.cs`, making the execution order explicit and deterministic
- **Overflow strategy** — when the Order Line is full, new items are logged and skipped rather than blocking production
- **Shared `Random` instance** — seeded from config and passed into `QualityChecker`, so results are reproducible across runs
- **Config-driven** — every tunable value lives in `SimulationConfig`, making it easy to change simulation behavior without touching component logic

---

## Technologies

- C# / .NET 10
- JetBrains Rider
