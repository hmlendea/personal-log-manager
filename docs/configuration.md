# Configuration

## Metadata
- Purpose: Inventory configuration sources, keys, defaults, binding, and consumers.
- Scope: ASP.NET Core configuration, application settings, Nuci logger settings, test overrides, and secret handling.
- Primary source areas: `appsettings.json`, `ServiceCollectionExtensions.cs`, `Startup.cs`, `DataStoreSettings.cs`, `SecuritySettings.cs`, integration fixture.
- Related documents: [Security](security.md), [Build And Deployment](build-and-deployment.md), [Architecture](architecture.md).

## Sources And Precedence
`Program.CreateHostBuilder` uses `Host.CreateDefaultBuilder`, so standard ASP.NET Core providers supply configuration. `ServiceCollectionExtensions.AddConfigurations` binds the `dataStoreSettings` and `securitySettings` sections. Nuci logger settings are registered through `AddNuciLoggerSettings(Configuration)`. Integration tests add an in-memory provider through `WebApplicationFactory` configuration and thereby override the default file values.

The source does not define a custom precedence mechanism, command-line parser, feature-flag system, or environment-variable names. Standard host provider precedence applies.

## Keys

| Section/key | Default in `appsettings.json` | Consumer | Behaviour |
|-------------|-------------------------------|----------|-----------|
| `dataStoreSettings:logStorePath` | `Data/logs.json` | `Startup`, `JsonRepository` registration | Store file path; startup creates directory/file |
| `securitySettings:apiKey` | Placeholder `[[PERSONAL_LOG_MANAGER_API_KEY]]` | `PersonalLogController` | Expected NuciAPI API key |
| `nuciLoggerSettings:logFilePath` | `logfile.log` | NuciLogger | File destination when file output is active |
| `nuciLoggerSettings:isFileOutputEnabled` | `true` | NuciLogger | Enables or disables configured file output |

No genuine secret is present in the repository. Deployment must supply the API key through protected configuration rather than commit it.

## Validation And Failure
`Startup.CreateStoreIfMissing` rejects a null or whitespace path with `ArgumentException`. Directory creation and file creation can fail due to permissions, invalid paths, or filesystem state. `DataStoreSettings` and `SecuritySettings` properties have no local validation attributes. Missing or unsuitable security configuration is therefore partly delegated to NuciAPI authorisation behaviour.

## Test Configuration
`PersonalLogApiFixture` uses unique temporary paths for JSON and logs, injects the integration API key, disables file logging, and selects the `Testing` environment. It also supplies a forwarded-for header and bearer authorisation header. Fixture disposal removes the temporary store directory.

## Configuration Change Guidance
Adding a setting requires a bound model or package configuration registration, a consumer registration/lifetime decision, default documentation, deployment guidance, and tests with explicit override values. Review [Change Guide](change-guide.md) and [Documentation Maintenance](documentation-maintenance.md).
