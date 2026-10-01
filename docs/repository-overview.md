# Repository Overview

## Metadata
- Purpose: Explain what Personal Log Manager is and the problem its components collectively solve.
- Scope: Conceptual system boundary and current runtime capabilities.
- Primary source areas: `Program.cs`, `Startup.cs`, `PersonalLogController.cs`, `PersonalLogService.cs`, `appsettings.json`, root documentation.
- Related documents: [Architecture](architecture.md), [Data Model](data-model.md), [Log Capability](behaviour/log-capability.md).

## Purpose And Problem
Personal Log Manager provides a self-hosted HTTP API for recording personal activity facts without requiring a database server. A record has a date, optional time and time zone, a template name, and arbitrary string data. Clients can later retrieve structured records or query records and receive natural-language sentences.

The repository solves four connected problems:

- accepting authenticated record mutations over HTTP;
- preserving records in a portable JSON file;
- selecting records with date, time, template, and data filters;
- converting template data into English or Romanian human-readable text.

The service is a storage and presentation API. It does not collect data from external sources, schedule activities, provide a user interface, or distribute records between processes.

## Consumers And Boundaries
The direct consumer is an HTTP client using the `/PersonalLog` resource. The client supplies API-key credentials through the NuciAPI contract and supplies arbitrary template data. Deployment operators supply configuration and filesystem permissions. Nuci packages provide middleware, repository, logging, normalisation, obfuscation, and request-security infrastructure.

The repository boundary contains one ASP.NET Core process, its application service, text builders, mapping code, configuration models, API DTOs, and test projects. The host filesystem, configured logging destination, Nuci package internals, and release helper are external boundaries.

## Principal Capabilities

- `POST /PersonalLog`: persist a new record with a generated `L#########` identifier.
- `GET /PersonalLog`: load all entities, filter them, order them, limit them, and render them.
- `GET /PersonalLog/{id}`: return one structured record.
- `PUT /PersonalLog/{id}`: partially revise scalar fields and merge supplied data keys.
- `DELETE /PersonalLog/{id}`: remove one record.
- English and Romanian text rendering selected by `localisation`.
- API-key protection and middleware-based request and exception handling.

See [Interfaces](interfaces.md) for contracts and [Log Capability](behaviour/log-capability.md) for complete use-case behaviour.

## Conceptual Model
`PersonalLogEntity` is the persistence representation. Its date, time, template, timestamps, and data values are strings because the JSON repository stores the entity directly. `PersonalLog` is the typed rendering representation, with `DateOnly`, nullable `TimeOnly`, and `PersonalLogTemplate`. API request and response classes are transport representations. Mapping is explicit in `PersonalLogMappingExtensions`.

The service deliberately treats the template as a string at write time. The template becomes an enum only when a query renders a record. This means malformed or unsupported template values can enter the JSON store and fail later during mapping or reflection-based builder dispatch.

## Runtime Processes And Stores
There is one normal runtime process. ASP.NET Core hosts the controller and middleware pipeline. `JsonRepository<PersonalLogEntity>` owns the configured JSON file through the `IFileRepository<PersonalLogEntity>` abstraction. `NuciLogger` may write operational events to the configured log file. There are no workers, queues, timers, migrations, caches, or distributed stores.

## Lifecycle At A Glance
1. `Program.CreateHostBuilder` creates the default host and selects `Startup`.
2. `Startup.ConfigureServices` binds configuration and registers middleware dependencies and singleton application services.
3. `Startup.Configure` creates the JSON file if absent and composes middleware and controller endpoints.
4. `PersonalLogController` authorises and forwards each request to `IPersonalLogService`.
5. `PersonalLogService` performs use-case orchestration, repository mutation/query, mapping, rendering, and operation logging.
6. Middleware translates exceptions and produces the HTTP response.

Detailed traces are in [Startup Flow](flows/startup.md) and [HTTP Request Flow](flows/http-request.md).

## What Is Not Implemented
The source contains no database adapter, distributed locking, background execution, client application, account system, schema migration, automatic backup, archival, retry policy, or health endpoint. Deployment must provide suitable filesystem access and operational protection for the JSON and log files.
