# Agent-Oriented Change Guide

## Metadata
- Purpose: Direct future modifications to the correct source, tests, registrations, and documentation.
- Scope: Common changes to this repository and their compatibility risks.
- Primary source areas: all application, test, configuration, and documentation directories.
- Related documents: [Architecture](architecture.md), [Repository Structure](repository-structure.md), [Testing](testing.md), [Documentation Maintenance](documentation-maintenance.md).

## Add Or Change An Endpoint
Start at `PersonalLogController.cs` and the relevant request/response model. Revise `IPersonalLogService` and `PersonalLogService`, then update API integration tests, [Interfaces](interfaces.md), [API Component](components/api.md), and the relevant behaviour/flow document. Preserve `ProcessRequest`, API-key authorisation, route identifier precedence, HMAC order, and Nuci error translation.

## Add A Template Or Change Rendering
Add or revise `PersonalLogTemplate`, public `Build{Template}LogText` methods in every supported localisation builder, and any base helper data-key assumptions. Add focused unit tests for English, Romanian, missing values, obfuscation, culture-sensitive numbers, and branch conditions. Revise [Text Building](components/text-building.md), [Query And Rendering](behaviour/query-and-rendering.md), [Data Model](data-model.md), and [Invariants](invariants.md).

## Add Or Change A Persistence Field
Revise `PersonalLogEntity`, transport models, mapping extensions, service copy/update logic, structured responses, and JSON compatibility tests. Determine whether old records remain readable. Update [Data Model](data-model.md), [State And Persistence](state-and-persistence.md), [Persistence Component](components/persistence.md), and configuration/migration notes if applicable. There is no migration framework to update automatically.

## Add Configuration
Add a settings property or package binding in `ServiceCollectionExtensions`, select its lifetime and default, and add an override in integration tests. Update `appsettings.json`, [Configuration](configuration.md), [Build And Deployment](build-and-deployment.md), security documentation when sensitive, and tests for missing/invalid values.

## Add An External Integration
Define the narrowest abstraction at the consuming boundary, register it in the composition root, document authentication/data/failure/timeout behaviour, and add integration tests that do not require the live system. Update [Integrations](integrations.md), [Dependencies](dependencies.md), [Architecture](architecture.md), and [Ambiguities](ambiguities-and-open-questions.md) when package behaviour remains external.

## Modify Validation Or Error Handling
Inspect data annotations, controller processing, service guards, mapping, and Nuci exception middleware. Add regression tests at the nearest observable boundary, including successful and failure branches. Update [Interfaces](interfaces.md), [Error Handling](error-handling.md), [Security](security.md), and affected behaviour flows.

## Modify Deployment Or Release
Inspect project files, `appsettings.json`, `release.sh`, and host startup. Test restore/build/run assumptions. Update [Build And Deployment](build-and-deployment.md), [Configuration](configuration.md), [Integrations](integrations.md), and the root README when user-facing commands change.

## Common Omissions
Do not add an enum value without builder methods. Do not change a DTO without HMAC order and integration review. Do not assume null means clear during update. Do not add a persistence field without mapping. Do not rely on query rendering to validate records written by create. Do not claim atomic id uniqueness or multi-process safety without changing the repository boundary and tests.
