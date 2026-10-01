# Data Model

## Metadata
- Purpose: Distinguish transport, persistence, and typed rendering representations.
- Scope: API requests/responses, `PersonalLogEntity`, `PersonalLog`, `PersonalLogTemplate`, mapping, timestamps, and arbitrary data.
- Primary source areas: `Api/Models/`, `DataAccess/DataObjects/PersonalLogEntity.cs`, `Service/Models/`, `Service/Mapping/PersonalLogMappingExtensions.cs`.
- Related documents: [Interfaces](interfaces.md), [State And Persistence](state-and-persistence.md), [Text Building Component](components/text-building.md).

## Representations

| Representation | Owner | Key characteristics |
|----------------|-------|----------------------|
| `StoreLogRequest` | HTTP input | Required `Date`; optional strings and dictionary; inherits `NuciApiRequest` |
| `GetLogRequest` | HTTP query | Optional filters, `Localisation = "en"`, `Count = 1`, range `1..100000` |
| `UpdateLogRequest` | HTTP input | Route-assigned `Identifier`; null means preserve scalar field; dictionary means merge |
| `PersonalLogEntity` | JSON persistence | String date/time/template/timestamps and arbitrary string dictionary |
| `PersonalLog` | Rendering domain | `DateOnly`, nullable `TimeOnly`, enum template, timestamps, and data dictionary |
| `GetLogByIdResponse` | HTTP output | Structured record, null update timestamp permitted, null data normalised to empty dictionary |
| `GetLogResponse` | HTTP output | Rendered strings and computed result count |

## Persistence Shape
A stored object has an inherited `Id` plus `Date`, `Time`, `TimeZone`, `CreatedDT`, `UpdatedDT`, `Template`, and `Data`. The default store is an array of these objects. No repository-local migration or schema version exists.

`StoreLogRequest` values are copied directly into a new entity. `DateTime.UtcNow.ToString("o")` supplies `CreatedDT`; `UpdatedDT` is initially null. Update sets `UpdatedDT` using the same round-trip format. Omitted create values remain null except `Data` may remain null in storage; retrieval converts null data to an empty dictionary in the structured response.

## Mapping Rules
`ToDomainModel` parses `Date` with `DateOnly.Parse`, attempts `TimeOnly.TryParse` and `DateTime.TryParse` for optional values, parses `Template` with `Enum.Parse<PersonalLogTemplate>`, preserves the entity identifier and data, and passes the time zone through the `PersonalLog` constructor. `ToDataObject` formats date as `yyyy-MM-dd`, time as `HH:mm`, template as the enum name, and timestamps as round-trip strings.

The application query path uses `ToDomainModel`; identifier retrieval does not and copies strings directly into `GetLogByIdResponse`. Consequently, query rendering is sensitive to parseable dates, enum names, and supported builder methods, while structured retrieval can return the stored strings.

## Template And Data Semantics
`PersonalLogTemplate` is a large enum covering account, health, household, media, work, device, finance, pet, travel, and other activities. The `Data` dictionary is intentionally open-ended. Template builders interpret well-known keys such as `text`, `platform`, `discriminator`, `amount`, `currency`, `request_id`, and template-specific values. There is no central schema or validation for those keys.

## Defaults And Nullability
The API model defaults only `GetLogRequest.Localisation` and `Count`. `PersonalLog` constructors default a null time zone to `UTC`, but service-created entities copy the request time zone directly, so omitted API time zone values remain null in persistence and structured retrieval. This distinction is current behaviour and must not be collapsed without a compatibility decision.
