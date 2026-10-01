# Build, Deployment, And Operations

## Metadata
- Purpose: Describe restoration, compilation, execution, release, and runtime requirements.
- Scope: SDK target, project files, scripts, host startup, filesystem requirements, and CI evidence.
- Primary source areas: `PersonalLogManager.slnx`, `*.csproj`, `Program.cs`, `Startup.cs`, `release.sh`, root `README.md`, `.github` if present in a checkout.
- Related documents: [Configuration](configuration.md), [Integrations](integrations.md), [Testing](testing.md), [Dependencies](dependencies.md).

## Build Inputs
The solution targets `.NET 10.0`. The application uses `Microsoft.NET.Sdk.Web` and references NuciAPI, NuciDAL, NuciLog, NuciSecurity.HMAC, and NuciText packages. `appsettings.json` is copied to output with `PreserveNewest`. `dotnet restore` obtains package dependencies and `dotnet build` compiles the application and test projects when targeting the solution.

## Runtime Start

```bash
dotnet run --project PersonalLogManager/PersonalLogManager.csproj
```

`Program.Main` builds and runs the default host. Startup binds configuration, registers dependencies, creates the JSON store if absent, composes middleware, and maps controllers. ASP.NET Core reports active listening URLs. The process requires read/write access to the configured JSON store directory and, when enabled, the logger destination.

## Deployment Shape
The implemented deployment shape is one self-hosted process with local filesystem state. A release archive can be obtained from the project release page, configured, extracted, and launched. The source does not contain container definitions, infrastructure-as-code, migrations, service-manager units, health checks, or deployment manifests.

## Release Script
`release.sh` delegates to an external maintainer script at the documented raw GitHub URL and passes the version argument. It is not a self-contained build or packaging implementation. Its downloaded content is external and should be reviewed before execution.

## Operational Characteristics
The JSON store is loaded through a file repository and queries materialise all records before filtering. There is no automatic backup, retention, archival, repair, migration, or log rotation in this repository. Operators must manage file permissions, disk capacity, process supervision, HTTPS certificates, secret injection, and backups.

## Verification
The source can be verified with `dotnet build PersonalLogManager.slnx` and `LC_NUMERIC=en_GB.UTF-8 dotnet test PersonalLogManager.slnx`. See [Testing](testing.md) for the existing suite and known culture-sensitive limitation.
