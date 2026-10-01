# Invariants And Rules

## Metadata
- Purpose: Collect rules that future changes must preserve or intentionally revise.
- Scope: Identifiers, transport validation, filtering, ordering, mapping, persistence, rendering, security, and deployment assumptions.
- Primary source areas: service, controller, models, mapping, text factory, startup, tests.
- Related documents: [Change Guide](change-guide.md), [Data Model](data-model.md), [Error Handling](error-handling.md), [State And Persistence](state-and-persistence.md).

## Rules

| Invariant | Established by | Depended upon | Violation consequence |
|-----------|----------------|---------------|-----------------------|
| Create requests contain Date | `StoreLogRequest` validation | Controller/service | Bad request |
| Query Count is `1..100000` | `GetLogRequest` validation | Query service | Bad request |
| Stored generated ids use `L` plus nine digits | `GenerateUniqueLogId` | Clients and logs | Identifier contract changes |
| Route id controls update target | Controller assignment | Update service | Wrong record could mutate |
| Query data filters are conjunctive | `FilterByRequestData` loop | API consumers | Result set changes |
| Field filters match complete strings | `DoesFieldMatch` | Query consumers | Query semantics change |
| Query order is date/time descending, template/created ascending | LINQ ordering | Client expectations | Result ordering changes |
| Enum templates have matching builder methods | Factory reflection | Query rendering | Missing method failure |
| Mutations call `SaveChanges` | Service methods | Persistent state | In-memory changes may not persist |
| Update preserves omitted scalar fields and dictionary keys | Update service | Partial-update clients | Data loss or contract change |
| API operations use configured API key policy | Controller and NuciAPI | All clients | Unauthorised access or incompatibility |
| Store exists before request handling | Startup | Repository | Startup/request failure |

## Additional Constraints
Date and template strings are not validated at create time. `UpdatedDT` changes on every update, including updates with no effective field change. Null stored data becomes an empty dictionary only in structured retrieval and base helper access varies by method. Identifier probing is not atomic across concurrent writers. These are current constraints, not guarantees of a stronger design.
