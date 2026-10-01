# Design Decisions And Constraints

## Metadata
- Purpose: Record implementation decisions that future agents could accidentally simplify or invalidate.
- Scope: Evidence-backed architecture and behaviour; historical rationale is included only when visible.
- Primary source areas: `Startup.cs`, `ServiceCollectionExtensions.cs`, `PersonalLogService.cs`, mapping, text factory, API models, tests.
- Related documents: [Architecture](architecture.md), [Invariants](invariants.md), [Change Guide](change-guide.md), [Ambiguities And Open Questions](ambiguities-and-open-questions.md).

## Single JSON Store
Decision: use `JsonRepository<PersonalLogEntity>` behind `IFileRepository` rather than a database. Evidence: repository registration and `Data/logs.json`. Consequence: portable deployment and simple setup, with full-file query and filesystem consistency constraints. Rationale beyond the source is not established.

## Service-Centred Orchestration
Decision: centralise use-case ordering in singleton `PersonalLogService`. Evidence: all controller actions delegate to `IPersonalLogService`; service owns filtering, sorting, mapping, persistence, and logging. Future endpoint changes must preserve this boundary unless a deliberate architecture change is made.

## Reflection-Based Template Dispatch
Decision: derive method names from enum values and invoke builders with reflection. Evidence: `PersonalLogTextBuilderFactory.BuildLogTextByTemplate`. Consequence: enum values, public method names, and supported languages form a coupled runtime contract. Static dispatch or a new registry would be an intentional redesign, not a harmless simplification.

## Flexible Data Dictionary
Decision: use arbitrary string key/value data rather than a per-template schema. Evidence: request/entity/domain dictionaries and builder helper methods. Consequence: new template data can be introduced without persistence schema changes, but validation and compatibility are distributed across builders and tests.

## Anchored Regex Filters
Decision: permit regex filters but force complete-field matching by adding missing anchors. Evidence: `DoesFieldMatch` and integration test. Consequence: callers can express patterns, but substring matching requires explicit regex structure that still remains within anchors.

## Partial Update Merge
Decision: null scalar fields preserve existing values and supplied dictionary entries merge. Evidence: `UpdatePersonalLog` and integration test. Consequence: clients cannot clear a scalar using null and cannot remove a dictionary key through the current contract.

## Package-Owned Cross-Cutting Behaviour
Decision: delegate API processing, exception handling, scanner protection, request logging, repository mechanics, text normalisation, and obfuscation to Nuci packages. Evidence: package references and registrations. Consequence: package upgrades can alter observable behaviour beyond local source changes and require integration verification.

The repository does not expose historical rationale for these choices. See [Ambiguities](ambiguities-and-open-questions.md).
