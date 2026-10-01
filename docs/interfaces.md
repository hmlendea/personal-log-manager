# Interfaces And Contracts

## Metadata
- Purpose: Record externally and internally significant contracts.
- Scope: HTTP resource, DTO semantics, service interface, repository interface, builder factory, configuration keys, and HMAC ordering.
- Primary source areas: `PersonalLogController.cs`, `Api/Models/`, `Service/IPersonalLogService.cs`, `Service/TextBuilding/IPersonalLogTextBuilderFactory.cs`, `appsettings.json`.
- Related documents: [API Component](components/api.md), [Configuration](configuration.md), [Data Model](data-model.md), [Security](security.md).

## HTTP Contract

| Method | Route | Input | Output or effect |
|--------|-------|-------|------------------|
| POST | `/PersonalLog` | `StoreLogRequest` JSON | Success after persistence; generated identifier is observable in operation logs, not a response body defined locally |
| GET | `/PersonalLog` | `GetLogRequest` query | `GetLogResponse` with rendered `logs` and computed `count` |
| GET | `/PersonalLog/{id}` | Route identifier | `GetLogByIdResponse` |
| PUT | `/PersonalLog/{id}` | `UpdateLogRequest` JSON | Success after partial update and save |
| DELETE | `/PersonalLog/{id}` | Route identifier | Success after remove and save |

All operations pass through NuciAPI authorisation and request processing. Exact generic success/error envelope behaviour belongs to NuciAPI.

## Request Semantics
`Date` is required only on create. Query count must be between `1` and `100000`. Date, time, and template filters are regular expressions that the service anchors to the complete field when anchors are absent. Data filters are also anchored and case-insensitive, and every supplied key/value pair must match.

Update route identifiers override any body `Identifier`. Non-null scalar values replace stored values. Supplied dictionary keys overwrite matching values and add new keys; omitted keys remain.

## Internal Contracts
`IPersonalLogService` exposes the five use cases consumed by the controller. `IFileRepository<PersonalLogEntity>` supplies `GetAll`, `Get`, `ContainsId`, `Add`, `Update`, `Remove`, and `SaveChanges` through NuciDAL. `IPersonalLogTextBuilderFactory.BuildLogText` accepts a typed `PersonalLog` and localisation code and returns a sentence.

## HMAC Ordering
`HmacOrder` values define deterministic field order for NuciSecurity.HMAC integration. Store fields are ordered Date, Time, TimeZone, Template, Data. Query fields are Date, Time, Template, Localisation, Data, Count. Structured response fields are Id, Date, Time, TimeZone, Template, Data, CreatedDateTime, UpdatedDateTime. Update request fields omit an HMAC order for `Identifier` because the route assigns it and order starts at Date.

## Configuration Contract
The recognised local keys are `dataStoreSettings:logStorePath`, `securitySettings:apiKey`, `nuciLoggerSettings:logFilePath`, and `nuciLoggerSettings:isFileOutputEnabled`. See [Configuration](configuration.md).
