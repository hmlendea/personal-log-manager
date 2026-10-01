# Dependencies

## Metadata
- Purpose: Explain significant package dependencies by architectural role.
- Scope: Direct package references in `PersonalLogManager.csproj` and repository abstractions used by source.
- Primary source areas: `PersonalLogManager/PersonalLogManager.csproj`, `ServiceCollectionExtensions.cs`, `Startup.cs`, controllers, service, text building.
- Related documents: [Integrations](integrations.md), [Architecture](architecture.md), [Build And Deployment](build-and-deployment.md), [Change Guide](change-guide.md).

## Direct Dependencies

| Package | Role | Encapsulation and replacement impact |
|---------|------|---------------------------------------|
| `NuciAPI` | Base request and response contracts and API infrastructure | Transport and authorisation contracts depend on it |
| `NuciAPI.Controllers` | `NuciApiController`, `ProcessRequest`, authorisation model | Replacing it requires controller and request pipeline changes |
| `NuciAPI.Middleware` | Middleware base integration | Startup pipeline depends on package extensions |
| `NuciAPI.Middleware.ExceptionHandling` | Exception translation | Client-visible error behaviour depends on it |
| `NuciAPI.Middleware.Logging` | Request logging | Request diagnostics depend on it |
| `NuciAPI.Middleware.Security` | Scanner protection | Security pipeline depends on it |
| `NuciDAL` | Entity base, file repository, JSON repository | Persistence is abstracted at service boundary but entity/repository contracts depend on it |
| `NuciLog` and `NuciLog.Core` | Logger implementation, settings, log values and statuses | Service operation logging depends on their types and semantics |
| `NuciSecurity.HMAC` | `HmacOrder` metadata | Client signing compatibility depends on field order |
| `NuciText.Normalisation` | Sentence normalisation | Rendered output post-processing depends on it |
| `NuciText.Obfuscation` | Data deobfuscation | Stored sensitive values may require its format |

The application directly owns abstractions only for the service and text-builder factory; the repository and logger contracts are package types. Dependency upgrades require running both unit and integration suites and checking serialised and HTTP compatibility.
