[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/personal-log-manager)](https://github.com/hmlendea/personal-log-manager/releases/latest)
[![Build Status](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/personal-log-manager/actions/workflows/dotnet.yml)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://gnu.org/licenses/gpl-3.0)

# Personal Log Manager

Personal Log Manager is an ASP.NET Core REST API for storing and querying personal activity logs in a JSON file.

The API supports:

- creating logs
- querying logs with filters
- updating existing logs
- deleting logs
- localized text output (English and Romanian)

## Table of Contents

- [Overview](#overview)
- [Requirements](#requirements)
- [Configuration](#configuration)
- [Run the API](#run-the-api)
- [API Reference](#api-reference)
- [Filtering Behavior](#filtering-behavior)
- [Templates and Data](#templates-and-data)
- [Storage](#storage)
- [Development](#development)
- [Release](#release)
- [Contributing](#contributing)
- [Related Projects](#related-projects)
- [License](#license)

## Overview

Base route:

- `/PersonalLog`

Controller actions:

- `POST /PersonalLog` to store a new log
- `GET /PersonalLog` to retrieve log text entries
- `PUT /PersonalLog` to update an existing log
- `DELETE /PersonalLog` to delete a log

## Requirements

- .NET SDK/runtime with support for `net10.0`

## Configuration

Default configuration is defined in `appsettings.json`:

```json
{
	"dataStoreSettings": {
		"logStorePath": "Data/logs.json"
	},
	"securitySettings": {
		"apiKey": "[[PERSONAL_LOG_MANAGER_API_KEY]]"
	},
	"nuciLoggerSettings": {
		"logFilePath": "logfile.log",
		"isFileOutputEnabled": true
	}
}
```

Important settings:

- `dataStoreSettings.logStorePath`: path to the JSON file used as persistent storage.
- `securitySettings.apiKey`: API key required by the controller authorization.

At startup, the app creates the store directory/file automatically if missing.

## Run the API

```bash
dotnet restore
dotnet run
```

By default, ASP.NET Core prints the listening URLs in the console.

## API Reference

### Authentication

Requests are validated through the NuciAPI request model and an API key authorizer.

All request DTOs inherit from `NuciApiRequest`, so include the inherited auth fields expected by your client integration (typically including `apiKey`).

### 1) Store Log

`POST /PersonalLog`

Request body:

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

Notes:

- `date` is required.
- `time`, `timeZone`, `template`, and `data` are optional.
- A new identifier is generated internally (format like `L123456789`).

### 2) Get Logs

`GET /PersonalLog`

Supported query parameters:

- `apiKey`
- `date`
- `time`
- `template`
- `localisation` (default: `en`, Romanian also supports `ro`, `ro-RO`, `ro-MD`)
- `count` (default: `1`, allowed range: `1..100000`)

Optional structured filter:

- `data` key-value filters can be passed by clients that support object-style query serialization.

Example request:

```text
GET /PersonalLog?apiKey=YOUR_API_KEY&date=2026-04-23&template=WaterDrinking&localisation=en&count=5
```

Success response shape:

```json
{
	"logs": [
		"L123456789 2026-04-23: 19:10 Europe/Bucharest: I drank 300 ml of water"
	],
	"count": 1
}
```

### 3) Update Log

`PUT /PersonalLog`

Request body:

```json
{
	"apiKey": "YOUR_API_KEY",
	"id": "L123456789",
	"time": "19:15",
	"data": {
		"amount": "350"
	}
}
```

Notes:

- `id` is required.
- Any provided field is updated.
- For `data`, provided keys are merged/overwritten on the existing dictionary.

### 4) Delete Log

`DELETE /PersonalLog`

Request body:

```json
{
	"apiKey": "YOUR_API_KEY",
	"id": "L123456789"
}
```

Notes:

- `id` is required.

## Filtering Behavior

When querying logs:

- `date`, `time`, and `template` filters are applied as anchored regex patterns.
- each provided `data` filter is matched case-insensitively against existing log data values.
- output is sorted by:
	1. date descending
	2. time descending
	3. template ascending
	4. creation timestamp ascending

## Templates and Data

Template names map to enum values in:

- `Service/Models/PersonalLogTemplate.cs`

The text output for each template is produced by localized builders:

- `Service/TextBuilding/Localisation/EnglishTextBuilder.cs`
- `Service/TextBuilding/Localisation/RomanianTextBuilder.cs`

Different templates use different keys in the `data` object (for example: `platform`, `discriminator`, `amount`, `currency`, and many others).

## Storage

Logs are persisted as JSON records at the configured `logStorePath`.

Stored fields include:

- `id`
- `date`
- `time`
- `timeZone`
- `template`
- `data`
- `createdDT`
- `updatedDT`

## Development

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run
```

### Release

The repository includes `release.sh`, which delegates to the upstream deployment script used by the project maintainer.

```bash
bash ./release.sh 1.0.0
```

This script downloads and executes an external release helper from: `https://raw.githubusercontent.com/hmlendea/deployment-scripts/master/release/dotnet/10.0.sh`

**Note:** Piping into `bash` is an intensely controversial topic. Please review any external scripts before running them in your environment!

## Contributing

Contributions are welcome.

Please:

- keep the changes cross-platform
- keep the pull requests focused and consistent with the existing style
- update the documentation when the behaviour changes
- add or update the tests for any new behaviour

## Related Projects

- [Personal Data Logger](https://github.com/hmlendea/personal-data-logger)
- [Personal Log Manager](https://github.com/hmlendea/personal-log-manager)

## License

Licensed under the GNU General Public License v3.0 or later.
See [LICENSE](./LICENSE) for details.