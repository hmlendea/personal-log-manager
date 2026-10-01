# External Dependencies And Integrations

## Metadata
- Purpose: Describe external libraries and host boundaries that materially affect runtime behaviour.
- Scope: ASP.NET Core, NuciAPI, NuciDAL, NuciLog, NuciSecurity.HMAC, NuciText, filesystem, and release helper.
- Primary source areas: `PersonalLogManager.csproj`, `Startup.cs`, `ServiceCollectionExtensions.cs`, `release.sh`, integration fixture.
- Related documents: [Dependencies](dependencies.md), [Configuration](configuration.md), [Error Handling](error-handling.md), [Build And Deployment](build-and-deployment.md).

## ASP.NET Core
The Web SDK supplies hosting, configuration providers, MVC model binding, data-annotation validation, routing, static files, HTTPS redirection, CORS, and endpoint execution. The process is started by `Program` and configured by `Startup`. No internal network client is used by the application.

## NuciAPI
NuciAPI controller and middleware packages provide `NuciApiController`, `ProcessRequest`, API-key authorisation, scanner protection, exception handling, and request logging. The controller supplies `SecuritySettings.ApiKey` to the authorisation policy. Middleware is ordered before routing and endpoint execution as configured in `Startup`; exact status envelopes and scanner rules are package-owned.

## NuciDAL
NuciDAL provides `EntityBase`, `IFileRepository<T>`, and `JsonRepository<T>`. Personal Log Manager supplies the entity type and path, invokes repository methods, and explicitly calls `SaveChanges`. File locking, JSON serialisation details, transaction semantics, and repository exception types are external behaviour.

## NuciLog
NuciLog and NuciLog.Core provide the injected `ILogger`, `LogInfo`, `OperationStatus`, and logger configuration. The service emits operation start, success, debug, and failure events. The repository does not redact log values locally; callers should consider the data passed as log metadata when configuring output and access.

## NuciSecurity.HMAC
`HmacOrder` attributes establish deterministic request and response field ordering for HMAC clients. The repository does not implement signature calculation or verification itself.

## NuciText
`NuciText.Normalisation` normalises generated sentences. `NuciText.Obfuscation` deobfuscates stored dictionary values before builders use them. Both are injected singleton adapters. Their algorithms, supported obfuscation format, and failure behaviour are package-owned.

## Filesystem
The host filesystem stores the JSON record file and optional operational log file. Startup creates the JSON parent directory and empty file. Operators own permissions, backup, retention, disk capacity, and protection from unauthorised access.

## Release Helper
`release.sh` accepts a version argument and downloads and executes an external maintainer script at the URL documented in `README.md`. The script is an operational trust boundary: its contents and availability are external and should be reviewed before execution.

## Retry, Timeout, Authentication, And Rate Limits
The application defines no external network calls, retry policy, timeout policy, cache, or rate-limit handling. API-key authorisation is the only application-facing authentication mechanism. Filesystem access is the primary infrastructure dependency.
