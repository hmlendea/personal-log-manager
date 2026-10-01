# Repository Structure

## Metadata
- Purpose: Explain where implementation, tests, configuration, scripts, and documentation reside.
- Scope: Meaningful tracked files and directories; generated `bin/` and `obj/` output is excluded except where operationally relevant.
- Primary source areas: repository root and project trees.
- Related documents: [Architecture](architecture.md), [Testing](testing.md), [Build And Deployment](build-and-deployment.md), [Change Guide](change-guide.md).

## Top-Level Layout

```text
ARCHITECTURE.md                         Existing architecture synopsis
README.md                               User and developer documentation
LICENSE                                 GPL v3 licence
PersonalLogManager.slnx                 .NET solution
release.sh                              External release-helper launcher
docs/                                   Agent-oriented knowledge base
PersonalLogManager/                     Executable web API
PersonalLogManager.UnitTests/           Unit tests
PersonalLogManager.IntegrationTests/    HTTP integration tests
```

Generated `bin/` and `obj/` directories occur beneath projects after build and restore. They are not source-of-truth inputs and should not receive manual edits.

## Application Project

### `Program.cs` And `Startup.cs`
These files are the host composition root. `Program` selects the default host and `Startup`. `Startup` configures services, creates the store when absent, and orders middleware. New cross-cutting registrations normally belong in `ServiceCollectionExtensions`; pipeline changes belong in `Startup`.

### `Api/`
`Api/Controllers/PersonalLogController.cs` is the HTTP boundary. `Api/Models/` contains transport requests and responses. These classes own HTTP shape, validation attributes, HMAC field ordering, and route-to-body identifier handling. They must not acquire persistence or rendering responsibilities.

### `Configuration/`
`DataStoreSettings` and `SecuritySettings` are bound configuration models consumed by startup, repository registration, and controller authorisation.

### `Data/`
`logs.json` is the default development store. It is a runtime data file, not a schema migration or fixture. Deployments can point `dataStoreSettings:logStorePath` elsewhere.

### `DataAccess/DataObjects/`
`PersonalLogEntity` is the JSON persistence entity. Persistence-specific fields and representation belong here, not in the typed rendering model.

### `Logging/`
`MyLogInfoKey` and `MyOperation` define operation-log vocabulary used by `PersonalLogService`.

### `Service/`
`IPersonalLogService` is the use-case contract and `PersonalLogService` implements it. `Models/` contains `PersonalLog` and `PersonalLogTemplate`. `Mapping/` contains entity/domain conversion. `TextBuilding/` contains the factory, base helpers, interface, and language-specific builders.

A new template normally requires enum support, builder methods in each supported language, helper data keys, unit tests, and documentation updates. See [Change Guide](change-guide.md).

## Unit Test Project
`PersonalLogManager.UnitTests/Service/` tests application service operations. `Service/TextBuilding/` tests the base helpers, factory, and language builders. Test names describe observable behaviour and should remain deterministic around date, culture, and data inputs.

## Integration Test Project
`PersonalLogManager.IntegrationTests/Infrastructure/PersonalLogApiFixture.cs` creates an isolated `WebApplicationFactory<Program>`, injects temporary store and logger paths, disables file logging, and supplies API credentials. The integration test classes exercise middleware, routes, validation, persistence, filtering, rendering, updates, deletion, and lifecycle behaviour.

## Placement Rules

- HTTP contracts and route binding belong in `Api/`.
- Use-case orchestration belongs in `Service/`.
- Persistence representations belong in `DataAccess/DataObjects/`.
- Configuration binding models belong in `Configuration/`.
- Template prose and rendering helpers belong in `Service/TextBuilding/`.
- Cross-cutting registrations belong in `ServiceCollectionExtensions` and middleware ordering in `Startup`.
- Tests belong in the project matching the boundary they exercise.
- Durable architectural knowledge belongs in `docs/`, with [docs/README.md](README.md) as the index.
