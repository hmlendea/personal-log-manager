# Ambiguities And Open Questions

## Metadata
- Purpose: Separate confirmed implementation facts from behaviour owned by dependencies or unresolved intent.
- Scope: Current source ambiguities that matter to future modifications.
- Primary source areas: application service, Nuci integrations, tests, root documentation, configuration.
- Related documents: [Documentation Coverage](documentation-coverage.md), [Error Handling](error-handling.md), [Integrations](integrations.md), [Change Guide](change-guide.md).

## External Repository Semantics
The source does not establish whether NuciDAL `JsonRepository` writes atomically, locks across threads/processes, or rolls back failed saves. Confirm package source or documentation before promising stronger consistency. This matters for concurrent create/update/delete changes.

## HTTP Error Envelopes
Integration tests accept either not found or server error for unknown identifiers and assert server error for unsupported rendering. The exact Nuci exception mapping is not defined locally. Preserve package-level behaviour unless tests and dependency contracts are intentionally revised.

## Configuration Precedence
The application uses `Host.CreateDefaultBuilder` and standard binding but does not enumerate every provider enabled by the host version. The exact precedence between command-line, environment, JSON, and other providers should be confirmed against the deployed host configuration when operational changes depend on it.

## Template Completeness
The enum is large and the language builders contain many methods, but the repository has no generated parity check proving that every enum member has a public method in every language builder. Reflection failures are therefore possible for unsupported combinations. A future parity test would clarify and strengthen this contract.

## Culture And Decimal Rendering
`PersonalLogTextBuilderBase` parses decimals using current culture and formats them with `F2`, then removes only `.00`. Existing memory records two culture-sensitive test failures under comma-decimal culture. The intended cross-locale contract is not established; do not document invariant-culture behaviour without a deliberate code and test change.

## CORS And Hosting Assumptions
The fixed localhost CORS origins and HTTPS redirection appear intended for local clients, but production origin and certificate requirements are not specified. Deployment documentation should obtain those values from the deployment environment rather than infer them from source.

## API Key Transport
Tests show both a bearer-form header and a raw configured key can pass through NuciAPI. The complete accepted header/body/query forms are package-owned and should be verified from NuciAPI documentation before client-contract changes.

## Intent Versus Current Behaviour
The root README describes a lightweight personal-data service, but the source does not define retention, privacy workflows, user identity, or backup. Those remain deployment and product concerns rather than implemented capabilities.
