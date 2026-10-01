# Error Handling And Resilience

## Metadata
- Purpose: Document validation, exception boundaries, propagation, and recovery behaviour.
- Scope: Transport validation, service catches, mapping/rendering failures, repository failures, startup failures, and middleware translation.
- Primary source areas: `Startup.cs`, `PersonalLogController.cs`, `PersonalLogService.cs`, mapping, text factory, Nuci middleware registrations, integration tests.
- Related documents: [Security](security.md), [Integrations](integrations.md), [Log Capability](behaviour/log-capability.md), [HTTP Request Flow](flows/http-request.md).

## Error Classes

| Failure | Local handling | Observable consequence |
|---------|----------------|-------------------------|
| Missing create Date | Data annotation and Nuci/ASP.NET request processing | Bad request |
| Query count outside `1..100000` | Data annotation and request processing | Bad request |
| Missing/invalid API key | Nuci authorisation | Unauthorized response |
| Invalid regex | No service catch around regex construction | Service failure translated by exception middleware |
| Unknown repository id | NuciDAL exception | Tested as not found or server error |
| Invalid persisted date/template | Mapping exception | Query fails; structured retrieval may still copy strings |
| Unsupported template builder | `MissingMethodException`, then query wrapper | Server error through middleware |
| Builder failure | Unwrapped by factory, wrapped with last id by service | Server error through middleware |
| Store initialisation failure | Startup propagation | Host startup fails |
| Save/read failure | Service logs and rethrows | Middleware translates failure |

## Service Logging And Propagation
Create, query, retrieve, update, and delete operations log start and success/failure events. Failures inside the main operation `try` blocks are passed to `logger.Error` and rethrown without local recovery. The create identifier probe occurs before its persistence `try` block, so a `ContainsId` failure does not receive the create failure event.

`BuildLogTexts` catches any mapping or builder exception, includes the last record id in an `InvalidOperationException`, and preserves the original exception as inner exception. The outer query handler logs and rethrows the wrapper.

## Resilience Characteristics
There are no retries, fallbacks, circuit breakers, timeouts, cancellation tokens, dead-letter paths, or compensating operations. The application assumes the configured filesystem, repository, logger, and Nuci middleware remain available. Idempotency is not established for create or update requests; repeated create requests generate separate records.

## Client-Visible Translation
The controller does not translate exceptions. NuciAPI exception middleware owns the HTTP status, envelope, and diagnostic exposure. Development additionally enables `UseDeveloperExceptionPage`; this is an environment-sensitive disclosure consideration documented in [Security](security.md).
