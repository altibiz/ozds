# `Ozds.Business`

This project contains all of the backend logic of the server. Critical functions
in this project is tested in the `Ozds.Business.Test` project. Logic is divided
into namespaces conceptually similar to each other (ie. all logic regarding
conversion is in the Conversion namespace).

For each namespace an `Abstractions` namespace is present which contains
interfaces that should be used in consuming code. If there is a `Agnostic`
namespace it should be used instead because it automatically handles logic for
picking the right implementation of the interface. For example, the
`AgnosticModelEntityConverter` will pick the right implementation of
`IModelEntityConverter` based on the type of the entity or model.

## `Ozds.Business.Aggregation`

This namespace contains client-side and server-side aggregation upsert logic via
(`AgnosticAggregateUpserter`). The server-side logic is used to tell the
database how to upsert aggregates via LINQ Expressions over database entities
and the client-side logic mimics this as a method over business models . If we
were just to use the server-side logic, the database would complain that it
couldn't upsert the same aggregate twice in the same transaction. This is why we
have to upsert first on client-side the aggregates that are to be inserted and
then upsert the aggregates that are on the server.

## `Ozds.Business.Capabilities`

This namespace is a WIP but will be used to detect which meter measures which
measures.

## `Ozds.Business.Conversion`

This namespace contains converters for:

- Database entities <-> business models (`AgnosticModelEntityConverter`)
- Push requests -> measurement models
  (`AgnosticPushRequestMeasurementConverter`)
- Measurement models -> aggregate models
  (`AgnosticMeasurementAggregateConverter`)

```plantuml
class AgnosticModelEntityConverter

interface IModelEntityConverter

AgnosticModelEntityConverter --> IModelEntityConverter
```

## `Ozds.Business.Finance`

This namespace contains billing logic. There are three levels of billing
calculations needed for every invoice:

- Invoice level: this is the top level corresponding to a network user or
  location and uses lower levels to calculate the totals and subtotals on an
  invoice (`NetworkUserInvoiceCalculator`)
- Calculation level: each invoice has a set of calculation corresponding to a
  measurement location and uses the lowest level to calculate the totals and
  subtotals of a particular calculation
  (`AgnosticNetworkUserCalculationCalculator`)
- Calculation item level: each calculation has a set of calculation items
  corresponding to a certain billing item and calculates the amounts and totals
  of a particular billing item (`AgnosticCalculationItemCalculator`)

For now, only network user invoice calculation is implemented.

## `Ozds.Business.Interceptors`

This namespace contains interceptors to any request sent to the database and
implement various business logic:

- `AggregateCreationInterceptor`: intercepts any request to the database
  containing measurements and creates aggregates for them.
- `AuditingInterceptor`: intercepts any request to the database that mutates
  entities that are auditable, mutates their audit fields and creates an audit
  event depending on the type of mutation done on the entity.
- `CascadingSoftDeleteInterceptor`: this interceptor is a WIP but it is meant to
  implement soft delete logic for auditable entities.
- `InvoiceIssuingInterceptor`: intercepts any invoice creation request and
  mutates the issuing fields on that invoice.
- `ReadonlyInterceptor`: throws an exception any time an attempt to mutate a
  readonly entity is made.
- `ServedSaveChangesInterceptors`: this is a base type for interceptors that is
  hooked up to provide inheritors with a `IServiceProvider`.

## `Ozds.Business.Iot`

Contains logic for handling IoT requests. It only handles pushing for now
(`OzdsIotHandler`), but it will be split into a `Push` and `Poll` interface once
we get around to implement polling.

Pushing is the process of IoT devices sending measurements to the server. It is
implemented via a REST API that the IoT devices can call. The IoT devices send
measurements which then get aggregated and stored in the database.

Polling is the process of IoT devices asking the server for newly updated
configuration. This way we bypass the need to send anything to IoT devices which
is problematic in todays internet because of technologies like CGNAT. It is
meant to be implemented as a REST API that the IoT devices can call. The IoT
devices asks for updated configuration and the server responds with the updated
configuration from the database.

## `Ozds.Business.Math`

Contains logic for manipulating electrical measures. This is a critical part of
the application that much of the application depends on and is tested
thoroughly.

There are three different dimensions each measure can have:

- `PhaseMeasure`: a measure can be a single phase or three phases.
- `DuplexMeasure`: a measure can be an import or export and these correspond to
  user consumption and production. It can also be any if it is a measure of
  current or voltage since these are not directional.
- `TariffMeasure`: a measure can be a high tariff and a low tariff or a unary
  tariff. This is used to calculate the cost of the measure depending on the
  time of day.

The measure structure is such that `TariffMeasure` contains `DuplexMeasure`
which contains `PhaseMeasure`. All three of these class hierarchies also contain
a `Null` class which is used to represent a measure that is not set. These
hierarchies also contain a composite class which is used to represent a measure
that is a combination of two or more measures. This is used to represent the
different ways in which a measure is stored or calculated for better accuracy.
For example, we might store a measure as three phases but also as a single phase
and for some calculations one is more accurate than the other.
