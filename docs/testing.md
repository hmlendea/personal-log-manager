# Testing Architecture

## Metadata
- Purpose: Map tests to production boundaries, validated behaviour, commands, and coverage gaps.
- Scope: Unit tests, HTTP integration tests, fixture isolation, test data, and absent coverage.
- Primary source areas: `PersonalLogManager.UnitTests/`, `PersonalLogManager.IntegrationTests/`, project files, repository testing memory.
- Related documents: [Change Guide](change-guide.md), [Build And Deployment](build-and-deployment.md), [Error Handling](error-handling.md), [Documentation Coverage](documentation-coverage.md).

## Test Projects
`PersonalLogManager.UnitTests` is an NUnit project for service orchestration, base text helpers, factory dispatch, and English/Romanian builders. `PersonalLogManager.IntegrationTests` is an NUnit project for the running ASP.NET Core pipeline, controller routes, middleware, validation, JSON persistence, filtering, localisation, update, and deletion.

## Integration Fixture
`PersonalLogApiFixture` creates `WebApplicationFactory<Program>` with the `Testing` environment. It overrides the store and logger paths with unique temporary paths, sets a known API key, disables file output, creates an HTTPS client without automatic redirects, and supplies forwarded-for and authorisation headers. Disposal closes the client/factory and recursively deletes the temporary store directory.

## Validated Behaviour
The integration suite verifies unauthorised requests, accepted API-key forms, required Date, Count range, empty query results, create defaults, default count, regex filtering and anchoring, time/template filters, descending date ordering, Romanian output, partial update and dictionary merge, route identifier precedence, unknown-id outcomes, unsupported-template rendering failure, and a complete create/query/retrieve/delete lifecycle.

The repository memory records a passing `dotnet test PersonalLogManager.slnx` run with `LC_NUMERIC=en_GB.UTF-8`, totalling 122 tests. Decimal rendering can expose two failures under a comma-decimal culture in the VS Code test adapter because `PersonalLogTextBuilderBase` uses current culture and strips only `.00`.

## Coverage Gaps
There are no dedicated tests for multi-process writes, concurrent id generation, malformed existing JSON, store initialisation failure, logger failure, CORS policy, HTTPS redirection, static files, scanner protection, HMAC verification, regex resource exhaustion, or exact Nuci exception envelopes. Nuci package internals are not unit tested here.

## Commands

```bash
 dotnet restore
 dotnet build PersonalLogManager.slnx
 LC_NUMERIC=en_GB.UTF-8 dotnet test PersonalLogManager.slnx
```

The culture setting is relevant to the current decimal-rendering test constraint. Tests should remain deterministic and use temporary stores for persistence scenarios.
