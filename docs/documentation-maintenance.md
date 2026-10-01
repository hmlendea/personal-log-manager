# Documentation Maintenance

## Metadata
- Purpose: Keep this knowledge base synchronised with implementation changes.
- Scope: Required documentation review by change category.
- Primary source areas: all source, tests, configuration, scripts, and `docs/`.
- Related documents: [Documentation Index](README.md), [Change Guide](change-guide.md), [Documentation Coverage](documentation-coverage.md).

## Synchronisation Rules

- Behaviour changes require the affected document in `docs/behaviour/` and any process trace in `docs/flows/` to be revised.
- Architectural changes require `architecture.md`, the relevant component document, and `repository-structure.md` review.
- New or changed DTOs, entities, mappings, templates, or serialised fields require `data-model.md` and `interfaces.md` review.
- New configuration requires `configuration.md`, `appsettings.json` guidance, deployment review, and security review when sensitive.
- New integrations or packages require `integrations.md`, `dependencies.md`, architecture review, and failure documentation.
- Persistence changes require `state-and-persistence.md`, `components/persistence.md`, `data-model.md`, and invariant review.
- Error or validation changes require `error-handling.md`, `security.md` when input-related, and regression tests.
- New invariants require `invariants.md` and relevant change guidance.
- Development, test, or release procedure changes require `testing.md`, `build-and-deployment.md`, or `change-guide.md` as appropriate.
- Every new document must be linked from [docs/README.md](README.md) and have metadata containing purpose, scope, source areas, and related documents.

## Change-Impact Matrix

| Code modification | Review or revise |
|-------------------|------------------|
| Controller route/DTO | `interfaces.md`, `components/api.md`, behaviour, flows, tests |
| Service use case | `components/service.md`, behaviour, flow, errors, invariants, tests |
| Entity or mapping | `data-model.md`, persistence, state, invariants, tests |
| Template/builder | `components/text-building.md`, query rendering, data model, tests |
| Configuration | `configuration.md`, security, deployment, tests |
| Package/middleware | `integrations.md`, dependencies, architecture, errors/security |
| Store lifecycle | persistence, state, deployment, security, flow |
| Test infrastructure | `testing.md`, build/deployment, coverage |
| Release script | build/deployment, integrations, security |

After changes, run the link audit, build, and test suite where feasible. Source remains authoritative; unresolved discrepancies belong in [Ambiguities](ambiguities-and-open-questions.md).
