# Persistence Component

## Metadata
- Purpose: Document the JSON persistence boundary and its application-facing contract.
- Scope: `PersonalLogEntity`, NuciDAL repository registration, store initialisation, and explicit commits.
- Primary source areas: `PersonalLogManager/DataAccess/DataObjects/PersonalLogEntity.cs`, `ServiceCollectionExtensions.cs`, `Startup.cs`, `appsettings.json`.
- Related documents: [Data Model](../data-model.md), [State And Persistence](../state-and-persistence.md), [Persistence Flow](../flows/persistence.md), [Integrations](../integrations.md).

## Purpose And Boundary
The persistence component stores `PersonalLogEntity` instances in a JSON file through `IFileRepository<PersonalLogEntity>`. The application constructs `JsonRepository<PersonalLogEntity>` with `DataStoreSettings.LogStorePath`, then the service invokes repository methods. Controllers never access the repository directly.

The repository package owns file serialisation, loading, repository collection semantics, and the exact exception types. This repository does not define transactions, file locking, atomic replacement, migration, backup, repair, or multi-process coordination.

## Entity Representation
`PersonalLogEntity` derives from NuciDAL `EntityBase`, which supplies the identifier property. Its local properties are string `Date`, nullable-by-convention `Time`, `TimeZone`, `CreatedDT`, `UpdatedDT`, `Template`, and `Dictionary<string,string> Data`. The JSON store therefore preserves the service's submitted string values rather than enforcing a relational schema.

## Lifecycle
At startup, `Startup.CreateStoreIfMissing` validates the configured path, creates its parent directory when needed, and writes `[]` when the file does not exist. The service explicitly calls `SaveChanges` after add, update, and remove. Reads use `GetAll` or `Get`.

## Failure And Consistency
A repository exception during a service operation is logged and rethrown. The identifier uniqueness loop uses `ContainsId` before `Add`; this is not an atomic reservation. The query path reads the complete repository before filtering and ordering. A single process is the tested deployment shape; concurrent processes writing one file are not verified.
