# Concurrency And Scheduling

## Metadata
- Purpose: Describe asynchronous conduct, shared state, ordering, and absent background execution.
- Scope: ASP.NET Core request concurrency, singleton state, identifier generation, repository operations, and lifecycle.
- Primary source areas: `Program.cs`, `Startup.cs`, `ServiceCollectionExtensions.cs`, `PersonalLogService.cs`, integration fixture.
- Related documents: [Architecture](architecture.md), [State And Persistence](state-and-persistence.md), [Invariants](invariants.md).

## Request Execution
Controller actions and service methods are synchronous. The integration tests use asynchronous `HttpClient` calls, but the application service performs synchronous repository and rendering calls. ASP.NET Core can process multiple requests concurrently; this repository defines no explicit queue, scheduler, worker, timer, cancellation token, or background service.

## Shared State
The singleton `PersonalLogService` shares a mutable `System.Random` instance for id generation. The singleton repository shares its NuciDAL state. The source does not add locks around either object. Thread-safety and file coordination are therefore dependent on framework and Nuci package guarantees that are not defined locally.

## Identifier Race
`GenerateUniqueLogId` repeatedly calls `repository.ContainsId` until an absent value is found, then the caller invokes `Add` later. Two concurrent requests can observe the same absent identifier. The code does not reserve ids atomically or retry an insertion conflict. This is a local invariant for single-process normal use, not a distributed uniqueness guarantee.

## Ordering
Query ordering is deterministic for equal values only to the extent of the string fields and `CreatedDT` values. Date and time are sorted lexically in descending order, template ascending, and creation timestamp lexically ascending. No cross-request ordering guarantee exists.

## Lifecycle
Startup initialises the store before requests are served. There is no scheduled work or explicit shutdown persistence. Host and dependency disposal are framework-owned. The test fixture disposes the client and factory and deletes its temporary directory.
