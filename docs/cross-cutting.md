# Cross-Cutting Concerns

## Metadata
- Purpose: Gather behaviours spanning transport, service, persistence, and rendering layers.
- Scope: Dependency injection, logging, validation, mapping, localisation, obfuscation, normalisation, serialisation, and HTTP middleware.
- Primary source areas: `Startup.cs`, `ServiceCollectionExtensions.cs`, `Logging/`, `Service/Mapping/`, `Service/TextBuilding/`, API models.
- Related documents: [Architecture](architecture.md), [Integrations](integrations.md), [Configuration](configuration.md), [Error Handling](error-handling.md).

## Dependency Injection
`Startup.ConfigureServices` composes settings, Nuci middleware, and custom services. Settings, repository, service, factory, normaliser, obfuscator, and logger are singleton registrations. Constructor injection carries collaborators into controller, service, and factory. Concrete language builders are created by the factory rather than registered.

## Operation Logging
`PersonalLogService` logs operation names from `MyOperation`, status values from NuciLog, and selected request metadata from `MyLogInfoKey`. Start events use `Info`; successful completions use `Debug`; caught failures use `Error`. The logger destination and file-output switch are package configuration.

## Validation
Data annotations enforce create Date and query Count range. The service adds no domain validator for date/time/template/data values. Persistence accepts arbitrary strings. Query matching and rendering are therefore the first points where several malformed values fail.

## Mapping And Serialisation
API models are bound and serialised by ASP.NET Core. Mapping extensions convert between string entity fields and typed rendering values. The JSON repository serialises entities. HMAC order attributes provide deterministic field metadata for NuciSecurity.HMAC.

## Localisation And Normalisation
The factory selects `EnglishTextBuilder` by default and `RomanianTextBuilder` for three codes. Builders create template-specific prose; the factory then invokes Nuci sentence normalisation. Romanian helper logic translates common conjunction forms, while English does the reverse for shared data values.

## Obfuscation
Every base-builder data accessor that returns a value calls `INuciTextObfuscator.Deobfuscate`, except direct dictionary accesses in some concrete methods. This means template-specific code can have different missing-key and obfuscation behaviour; changes must inspect the exact method.

## HTTP Middleware
Startup orders Nuci exception handling, scanner protection, request logging, development diagnostics, HTTPS redirection, CORS, static files, routing, authorisation, and controller endpoints. Middleware details not implemented in this repository remain package-owned.
