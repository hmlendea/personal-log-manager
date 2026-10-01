# Security Model

## Metadata
- Purpose: Document implemented trust boundaries and security-relevant assumptions.
- Scope: API-key authorisation, middleware, input handling, secrets, filesystem, logging, and development diagnostics.
- Primary source areas: `Startup.cs`, `PersonalLogController.cs`, `SecuritySettings.cs`, `appsettings.json`, Nuci package registrations, integration fixture.
- Related documents: [Configuration](configuration.md), [Integrations](integrations.md), [Error Handling](error-handling.md), [Interfaces](interfaces.md).

## Trust Boundaries
Untrusted HTTP clients cross into the ASP.NET Core process. The process crosses a filesystem boundary when reading/writing the JSON store and log output. Configuration providers supply the API key and paths. Nuci package middleware forms part of the request protection boundary.

## Authentication And Authorisation
Every controller action supplies `NuciApiAuthorisation.ApiKey(securitySettings.ApiKey)` to `ProcessRequest`. Integration tests verify missing and invalid `Authorization` headers are rejected, and also verify that the configured API key without a `Bearer` scheme is accepted by the package contract. The source defines one shared API key and no users, roles, scopes, per-record ownership, or authorisation differences between operations.

## Input Risks And Controls
ASP.NET Core model binding and data annotations validate required Date and query Count range. The service accepts caller-supplied regex patterns, arbitrary dictionary keys/values, template strings, and date/time strings. Regexes are anchored but not otherwise constrained; invalid or expensive patterns can produce errors or resource use. Template-specific direct dictionary access can raise exceptions for absent required keys.

The application does not define a request-size limit, regex timeout, schema validation for template data, or explicit output encoding beyond framework serialisation and Nuci text normalisation. Nuci scanner protection and request processing are package-owned controls.

## Secrets And Sensitive Data
The repository contains only an API-key placeholder. Real keys must arrive through protected deployment configuration. Logs can include identifiers, template, date, time, localisation, count, and exception data; arbitrary personal data can also influence builder output. The application has no local redaction policy. Restrict filesystem permissions for both JSON and log files.

## Transport And Diagnostics
`UseHttpsRedirection` is enabled. CORS permits a fixed list of localhost origins and any header/method for those origins. `UseDeveloperExceptionPage` is enabled only in development and can expose diagnostic details; production deployments should not select the development environment.

## Persistence And Operational Assumptions
The JSON file is a sensitive personal-data store. The application does not encrypt it, implement access control on the filesystem, or manage backups. Those controls belong to deployment infrastructure. The release helper executes a downloaded external script and therefore must be treated as a privileged supply-chain operation.
