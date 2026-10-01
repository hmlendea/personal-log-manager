# Query And Rendering Behaviour

## Metadata
- Purpose: Preserve the detailed selection, ordering, mapping, localisation, and template dispatch algorithm.
- Scope: `GetPersonalLogs`, filter helpers, mapping, text factory, base helpers, and concrete builders.
- Primary source areas: `PersonalLogService.cs`, `PersonalLogMappingExtensions.cs`, `PersonalLogTextBuilderFactory.cs`, `PersonalLogTextBuilderBase.cs`, localisation builders.
- Related documents: [Text Building Component](../components/text-building.md), [Data Model](../data-model.md), [Log Capability](log-capability.md), [Error Handling](../error-handling.md).

## Execution Sequence
1. The controller binds `GetLogRequest`; `Count` defaults to 1 and `Localisation` to `en`.
2. The service loads every `PersonalLogEntity` using `repository.GetAll()`.
3. Non-empty Date, Time, and Template filters are applied with `DoesFieldMatch`.
4. `FilterByRequestData` applies one case-insensitive anchored match per requested dictionary pair.
5. A success debug event records the number of matches before ordering and `Take`.
6. Results are ordered by Date descending, Time descending, Template ascending, and CreatedDT ascending.
7. The first `Count` entities are passed to `BuildLogTexts`.
8. Each entity is converted by `ToDomainModel`; parsing includes `DateOnly.Parse` and `Enum.Parse<PersonalLogTemplate>`.
9. The factory selects Romanian for `ro`, `ro-RO`, or `ro-MD`; every other value selects English.
10. The factory prefixes the sentence with date and optional time/time zone, dispatches `Build{Template}LogText`, and normalises the sentence.
11. The service prefixes the generated sentence with the record id and returns `GetLogResponse`.

## Matching Rules
The helper adds `^` at the start and `$` at the end of a pattern if absent. Existing anchors are retained. Date, time, and template matching uses default regex options, which makes it case-sensitive. Data value matching uses `RegexOptions.IgnoreCase`. Null input or pattern returns false. Regex syntax errors are not caught locally.

## Rendering Rules
The factory dispatches by reflection, so enum names and public builder method names are a runtime contract. Builder methods retrieve values through obfuscator-backed helpers. The base class supports fallback discriminator keys, mapped values with normalised key spellings, amount/currency fallbacks, plural detection, and basic English/Romanian conjunction conversion.

## Failure Path
If a record cannot map or render, `BuildLogTexts` wraps the exception in `InvalidOperationException` and includes the last processed id. The service catches that failure in its outer query try/catch, logs failure, and rethrows. NuciAPI exception middleware determines the HTTP representation.

## Performance And Ordering
The repository is fully materialised before filtering, and text is generated only after sorting and count limiting. This avoids rendering excluded records but still makes query memory and CPU proportional to the complete store. Created timestamps are compared as strings, relying on the round-trip format's lexical ordering.
