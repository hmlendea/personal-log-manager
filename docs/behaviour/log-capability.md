# Log Capability Behaviour

## Metadata
- Purpose: Describe observable create, query, retrieve, update, and delete behaviour.
- Scope: Normal paths, validation, transformations, persistence, side effects, and failure conditions.
- Primary source areas: `PersonalLogController.cs`, `PersonalLogService.cs`, API models, mapping, repository registration, integration tests.
- Related documents: [Query And Rendering](query-and-rendering.md), [HTTP Request Flow](../flows/http-request.md), [Data Model](../data-model.md), [Error Handling](../error-handling.md).

## Create
`POST /PersonalLog` requires a non-null `Date` through data annotations. After Nuci authorisation and request processing, the service logs a start event, generates an identifier in `L` plus nine decimal digits, probes the repository until `ContainsId` reports absence, copies request fields into `PersonalLogEntity`, sets `CreatedDT` to the current UTC round-trip timestamp, calls `Add` and `SaveChanges`, then logs success. Persistence exceptions are logged and rethrown. The operation does not validate date format, template membership, time format, or data keys.

## Query
`GET /PersonalLog` loads all entities. Optional date, time, and template filters use anchored, case-sensitive regex matching. Each supplied data pair adds a case-insensitive anchored regex filter, so data criteria are conjunctive. The service logs the count before sorting and limiting, orders by date descending, time descending, template ascending, and creation timestamp ascending, then takes the requested count. Each selected entity is parsed to `PersonalLog` and rendered. The response contains rendered text and its count.

An empty repository or no matches returns an empty `logs` sequence and count zero. Invalid regexes, unparseable persisted values, unsupported enum names, missing builder methods, direct missing data keys, and culture-sensitive parsing can fail the request.

## Retrieve
`GET /PersonalLog/{id}` calls repository `Get(id)` and copies stored fields into `GetLogByIdResponse`. Null entity data becomes an empty dictionary. It does not parse the date, time, or template and does not render text. Unknown identifier behaviour is delegated to NuciDAL and NuciAPI exception translation.

## Update
`PUT /PersonalLog/{id}` assigns the route id to the request, authorises it, then loads the entity. Non-null Date, Time, TimeZone, and Template values replace existing values. A non-null Data dictionary is merged key by key. `UpdatedDT` is replaced with the current UTC round-trip timestamp, then `Update` and `SaveChanges` execute. There is no explicit no-op branch and no field-level validation beyond transport validation.

## Delete
`DELETE /PersonalLog/{id}` creates a request containing the route id, authorises it, invokes `Remove`, and calls `SaveChanges`. Unknown ids and persistence failures propagate through the common error path.

## Observable Side Effects
Mutations change the JSON store and emit operation log events. Querying does not mutate the store but emits operation events and may invoke deobfuscation and text normalisation. The service has no event bus, cache invalidation, retry, or background side effect.
