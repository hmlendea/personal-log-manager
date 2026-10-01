# State And Persistence

## Metadata
- Purpose: Describe every state store, owner, lifecycle, and consistency characteristic.
- Scope: JSON records, singleton collaborators, request state, timestamps, and absent distributed state.
- Primary source areas: `Startup.cs`, `ServiceCollectionExtensions.cs`, `PersonalLogService.cs`, `PersonalLogEntity.cs`, integration fixture.
- Related documents: [Persistence Component](components/persistence.md), [Data Model](data-model.md), [Concurrency And Scheduling](concurrency-and-scheduling.md), [Persistence Flow](flows/persistence.md).

## Persistent State
The configured JSON file is the authoritative record store. It contains an array of `PersonalLogEntity` values. A record persists until explicit deletion or operator file manipulation. There is no retention, archive, backup, migration, version marker, or repair routine.

The service writes after each create, update, or delete through `SaveChanges`. The application does not group multiple mutations into a transaction. Whether NuciDAL writes atomically or locks the file is not established locally.

## In-Memory State

- ASP.NET Core configuration objects are singleton settings values.
- `PersonalLogService` retains one mutable `Random` instance.
- Nuci repository, text factory, normaliser, obfuscator, and logger instances are registered as singletons.
- Request DTOs, query enumerables, mapped `PersonalLog` values, and generated text are request-scoped transient data.
- No cache or cross-request domain state exists.

## State Transitions
A create transition is absent -> entity with id and `CreatedDT`. An update transition changes selected fields, merges dictionary data, and sets `UpdatedDT`. A delete transition removes the entity. Query and retrieve are read-only. There is no explicit record status or lifecycle enum.

## Consistency And Recovery
The service probes identifier absence, then inserts, so uniqueness is not guaranteed across concurrent writers. Reads load repository state at the time of the call. Partial failure after an in-memory mutation but before successful `SaveChanges` is governed by NuciDAL; no local rollback or retry exists. Startup only creates a missing file and does not validate or repair existing JSON.
