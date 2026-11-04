[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/personal-log-manager)](https://github.com/hmlendea/personal-log-manager/releases/latest)
[![Build Status](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml)
[![License](https://img.shields.io/github/license/hmlendea/personal-log-manager)](https://github.com/hmlendea/personal-log-manager/blob/master/LICENSE)

# Personal Log Manager

Personal Log Manager is a self-hosted ASP.NET Core REST API for recording, querying, and rendering personal activity logs. It combines structured JSON persistence with flexible filters and English or Romanian natural-language output, providing a lightweight service for personal data workflows.

## 📑 Table of Contents

- [Capabilities](#capabilities)
- [Usage](#usage)
  - [Authentication](#authentication)
  - [Start the API](#start-the-api)
  - [Store a Log](#store-a-log)
  - [Query Logs](#query-logs)
  - [Retrieve a Log](#retrieve-a-log)
  - [Update a Log](#update-a-log)
  - [Delete a Log](#delete-a-log)
  - [Filtering and Ordering](#filtering-and-ordering)
  - [Templates and Data](#templates-and-data)
  - [Storage](#storage)
- [Known Limitations](#known-limitations)
- [System Requirements](#system-requirements)
- [Installation](#installation)
- [Configuration](#configuration)
  - [Configuration Files](#configuration-files)
  - [Settings](#settings)
- [Localisation](#localisation)
- [Development](#development)
  - [Requirements](#requirements)
  - [Setup](#setup)
  - [Build](#build)
  - [Run](#run)
  - [Test](#test)
  - [Release](#release)
  - [Dependencies](#dependencies)
- [Project Structure](#project-structure)
  - [Projects and Packages](#projects-and-packages)
  - [Directories](#directories)
- [Architecture](#architecture)
- [Privacy and Data](#privacy-and-data)
- [Security](#security)
- [Contributing](#contributing)
- [Code of Conduct](#code-of-conduct)
- [Related Projects](#related-projects)
- [Project Engagement](#project-engagement)
- [License](#license)

## ✨ Capabilities

- Create, retrieve, partially update, and delete personal log records through a REST resource.
- Query logs by date, time, template, and arbitrary data values with regular-expression matching.
- Render matching records as English or Romanian natural-language sentences.
- Preserve structured records in a configurable JSON file without requiring a database server.
- Protect controller operations with a configured NuciAPI API-key policy.
- Record request and operation diagnostics through NuciAPI middleware and NuciLog.

## 🚀 Usage

Configure the API key, start the service, and submit structured records to the `/PersonalLog` resource. ASP.NET Core prints the active listening URLs when the process starts.

### Authentication

Every controller operation supplies the configured API-key authorisation policy to NuciAPI. Request DTOs inherit from `NuciApiRequest`; clients must include the inherited authentication fields expected by their NuciAPI integration. The subsequent examples use `apiKey`.

### Start the API

```bash
dotnet run --project PersonalLogManager/PersonalLogManager.csproj
```

### Store a Log

`POST /PersonalLog`

```json
{
  "apiKey": "YOUR_API_KEY",
  "date": "2026-04-23",
  "time": "19:10",
  "timeZone": "Europe/Bucharest",
  "template": "WaterDrinking",
  "data": {
    "amount": "300",
    "amount_currency": "ml"
  }
}
```

`date` is required. `time`, `timeZone`, `template`, and `data` are optional. The service generates an identifier in the `L#########` format.

### Query Logs

`GET /PersonalLog`

Supported query parameters:
- `apiKey`
- `date`
- `time`
- `template`
- `localisation`, which defaults to `en`
- `count`, which defaults to `1` and accepts `1..100000`
- `data`, for clients that support dictionary-style query serialisation

Example request:

```text
GET /PersonalLog?apiKey=YOUR_API_KEY&date=2026-04-23&template=WaterDrinking&localisation=en&count=5
```

Success response:

```json
{
  "logs": [
    "L123456789 2026-04-23: 19:10 Europe/Bucharest: I drank 300 ml of water"
  ],
  "count": 1
}
```

### Retrieve a Log

`GET /PersonalLog/L123456789`

The response contains `id`, `date`, `time`, `timeZone`, `template`, `data`, `createdDateTime`, and `updatedDateTime`.

### Update a Log

`PUT /PersonalLog/L123456789`

```json
{
  "apiKey": "YOUR_API_KEY",
  "time": "19:15",
  "data": {
    "amount": "350"
  }
}
```

Only supplied scalar fields are revised. Supplied `data` keys are merged into the existing dictionary and replace values with identical keys.

### Delete a Log

`DELETE /PersonalLog/L123456789`

The route identifier selects the record for deletion. Supply authentication through the NuciAPI client contract used by the deployment.

### Filtering and Ordering

When querying logs:
- `date`, `time`, and `template` filters are applied as anchored, case-sensitive regular expressions.
- Every supplied `data` filter must match; data values are compared case-insensitively with anchored regular expressions.
- Output is sorted by:
  1. date descending
  2. time descending
  3. template ascending
  4. creation timestamp ascending

### Templates and Data

Template names correspond to [PersonalLogTemplate.cs](PersonalLogManager/Service/Models/PersonalLogTemplate.cs). Each template consumes relevant keys from the `data` dictionary, such as `platform`, `discriminator`, `amount`, or `currency`.

The language-specific output is implemented by [EnglishTextBuilder.cs](PersonalLogManager/Service/TextBuilding/Localisation/EnglishTextBuilder.cs) and [RomanianTextBuilder.cs](PersonalLogManager/Service/TextBuilding/Localisation/RomanianTextBuilder.cs).

### Storage

Logs are persisted as JSON records at the configured `logStorePath`. Stored values include the identifier, date, optional time and time zone, template, arbitrary data, creation timestamp, and optional update timestamp. Startup creates the directory and an empty store when they are absent.

## ⚠️ Known Limitations

- The JSON repository loads all records before applying query filters, ordering, and count limitation.
- Multiple processes writing the same store are not a verified deployment configuration.
- Filters are caller-supplied regular expressions; invalid or computationally expensive patterns can fail or delay requests.
- Decimal parsing and formatting depend upon the process culture, and comma-decimal cultures can produce different rendered values.
- The repository defines no schema migration, automatic backup, archival, or repair process.

## 🖥️ System Requirements

| Component | Minimum | Recommended |
|-----------|---------|-------------|
| OS | Linux (`arm`, `arm64`, or `x64`), macOS (`arm64` or `x64`), or Windows (`arm64` or `x64`) | — |
| RAM | No project-specific minimum is specified | — |
| Filesystem | Read-write access for the JSON store and configured log output | — |

## 📦 Installation

[![Obtain it from GitHub](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/github.png)](https://github.com/hmlendea/personal-log-manager/releases)

Download the archive for your operating system and architecture from the [latest release](https://github.com/hmlendea/personal-log-manager/releases/latest), extract it, configure the API key and storage path, then launch the included executable.

## ⚙️ Configuration

All settings are loaded from the configuration file. The subsequent keys are recognised:

### Configuration Files

| File | Scope | Purpose |
|------|-------|---------|
| `appsettings.json` | Application | Data-store path, API key, and logger settings |

### Settings

The subsequent settings are recognised:
| Section | Key | Type | Default | Required | Description |
|---------|-----|------|---------|----------|-------------|
| `dataStoreSettings` | `logStorePath` | `string` | `Data/logs.json` | No | Filesystem path of the JSON record store |
| `securitySettings` | `apiKey` | `string` | — | Yes | Secret used by the controller API-key authorisation policy |
| `nuciLoggerSettings` | `logFilePath` | `string` | `logfile.log` | No | Destination used when file logging is active |
| `nuciLoggerSettings` | `isFileOutputEnabled` | `bool` | `true` | No | Activates or deactivates file output |

Default values are defined in [appsettings.json](PersonalLogManager/appsettings.json). Supply the genuine API key through protected deployment configuration rather than committing it. Startup creates the configured store when it is absent.

## 🌍 Localisation

Translations are located in the project's localisation resources. The subsequent languages are currently supported:

| Language | Locale | Status |
|----------|--------|--------|
| English | `en` and all unrecognised values | Complete |
| Romanian | `ro`, `ro-RO`, `ro-MD` | Complete |

## 🛠️ Development

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Setup

All NuGet dependencies are restored automatically by `dotnet restore`.

### Build

```bash
dotnet build PersonalLogManager/PersonalLogManager.csproj
```

### Run

```bash
dotnet run --project PersonalLogManager/PersonalLogManager.csproj
```

### Test

```bash
dotnet test PersonalLogManager.slnx
```

The solution includes isolated HTTP integration tests in `PersonalLogManager.IntegrationTests`; each test uses a temporary JSON store and exercises the configured ASP.NET Core middleware, controller routes, validation, persistence, filtering, localisation, update, and deletion paths.

### Release

The repository includes `release.sh`, which delegates to the upstream deployment script used by the project maintainer.

```bash
bash ./release.sh 2.9.0
```

This script downloads and executes an external release helper from `https://raw.githubusercontent.com/hmlendea/dotnet-release-scripts/refs/heads/master/dotnet/10.0.sh`.

**Note:** Piping into `bash` is an intensely controversial topic. Please review any external scripts before running them in your environment!

### Dependencies

| Package | Version | Scope | Purpose |
|---------|---------|-------|---------|
| `NuciAPI` | 3.5.1 | Runtime | Request processing and API-key authorisation |
| `NuciAPI.Controllers` | 2.3.1 | Runtime | Controller base classes and request contracts |
| `NuciAPI.Middleware` | 2.0.2 | Runtime | Middleware pipeline components |
| `NuciAPI.Middleware.ExceptionHandling` | 1.0.1 | Runtime | Exception handling middleware |
| `NuciAPI.Middleware.Logging` | 1.0.1 | Runtime | Request logging middleware |
| `NuciAPI.Middleware.Security` | 1.0.5 | Runtime | Security middleware |
| `NuciDAL` | 3.2.1 | Runtime | File-repository abstraction and JSON persistence |
| `NuciLog` | 1.2.1 | Runtime | Structured operation logging |
| `NuciLog.Core` | 3.0.0 | Runtime | Core logging contracts |
| `NuciSecurity.HMAC` | 4.1.3 | Runtime | Deterministic request and response field ordering for HMAC integration |
| `NuciText.Normalisation` | 1.0.1 | Runtime | Sentence normalisation |
| `NuciText.Obfuscation` | 1.1.1 | Runtime | Stored-value deobfuscation |

## 🗂️ Project Structure

### Projects and Packages

| Project | Type | Purpose |
|---------|------|---------|
| `PersonalLogManager` | ASP.NET Core API | API, application service, persistence adapter, and localised text builders |
| `PersonalLogManager.UnitTests` | Unit tests | NUnit tests for service orchestration and text rendering |
| `PersonalLogManager.IntegrationTests` | Integration tests | NUnit HTTP integration tests for the running API pipeline and JSON persistence |
| `PersonalLogManager.ArchitectureTests` | Architecture tests | ArchUnit.NET and Roslyn fitness tests for layer dependencies and conventions |

### Directories

| Directory | Purpose |
|-----------|---------|
| `PersonalLogManager/Api/` | Controller and transport request/response models |
| `PersonalLogManager/Configuration/` | Bound data-store and security settings |
| `PersonalLogManager/Data/` | Default JSON store |
| `PersonalLogManager/DataAccess/` | Persistence entity definitions |
| `PersonalLogManager/Logging/` | Operation and log-context identifiers |
| `PersonalLogManager/Service/` | Use cases, domain models, mapping, and localised text building |

## 🏗️ Architecture

Personal Log Manager is a single-process ASP.NET Core REST API that stores structured personal activity records and returns either structured records or localised natural-language text. See the [Architecture Documentation](./ARCHITECTURE.md) for the system context, principal components, runtime flows, ownership boundaries, dependencies, constraints, and extension points.

See [docs/README.md](./docs/README.md) for the implementation-grounded documentation corpus.

## 🛡️ Privacy and Data

Personal Log Manager is a self-hosted ASP.NET Core REST API that processes personal log data locally within the operator's deployment. It stores log records in a configurable JSON file and writes structured application logs to a local file. No telemetry, crash reports, update checks, or other data is transmitted to project maintainers or external services. All data processing occurs locally. For complete details, see [PRIVACY.md](./PRIVACY.md).

## 🔒 Security

Personal Log Manager uses HMAC-based API key authentication for all controller operations. The application processes log data locally with no external data transmission. Supported versions receive security updates via GitHub Releases. For vulnerability reporting, supported versions, scope, and disclosure policy, see [SECURITY.md](./SECURITY.md).

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Preserve the existing public contract unless a breaking change is intentional
- Submit focused pull requests that conform to the existing code style
- Maintain your branch synchronised with `master`
- Revise the documentation when functionality changes
- Properly test all modifications, including edge cases and error conditions
- Include tests for additional or modified functionality
- Raise a new [issue](https://github.com/hmlendea/personal-log-manager/issues) for problems or suggestions

See the [Contributing Guidelines](./CONTRIBUTING.md) for details on how to report issues, suggest enhancements, and submit changes.

## 📜 Code of Conduct

See the [Code of Conduct](./CODE_OF_CONDUCT.md) for the standards of conduct expected in all project spaces.

## 🔗 Related Projects

- [Personal Data Logger](https://github.com/hmlendea/personal-data-logger): Companion service for collecting personal data from external sources
- [Personal Log Manager Client](https://github.com/hmlendea/personal-log-manager-client): Client application for interacting with this API

## 💝 Project Engagement

Discovered a problem or have a suggestion? [Open an issue](https://github.com/hmlendea/personal-log-manager/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or starring ⭐️ it on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/funding)

## 📄 License

This project is being distributed under the `GNU General Public License v3.0` or later.
See [LICENSE](./LICENSE) for further information.