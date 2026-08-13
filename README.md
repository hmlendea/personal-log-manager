[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/funding)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/personal-log-manager)](https://github.com/hmlendea/personal-log-manager/releases/latest)
[![Build Status](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# Personal Log Manager

Personal Log Manager is a self-hosted ASP.NET Core REST API for recording, querying, and rendering personal activity logs. It combines structured JSON persistence with flexible filters and English or Romanian natural-language output, providing a lightweight service for personal data workflows.

## 📑 Table of Contents

- [Table of Contents](#table-of-contents)
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
  - [CLI Installation](#cli-installation)
- [Configuration](#configuration)
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
- [Architecture](#architecture)
- [Contributing](#contributing)
- [Related Projects](#related-projects)
- [Supporting the Project](#supporting-the-project)
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

- **OS:** Linux (`arm`, `arm64`, or `x64`), macOS (`arm64` or `x64`), or Windows (`arm64` or `x64`)
- **RAM:** No project-specific minimum is specified
- Read-write filesystem access for the JSON store and configured log output

## 📦 Installation

[![Obtain it from GitHub](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/github.png)](https://github.com/hmlendea/personal-log-manager/releases)

Download the archive for your operating system and architecture from the [latest release](https://github.com/hmlendea/personal-log-manager/releases/latest), extract it, configure the API key and storage path, then launch the included executable.

### CLI Installation

For a source installation:

```bash
git clone https://github.com/hmlendea/personal-log-manager.git
cd personal-log-manager
dotnet restore
```

## ⚙️ Configuration

All settings are loaded from the configuration file. The subsequent keys are recognised:

| Section | Key | Description |
|---------|-----|-------------|
| `dataStoreSettings` | `logStorePath` | Filesystem path of the JSON record store; defaults to `Data/logs.json` |
| `securitySettings` | `apiKey` | Secret used by the controller API-key authorisation policy |
| `nuciLoggerSettings` | `logFilePath` | Destination used when file logging is active; defaults to `logfile.log` |
| `nuciLoggerSettings` | `isFileOutputEnabled` | Activates or deactivates file output; defaults to `true` |

Default values are defined in [appsettings.json](PersonalLogManager/appsettings.json). Supply the genuine API key through protected deployment configuration rather than committing it. Startup creates the configured store when it is absent.

## 🌍 Localisation

Translations are located in the project's localisation resources. The subsequent languages are currently supported:

| Language | Code | Status |
|----------|------|--------|
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

### Release

The repository includes `release.sh`, which delegates to the upstream deployment script used by the project maintainer.

```bash
bash ./release.sh 2.9.0
```

This script downloads and executes an external release helper from `https://raw.githubusercontent.com/hmlendea/deployment-scripts/master/release/dotnet/10.0.sh`.

**Note:** Piping into `bash` is an intensely controversial topic. Please review any external scripts before running them in your environment!

### Dependencies

| Package | Purpose |
|---------|---------|
| `NuciAPI`, `NuciAPI.Controllers`, and NuciAPI middleware packages | Request processing, API-key authorisation, scanner protection, exception handling, and request logging |
| `NuciDAL` | File-repository abstraction and JSON persistence |
| `NuciLog` and `NuciLog.Core` | Structured operation logging |
| `NuciSecurity.HMAC` | Deterministic request and response field ordering for HMAC integration |
| `NuciText.Normalisation` and `NuciText.Obfuscation` | Sentence normalisation and stored-value deobfuscation |

## 🗂️ Project Structure

The solution contains the subsequent projects:
- `PersonalLogManager`: ASP.NET Core API, application service, persistence adapter, and localised text builders
- `PersonalLogManager.UnitTests`: NUnit tests for service orchestration and text rendering

The key directories inside `PersonalLogManager/` are:
| Directory | Purpose |
|-----------|---------|
| `Api/` | Controller and transport request/response models |
| `Configuration/` | Bound data-store and security settings |
| `Data/` | Default JSON store |
| `DataAccess/` | Persistence entity definitions |
| `Logging/` | Operation and log-context identifiers |
| `Service/` | Use cases, domain models, mapping, and localised text building |

## 🏗️ Architecture

See [ARCHITECTURE.md](./ARCHITECTURE.md) for a structural synopsis and component interactions.

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Maintain the existing public contract intact unless a breaking change is intentional
- Maintain the pull requests as focused and consistent with the existing code style
- Maintain your branch up-to-date with `master`
- Revise the documentation when behaviour changes
- Properly test all changes, including edge cases and error conditions
- Add unit tests for any new or changed functionality

## 🔗 Related Projects

- [Personal Data Logger](https://github.com/hmlendea/personal-data-logger): Companion service for collecting personal data from external sources
- [Personal Log Manager Client](https://github.com/hmlendea/personal-log-manager-client): Client application for interacting with this API

## 💝 Supporting the Project

Discovered a problem or have a suggestion? [Open an issue](https://github.com/hmlendea/personal-log-manager/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/funding) or starring ⭐️ it on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/funding)

## 📄 License

This project is being distributed under the `GNU General Public License v3.0` or later.
See [LICENSE](./LICENSE) for further information.