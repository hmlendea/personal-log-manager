# Privacy and Personal Data

This document describes how Personal Log Manager at https://github.com/hmlendea/personal-log-manager handles personal data. It covers the application behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 📑 Table of Contents

- [What This Document Covers](#what-this-document-covers)
- [Self-Hosted Deployments](#self-hosted-deployments)
- [Data We Handle](#data-we-handle)
- [Processing and Use](#processing-and-use)
- [Storage, Retention, and Deletion](#storage-retention-and-deletion)
- [External Processing and Integrations](#external-processing-and-integrations)
- [User Controls and Requests](#user-controls-and-requests)
- [Data Protection and Security](#data-protection-and-security)
- [Document Changes](#document-changes)
- [Contact](#contact)

## 🔎 What This Document Covers

This document describes how Personal Log Manager at https://github.com/hmlendea/personal-log-manager handles personal data. It covers the application behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 🏠 Self-Hosted Deployments

Personal Log Manager is a self-hosted ASP.NET Core REST API. This document covers the application behaviour as distributed by the project maintainers. Self-hosted instance operators control their instance's configuration, local storage, logs, backups, access controls, retention, and request handling. The project maintainers do not operate user instances and do not receive data from self-hosted deployments.

The application sends no telemetry, crash reports, update checks, or other data to project maintainers or external services. All data processing occurs locally within the operator's deployment. Operators may configure optional integrations such as reverse proxies, object storage, email providers, or authentication services; any data flows introduced by those integrations are the operator's responsibility.

## 📥 Data We Handle

### Data Provided to the Application

- API key for authentication (configured by the operator, supplied by clients in requests)
- Log date (required), time, time zone, template name, and arbitrary key-value data dictionary (supplied by clients in store and update requests)
- Query filters: date, time, template, localisation, count, and data filters (supplied by clients in query requests)
- Log identifier for retrieval, update, and deletion (supplied by clients in route and request body)

### Data Generated or Collected by the Application

- Log identifiers in `L#########` format (generated on store)
- Creation timestamp in ISO 8601 round-trip format (generated on store)
- Update timestamp in ISO 8601 round-trip format (generated on update)
- Structured access and operation logs via NuciLog, including operation name, status, and selected request fields (template, date, time, time zone, localisation, count, identifier)

### Data Received from Integrations

No personal data is received from integrations or third parties. The application uses NuciAPI for request authentication, NuciLog for structured logging, and NuciDAL for JSON file persistence; these are libraries that operate within the application process and do not transmit data externally.

## 🧭 Processing and Use

The application processes the data described above for these verified functions:
- Store personal log — API key, date, time, time zone, template, data dictionary, generated identifier, creation timestamp
- Query personal logs — API key, date, time, template, localisation, count, data filters; returns rendered natural-language strings and result count
- Retrieve personal log by identifier — API key, identifier; returns structured record with all stored fields
- Update personal log — API key, identifier, optional time, optional data dictionary merge; generates update timestamp
- Delete personal log — API key, identifier
- Render logs as English or Romanian natural-language sentences — template, data dictionary, localisation

## 🗄️ Storage, Retention, and Deletion

Log records are persisted as a JSON array at the configured `logStorePath` (default: `Data/logs.json`). Each record contains the identifier, date, optional time and time zone, template, arbitrary data dictionary, creation timestamp, and optional update timestamp. The directory and an empty store are created on startup when absent.

Structured application logs are written to the configured `logFilePath` (default: `logfile.log`) when file output is enabled.

The application defines no automatic retention period, archival, backup, or schema migration. Deletion occurs only when a client invokes the delete operation for a specific identifier. For self-hosted deployments, the instance operator controls the JSON store file, log files, backups, and any retention or deletion policies applied to them.

## 🔗 External Processing and Integrations

The application has no built-in external data transfer. All processing occurs within the application process. The following libraries are used internally and do not transmit data externally:

| Service or integration | Purpose | Data involved | Configuration or documentation |
|-----------------------|---------|---------------|--------------------------------|
| NuciAPI | HMAC-based request authentication | API key, request fields for HMAC verification | https://github.com/hmlendea/NuciAPI |
| NuciLog | Structured application logging | Operation name, status, selected request fields, exception details | https://github.com/hmlendea/NuciLog |
| NuciDAL | JSON file repository persistence | Log entities (identifier, date, time, time zone, template, data, timestamps) | https://github.com/hmlendea/NuciDAL |

## ⚙️ User Controls and Requests

The following documented controls or request procedures are available:
- Delete a personal log by identifier — `DELETE /PersonalLog/{id}` with valid API key
- Update a personal log by identifier — `PUT /PersonalLog/{id}` with valid API key and optional fields
- Query personal logs with filters — `GET /PersonalLog` with valid API key and optional query parameters
- Retrieve a personal log by identifier — `GET /PersonalLog/{id}` with valid API key

## 🛡️ Data Protection and Security

- Authentication: All controller operations require a valid API key verified through NuciAPI HMAC authentication.
- Transport: The application uses ASP.NET Core Kestrel; operators should configure TLS termination via a reverse proxy for production deployments.
- Storage: Log data is stored in a local JSON file with file-system permissions controlled by the operator. Log files are written to a local path controlled by the operator.
- Access control: The API key is the sole access control mechanism; operators must protect the configured key and rotate it as needed.
- Logging: Structured logs include request fields but not the API key or full request bodies. Operators should protect log file access.
- Secrets: The API key is loaded from configuration; operators should use environment variables or secret managers rather than committing keys to source control.
- Updates: The application has no automatic update mechanism; operators are responsible for applying updates and security patches.

The original maintainers are not responsible for changes, modifications, or privacy issues introduced by downstream forks.

## 🔄 Document Changes

Update this document when application data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/personal-log-manager/blob/main/PRIVACY.md.

## 📬 Contact

For questions about application data handling, contact the project maintainers. For a self-hosted instance, contact the instance operator, unless the project explicitly handles the request. Include the deployment identifier or repository URL if applicable; do not send passwords, access tokens, or other secrets.