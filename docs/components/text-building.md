# Text Building Component

## Metadata
- Purpose: Explain localised sentence generation and its reflection-based dispatch.
- Scope: `IPersonalLogTextBuilderFactory`, `PersonalLogTextBuilderFactory`, `PersonalLogTextBuilderBase`, English/Romanian builders, and NuciText adapters.
- Primary source areas: `PersonalLogManager/Service/TextBuilding/`.
- Related documents: [Query And Rendering](../behaviour/query-and-rendering.md), [Data Model](../data-model.md), [Cross-Cutting Concerns](../cross-cutting.md), [Design Decisions](../design-decisions.md).

## Purpose And Scope
The text-building component turns a typed `PersonalLog` into a date/time-prefixed natural-language sentence. The factory selects English for null, empty, unknown, or non-Romanian localisation values, and Romanian for `ro`, `ro-RO`, or `ro-MD`, case-insensitively.

The factory does not select records, persist data, or validate arbitrary template strings at write time. Template-specific prose belongs in the concrete language builders.

## Dispatch Algorithm
`BuildLogText` creates a `yyyy-MM-dd` prefix. When a time exists, it appends `: HH:mm {TimeZone}`. It selects a builder, forms the method name `Build{PersonalLogTemplate}LogText`, locates a public instance method with reflection, invokes it, and passes the returned sentence to `INuciTextNormaliser.NormaliseSentence`.

A missing builder method raises `MissingMethodException`. A builder exception is unwrapped from `TargetInvocationException`. The service adds the record identifier before returning query text.

## Base Helpers
`PersonalLogTextBuilderBase` centralises:

- Nuci obfuscation reversal for data values;
- fallback discriminator selection from `discriminator`, `account`, `account_id`, `username`, `phone_number`, and `email_address`;
- platform plus discriminator formatting;
- decimal parsing and formatting;
- localisation of the words `and` and `și`;
- plural detection using `&`, comma, `and`, or `și` separators;
- key presence checks;
- tolerant key mapping after removing spaces, hyphens, underscores, and converting `&` to `And`;
- balance and currency fallback selection.

Concrete builders implement language grammar and helper vocabulary. The enum and method names must remain aligned: adding a `PersonalLogTemplate` value without corresponding public methods in supported builders makes query rendering fail.

## Supported Languages
`EnglishTextBuilder` implements the English template methods. `RomanianTextBuilder` implements Romanian equivalents and language-specific helper forms. The enum contains a large set of activity templates; the source and builder methods, not this document, are authoritative for exact template coverage.

## Data And Culture Constraints
Values are strings and are deobfuscated before use. `GetDecimalValue` parses using the process culture, formats with two decimal places, and removes `.00`; `GetBalance` uses decimal parsing and may therefore vary with culture. Missing keys often produce nulls or `MissingValue`, while some direct dictionary indexers intentionally throw for required template data.
