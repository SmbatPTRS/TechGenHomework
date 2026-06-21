# Job Simulation

A console-based, event-driven job scheduler implemented in C#. Jobs are queued, executed through pluggable execution strategies, and report their lifecycle through a custom event system that multiple independent services listen to.

## What this project demonstrates

This project was built as an exercise in core C# concepts:

- **Delegates** — `JobExecutor`, a custom delegate type used to assign different execution strategies to different jobs
- **Events & the publisher-subscriber pattern** — `Scheduler` raises a single event that multiple unrelated services subscribe to independently
- **Custom `EventArgs`** — `JobEventArgs` carries job, event type, and error data with every notification
- **Custom collections** — `JobQueue`, a hand-built dynamic array (no `List<T>`), with its own `IEnumerable` / `IEnumerator` implementation
- **Exception handling** — wrapped exceptions, retry logic, and graceful failure reporting

## Architecture

### Domain types
- `JobStatus` — `Pending → Running → Completed / Failed`
- `Job` — holds an `Id`, `Name`, current `Status`, an assigned `Executor`, and `RetryFailuresBeforeSuccess` (used by the retry strategy)
- `JobExecutor` — `delegate void JobExecutor(Job job);`, the contract every execution strategy must match

### Event payload
- `JobEventArgs : EventArgs` — carries the `Job`, an `EventName` (`"JobStarted"`, `"JobCompleted"`, `"JobFailed"`), and an optional `Error`

### Custom queue
- `JobQueue : IEnumerable` — an array-backed queue that resizes itself when full
- `JobQueueEnumerator : IEnumerator` — iterates only over jobs still in `Pending` status

### Scheduler
- `Scheduler` holds the queue and exposes one event: `event EventHandler<JobEventArgs>? JobStateChanged`
- `ExecuteAll()` pulls each pending job, runs it through its assigned executor, and raises `JobStarted` / `JobCompleted` / `JobFailed` accordingly

### Services (subscribers)

| Service | Reacts by |
|---|---|
| `MonitoringService` | Printing a short status line for every event |
| `LoggerService` | Printing a timestamped line, including the error message on failure |
| `StatisticsService` | Counting started / completed / failed jobs, printed once at the end |

### Execution strategies

| Executor | Behavior |
|---|---|
| `FastExecutor` | Short simulated work; fails if the job name contains `"fail-fast"` |
| `SafeExecutor` | Longer simulated work; fails if the job name contains `"fail-safe"`, wrapping the original exception |
| `RetryExecutor` | Up to 3 attempts; fails immediately on every attempt if the name contains `"fail-retry"`, otherwise simulates transient failures using `RetryFailuresBeforeSuccess` before succeeding |

## Job lifecycle

```
Pending --(Scheduler picks it up)--> Running --(success)--> Completed
                                          \
                                           --(exception)--> Failed
```

Every transition raises a `JobStateChanged` event, which all three services receive simultaneously, each reacting independently and without knowing about one another.

## Running it

```bash
cd job_simulation/JobSimulation
dotnet run
```

Or open `JobSimulation.sln` in Rider / Visual Studio and run from there.

## Example output

```
[Monitoring] JobStarted - Job 1 (fast_job1) - Status: Running
[Logger] 17:49:27 JobStarted - Job 1 (fast_job1) - Status: Running
[Monitoring] JobCompleted - Job 1 (fast_job1) - Status: Completed
[Logger] 17:49:27 JobCompleted - Job 1 (fast_job1) - Status: Completed
Job 1 (fast_job1) finished with status Completed
...
Started Count: 5, Completed Count: 4, Failed: 1
```

## Tech

- C# / .NET
- Console application, no external dependencies
