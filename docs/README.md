# Documentation Index

## Metadata
- Purpose: Navigation and retrieval index for the implementation-grounded Personal Log Manager knowledge base.
- Scope: Current source, tests, configuration, scripts, and documented runtime behaviour.
- Primary source areas: `PersonalLogManager/`, `PersonalLogManager.UnitTests/`, `PersonalLogManager.IntegrationTests/`, `README.md`, `ARCHITECTURE.md`.
- Related documents: [Repository Overview](repository-overview.md), [Architecture](architecture.md), [Change Guide](change-guide.md), [Documentation Maintenance](documentation-maintenance.md).

## Repository Synopsis
Personal Log Manager is a single-process ASP.NET Core REST API for recording personal activity records in a configurable JSON file. `PersonalLogService` owns create, query, retrieve, update, and delete use cases. Query results are rendered into English or Romanian sentences through reflection-selected, template-specific text builders. API-key authorisation, middleware, operation logging, configuration binding, and JSON persistence are supplied partly by Nuci packages.

This corpus describes current implementation behaviour. The source remains authoritative when this documentation and implementation disagree.

## Documentation Map

| Document | Use it for |
|----------|------------|
| [Repository Overview](repository-overview.md) | Purpose, boundaries, runtime shape, and principal capabilities |
| [Architecture](architecture.md) | Layers, dependency direction, composition root, and topology |
| [Repository Structure](repository-structure.md) | Physical files, directories, projects, and placement rules |
| [API Component](components/api.md) | Controller routes, DTOs, validation, and transport boundaries |
| [Service Component](components/service.md) | Use-case orchestration, filtering, mutation, and operation logging |
| [Persistence Component](components/persistence.md) | JSON entity, repository boundary, and commit semantics |
| [Text Building Component](components/text-building.md) | Localisation selection, reflection dispatch, and rendering helpers |
| [Data Model](data-model.md) | API DTOs, entities, domain models, mappings, and JSON shape |
| [Interfaces](interfaces.md) | HTTP routes, service contracts, repository contracts, and HMAC ordering |
| [Configuration](configuration.md) | Configuration sections, keys, defaults, and consumers |
| [Integrations](integrations.md) | Nuci libraries, ASP.NET Core, filesystem, and external release helper |
| [State And Persistence](state-and-persistence.md) | JSON store ownership, lifecycle, consistency, and in-memory state |
| [Error Handling](error-handling.md) | Validation, exceptions, propagation, translation, and logging |
| [Security](security.md) | API-key boundary, middleware controls, secrets, and attack surface |
| [Concurrency And Scheduling](concurrency-and-scheduling.md) | Tasks, singleton state, uniqueness races, and absent workers |
| [Testing](testing.md) | Unit/integration suites, fixtures, commands, and coverage gaps |
| [Build And Deployment](build-and-deployment.md) | SDK, build, runtime, release, and operational requirements |
| [Dependencies](dependencies.md) | Package roles and replacement boundaries |
| [Cross-Cutting Concerns](cross-cutting.md) | Logging, mapping, normalisation, obfuscation, validation, and localisation |
| [Design Decisions](design-decisions.md) | Evidence-backed constraints and non-obvious implementation choices |
| [Invariants](invariants.md) | Rules future modifications must preserve |
| [Change Guide](change-guide.md) | Modification maps for common future work |
| [Ambiguities And Open Questions](ambiguities-and-open-questions.md) | Unresolved behaviour and external-package uncertainty |
| [Documentation Maintenance](documentation-maintenance.md) | Synchronisation rules for future changes |
| [Documentation Coverage](documentation-coverage.md) | Source-to-document audit and known omissions |

## Behaviour And Process Documents

- [Application Lifecycle](behaviour/application-lifecycle.md): host startup, middleware, and shutdown boundaries.
- [Log Capability](behaviour/log-capability.md): create, retrieve, update, delete, and query semantics.
- [Query And Rendering](behaviour/query-and-rendering.md): filtering, ordering, localisation, and text generation.
- [Startup Flow](flows/startup.md): concrete composition and store initialisation sequence.
- [HTTP Request Flow](flows/http-request.md): transport, authorisation, service, repository, and response sequence.
- [Persistence Flow](flows/persistence.md): JSON repository reads, writes, and mutation commits.

## Recommended Reading Sequences

### Adding Or Changing An Endpoint
1. [Interfaces](interfaces.md)
2. [Architecture](architecture.md)
3. [Change Guide](change-guide.md)
4. [Testing](testing.md)
5. [HTTP Request Flow](flows/http-request.md)

### Changing Stored Data
1. [Data Model](data-model.md)
2. [State And Persistence](state-and-persistence.md)
3. [Persistence Component](components/persistence.md)
4. [Invariants](invariants.md)
5. [Testing](testing.md)

### Changing Text Rendering
1. [Query And Rendering](behaviour/query-and-rendering.md)
2. [Text Building Component](components/text-building.md)
3. [Data Model](data-model.md)
4. [Change Guide](change-guide.md)
5. [Unit Testing](testing.md)

### Investigating Runtime Failures
1. [Error Handling](error-handling.md)
2. [HTTP Request Flow](flows/http-request.md)
3. [State And Persistence](state-and-persistence.md)
4. [Integrations](integrations.md)
5. [Ambiguities And Open Questions](ambiguities-and-open-questions.md)

### Changing Deployment Or Configuration
1. [Configuration](configuration.md)
2. [Build And Deployment](build-and-deployment.md)
3. [Security](security.md)
4. [Integrations](integrations.md)
