# Documentation Coverage

## Metadata
- Purpose: Audit substantial implementation areas against the documentation corpus and expose remaining gaps.
- Scope: Tracked application, tests, configuration, scripts, and root documentation.
- Primary source areas: repository inventory, application project, test projects, root docs.
- Related documents: [Documentation Index](README.md), [Documentation Maintenance](documentation-maintenance.md), [Ambiguities](ambiguities-and-open-questions.md).

## Coverage Map

| Implementation area | Primary documentation | Status |
|----------------------|-----------------------|--------|
| `Program` and `Startup` | [Architecture](architecture.md), [Startup Flow](flows/startup.md), [Application Lifecycle](behaviour/application-lifecycle.md) | Covered |
| DI and settings registration | [Architecture](architecture.md), [Configuration](configuration.md), [Cross-Cutting](cross-cutting.md) | Covered |
| `PersonalLogController` and API DTOs | [API Component](components/api.md), [Interfaces](interfaces.md), [HTTP Flow](flows/http-request.md) | Covered |
| `PersonalLogService` and service interface | [Service Component](components/service.md), [Log Capability](behaviour/log-capability.md) | Covered |
| `PersonalLogEntity` and JSON repository | [Persistence Component](components/persistence.md), [Data Model](data-model.md), [Persistence Flow](flows/persistence.md) | Covered |
| Domain model and template enum | [Data Model](data-model.md), [Text Building](components/text-building.md) | Covered; exhaustive enum prose intentionally omitted |
| Mapping extensions | [Data Model](data-model.md), [Cross-Cutting](cross-cutting.md) | Covered |
| English and Romanian builders | [Text Building](components/text-building.md), [Query And Rendering](behaviour/query-and-rendering.md) | Covered at subsystem level; individual template prose is source-authoritative |
| Logging identifiers and operation statuses | [Cross-Cutting](cross-cutting.md), [Error Handling](error-handling.md) | Covered |
| Unit tests | [Testing](testing.md) | Covered |
| Integration fixture and HTTP tests | [Testing](testing.md), [Security](security.md), [Log Capability](behaviour/log-capability.md) | Covered |
| Project/package declarations | [Dependencies](dependencies.md), [Build And Deployment](build-and-deployment.md) | Covered |
| `appsettings.json` | [Configuration](configuration.md) | Covered |
| `release.sh` | [Build And Deployment](build-and-deployment.md), [Integrations](integrations.md) | Covered |
| Root `README.md` and `ARCHITECTURE.md` | [Repository Overview](repository-overview.md), [Architecture](architecture.md) | Covered and retained |

## Principal Capability Audit
Create, query/render, structured retrieve, partial update, and delete are documented in [Log Capability](behaviour/log-capability.md) and [HTTP Request Flow](flows/http-request.md). Startup and store creation are documented in [Startup Flow](flows/startup.md). No background process, queue, scheduler, migration, or external data collector exists in the inspected source.

## Known Limits
This corpus does not duplicate every method in the very large English and Romanian builder classes or enumerate every `PersonalLogTemplate` member with its data keys. Those details are intentionally left source-authoritative, while the dispatch contract, helper semantics, failure mode, and extension procedure are documented. Nuci package internals, exact HTTP error envelopes, repository locking, and host provider precedence remain external or unresolved and are listed in [Ambiguities](ambiguities-and-open-questions.md).

## Audit Method
The audit compared the source inventory, project files, configuration, scripts, entry points, major classes, tests, and root documents against the documentation index. Markdown links and source references must be checked after all files are present. Build and tests are separate runtime checks because documentation cannot prove source correctness.
