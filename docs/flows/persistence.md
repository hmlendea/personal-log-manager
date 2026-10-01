# Persistence Flow

## Metadata
- Purpose: Trace store initialisation and record reads/writes through the repository boundary.
- Scope: Startup file creation, repository calls, explicit commits, and failure paths.
- Primary source areas: `Startup.cs`, `ServiceCollectionExtensions.cs`, `PersonalLogService.cs`, `PersonalLogEntity.cs`.
- Related documents: [Persistence Component](../components/persistence.md), [State And Persistence](../state-and-persistence.md), [Data Model](../data-model.md).

## Initialisation
Startup receives the configured path, validates non-whitespace, creates a missing parent directory, and writes `[]` for a missing file. It does not inspect existing JSON. The DI factory later constructs `JsonRepository<PersonalLogEntity>` using the same path.

## Create
The service generates and probes an id, constructs an entity from request strings and a UTC creation timestamp, calls repository `Add`, then `SaveChanges`. A repository exception is logged by the service and propagated. The id probe and add are separate operations.

## Read
Query calls `GetAll`, then performs filtering and ordering in application memory. Retrieve calls `Get(id)` and maps fields directly into a response. Repository exceptions propagate to middleware.

## Update And Delete
Update calls `Get`, mutates the entity in memory, calls `Update`, then `SaveChanges`. Delete calls `Remove(id)`, then `SaveChanges`. No local transaction, retry, rollback, or conflict resolution is defined.

## Final State
Successful mutations alter the JSON store. Failed operations have package-dependent persistence guarantees after the repository call begins. The application does not emit domain events or maintain a second state store.
