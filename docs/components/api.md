# API Component

## Metadata
- Purpose: Document the HTTP controller and transport model boundary.
- Scope: `PersonalLogController` and `Api/Models`; middleware behaviour is covered in [Integrations](../integrations.md).
- Primary source areas: `PersonalLogManager/Api/Controllers/PersonalLogController.cs`, `PersonalLogManager/Api/Models/`.
- Related documents: [Interfaces](../interfaces.md), [HTTP Request Flow](../flows/http-request.md), [Security](../security.md), [Service Component](service.md).

## Purpose And Scope
The API component translates HTTP requests into `IPersonalLogService` calls and exposes service results as ASP.NET Core action results. It owns route declarations, model binding, data-annotation validation, HMAC property ordering, route identifier precedence, and the configured API-key policy. It does not own business filtering, persistence, identifier generation, or text rendering.

## Controller Operations
`PersonalLogController` is routed as `[controller]`, yielding `/PersonalLog`. Each action delegates through inherited `NuciApiController.ProcessRequest`, passing the request, operation delegate, and `NuciApiAuthorisation.ApiKey(securitySettings.ApiKey)`.

| Action | HTTP route | Service call |
|--------|------------|--------------|
| `AddPersonalLog` | `POST /PersonalLog` | `StorePersonalLog(StoreLogRequest)` |
| `GetPersonalLogs` | `GET /PersonalLog` | `GetPersonalLogs(GetLogRequest)` |
| `GetPersonalLog` | `GET /PersonalLog/{id}` | `GetPersonalLog(id)` |
| `UpdatePersonalLog` | `PUT /PersonalLog/{id}` | `UpdatePersonalLog(UpdateLogRequest)` |
| `DeletePersonalLog` | `DELETE /PersonalLog/{id}` | `DeletePersonalLog(DeleteLogRequest)` |

For update, the route value is assigned to `request.Identifier` before processing. A body identifier therefore cannot select another record. The GET-by-id action constructs a `GetLogByIdRequest` for authorisation/validation but calls the service with the route string.

## Transport Models
`StoreLogRequest.Date` is required. `GetLogRequest.Count` has range validation `1..100000` and defaults to `1`; `Localisation` defaults to `en`. Other log fields are nullable strings or nullable dictionaries. `GetLogByIdResponse` returns structured fields and inherits the Nuci success response. `GetLogResponse.Logs` defaults to an empty sequence and computes `Count` from the returned sequence.

All relevant request and response fields have `HmacOrder` attributes. The HMAC package owns the wider signing contract; this repository defines the order values shown in [Interfaces](../interfaces.md).

## Failure Behaviour
Missing or invalid authorisation is rejected by NuciAPI before service execution. Data-annotation failures produce a client validation response through framework/Nuci middleware. Repository and rendering exceptions are not converted in the controller; they propagate to NuciAPI exception handling. The exact status for unknown identifiers is package-dependent and is tested as either not found or server error.
