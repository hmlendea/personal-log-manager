# Application Service Component

## Metadata
- Purpose: Describe `IPersonalLogService` and `PersonalLogService`, the use-case orchestration centre.
- Scope: Create, query, retrieve, update, delete, filtering, ordering, identifier generation, mapping invocation, and operation logging.
- Primary source areas: `PersonalLogManager/Service/IPersonalLogService.cs`, `PersonalLogManager/Service/PersonalLogService.cs`, `PersonalLogManager/Logging/`.
- Related documents: [Log Capability](../behaviour/log-capability.md), [Query And Rendering](../behaviour/query-and-rendering.md), [Persistence Component](persistence.md), [Error Handling](../error-handling.md).

## Responsibilities
`PersonalLogService` coordinates all five application operations. It creates persistence entities, generates a unique-looking identifier, calls repository methods, applies anchored regex filters, orders and limits records, invokes mapping and text rendering, merges update data, sets timestamps, and records `Started`, `Success`, and `Failure` operation events through NuciLog.

It intentionally does not own HTTP routing, request authorisation, JSON serialisation, file format mechanics, or language-specific template prose.

## Dependencies And Lifetime
The service receives `IPersonalLogTextBuilderFactory`, `IFileRepository<PersonalLogEntity>`, and Nuci `ILogger` by constructor injection. It is registered as a singleton. Its `Random` instance is consequently shared by all requests in the process. The repository and logger are also singleton registrations.

## Operation Semantics

- `StorePersonalLog`: logs request metadata, generates an unused `L` plus nine-digit identifier by repeatedly calling `ContainsId`, adds an entity, stores an ISO `DateTime.UtcNow` timestamp, and calls `SaveChanges`.
- `GetPersonalLogs`: loads `GetAll`, applies optional date/time/template anchored case-sensitive regex filters and all data filters with case-insensitive matching, logs the pre-order result count, sorts date and time descending then template and creation timestamp ascending, takes `Count`, and renders each result.
- `GetPersonalLog`: calls `Get(id)` and maps the entity into `GetLogByIdResponse`, replacing null data with an empty dictionary.
- `UpdatePersonalLog`: gets the entity, applies only non-null scalar request values, creates the data dictionary if necessary, overwrites supplied keys while retaining omitted keys, sets `UpdatedDT`, calls `Update` and `SaveChanges`.
- `DeletePersonalLog`: calls `Remove(id)` and `SaveChanges`.

## Important Internal Rules
`DoesFieldMatch` adds `^` and `$` when absent, so a caller's unanchored pattern still has to match the complete field. Data filters are conjunctive because each dictionary pair adds another `Where`. `BuildLogTexts` catches rendering failures and wraps them with the last record identifier.

The service does not validate template names during storage, parse dates or times before persistence, retry repository operations, or establish atomic uniqueness between identifier probing and insertion. These are documented in [Invariants](../invariants.md) and [Ambiguities](../ambiguities-and-open-questions.md).
