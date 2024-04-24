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

This namespace contains client-side and server-side aggregation upsert logic.
The server-side logic is used to tell the database how to upsert aggregates via
LINQ Expressions over database entities and the client-side logic mimics this as
a method over business models . If we were just to use the server-side logic,
the database would complain that it couldn't upsert the same aggregate twice in
the same transaction. This is why we have to upsert first on client-side the
aggregates that are to be inserted and then upsert the aggregates that are on
the server.

```plantuml
class AgnosticAggregateUpserter
{
  + Expression<Func<TEntity, TEntity, TEntity>>UpsertEntity<TEntity>()
  + LambdaExpression UpsertEntityAgnostic(Type entityType)
  + TModel UpsertModel<TModel>(TModel lhs, TModel rhs)
  + IAggregate UpsertModelAgnostic(IAggregate lhs, IAggregate rhs)
}

interface IAggregateUpserter
{
  + LambdaExpression UpsertEntity

  + bool CanUpsertModel(Type modelType)
  + bool CanUpsertEntity(Type entityType)
  + IAggregate UpsertModel(IAggregate lhs, IAggregate rhs)
}

AgnosticAggregateUpserter o-- "0..N" IAggregateUpserter
```

## `Ozds.Business.Capabilities`

This namespace is a WIP but will be used to detect which meter measures which
measures.

## `Ozds.Business.Conversion`

This namespace contains converters for:

- Database entities &harr; business models

```plantuml
class AgnosticModelEntityConverter
{
  + TEntity ToEntity<TEntity>(object model)
  + TModel ToModel<TModel>(object entity)
  + object ToEntity(object model)
  + object ToModel(object entity)
  + Type EntityType()
}

interface IModelEntityConverter
{
  + bool CanConvertToEntity(Type modelType)
  + bool CanConvertToModel(Type entityType)
  + object ToEntity(object model)
  + object ToModel(object entity)
  + Type EntityType()
}

AgnosticModelEntityConverter o-- "0..N" IModelEntityConverter
```

- Push requests &harr; measurement models

```plantuml
class AgnosticPushRequestMeasurementConverter
{
  + IMeasurement ToMeasurement( \
    JsonObject pushRequest, \
    string meterId, \
    DateTimeOffset timestamp \
  )
  + TMeasurement ToMeasurement<TMeasurement>a(
    JsonObject pushRequest, \
    string meterId, \
    DateTimeOffset timestamp \
  )
  + JsonObject ToPushRequest(IMeasurement measurement)
}

interface IPushRequestMeasurementConverter
{
  + bool CanConvert(string meterId)
  + IMeasurement ToMeasurement( \
    JsonObject pushRequest, \
    string meterId, \
    DateTimeOffset timestamp \
  )
  + JsonObject ToPushRequest(IMeasurement measurement)
}

AgnosticPushRequestMeasurementConverter o-- "0..N" \
  IPushRequestMeasurementConverter
```

- Measurement models &rarr; aggregate models

```plantuml
interface IMeasurementAggregateConverter
{
  + bool CanConvertToAggregate(Type measurementType)
  + IAggregate ToAggregate(IMeasurement measurement, IntervalModel interval)
}

class AgnosticMeasurementAggregateConverter
{
  + IAggregate ToAggregate(IMeasurement measurement, IntervalModel interval)
  + TAggregate ToAggregate<TAggregate>( \
    IMeasurement measurement, \
    IntervalModel interval \
  )
}

AgnosticMeasurementAggregateConverter o-- "0..N" IMeasurementAggregateConverter
```

## `Ozds.Business.Finance`

This namespace contains billing logic. There are three levels of billing
calculations needed for every invoice:

- Invoice level: this is the top level corresponding to a network user or
  location and uses lower levels to calculate the totals and subtotals on an
  invoice

```plantuml
class NetworkUserInvoiceCalculator
{
  + CalculatedNetworkUserInvoiceModel Calculate(NetworkUserInvoiceCalculationBasisModel basis)
}
```

- Calculation level: each invoice has a set of calculation corresponding to a
  measurement location and uses the lowest level to calculate the totals and
  subtotals of a particular calculation

```plantuml
class AgnosticNetworkUserCalculationCalculator
{
  + INetworkUserCalculation Calculate(NetworkUserCalculationBasisModel basis)
  + TCalculation Calculate<TCalculation>(NetworkUserCalculationBasisModel basis)
}

interface INetworkUserCalculationCalculator
{
  + bool CanCalculate(NetworkUserCalculationBasisModel basis)
  + INetworkUserCalculation Calculate(NetworkUserCalculationBasisModel basis)
}

AgnosticNetworkUserCalculationCalculator o-- "0..N" \
  INetworkUserCalculationCalculator
```

- Calculation item level: each calculation has a set of calculation items
  corresponding to a certain billing item and calculates the amounts and totals
  of a particular billing item

```plantuml
class AgnosticCalculationItemCalculator
{
  + ICalculationItem Calculate(CalculationItemBasisModel basis)
  + TCalculationItem Calculate<TCalculationItem>( \
    CalculationItemBasisModel basis \
  )
}

interface ICalculationItemCalculator
{
  + bool CanCalculate(Type calculationItemType)
  + ICalculationItem Calculate(CalculationItemBasisModel basis)
}

AgnosticCalculationItemCalculator o-- "0..N" ICalculationItemCalculator
```

For now, only network user invoice calculation is implemented.

```plantuml
class NetworkUserInvoiceCalculator

class AgnosticNetworkUserCalculationCalculator

interface INetworkUserCalculationCalculator

class AgnosticCalculationItemCalculator

interface ICalculationItemCalculator

NetworkUserInvoiceCalculator o-- "1" AgnosticNetworkUserCalculationCalculator
AgnosticNetworkUserCalculationCalculator o-- "0..N" INetworkUserCalculationCalculator
INetworkUserCalculationCalculator o-- "1" AgnosticCalculationItemCalculator
AgnosticCalculationItemCalculator o-- "0..N" ICalculationItemCalculator
```

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

```plantuml
left to right direction

class ServedSaveChangesInterceptors
{
  # IServiceProvider _serviceProvider
}

class AggregateCreationInterceptor
class AuditingInterceptor
class CascadingSoftDeleteInterceptor
class InvoiceIssuingInterceptor
class ReadonlyInterceptor

SaveChangesInterceptor <|-- ServedSaveChangesInterceptors
ServedSaveChangesInterceptors <|-- AggregateCreationInterceptor
ServedSaveChangesInterceptors <|-- AuditingInterceptor
ServedSaveChangesInterceptors <|-- CascadingSoftDeleteInterceptor
ServedSaveChangesInterceptors <|-- InvoiceIssuingInterceptor
ServedSaveChangesInterceptors <|-- ReadonlyInterceptor
```

## `Ozds.Business.Iot`

Contains logic for handling IoT requests. It only handles pushing for now
(`OzdsIotHandler`), but it will be split into a `Push` and `Poll` interface once
we get around to implement polling.

Pushing is the process of IoT devices sending measurements to the server. It is
implemented via a REST API that the IoT devices can call. The IoT devices send
measurements which then get aggregated and stored in the database.

```plantuml
class OzdsIotHandler
{
  + Task OnPush(string id, string request)
}

class MessengerPushRequest
{
  DateTimeOffset Timestamp
  MessengerPushRequestMeasurement[] Measurements
}

class MessengerPushRequestMeasurement
{
  string MeterId
  DateTimeOffset Timestamp
  JsonObject Data
}

class AbbB2xPushRequest
class SchneideriEM3xxxPushRequest

MessengerPushRequestMeasurement *-- "0..N Data" AbbB2xPushRequest
MessengerPushRequestMeasurement *-- "0..N Data" SchneideriEM3xxxPushRequest
MessengerPushRequest *-- "0..N" MessengerPushRequestMeasurement
OzdsIotHandler -- "processes" MessengerPushRequest
```

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

- Phase: a measure can be a single phase or triphasic measure.

```plantuml
class PhasicMeasure<T>
{
  + {static} PhasicMeasure<T> Null

  + T PhaseSum
  + T PhaseAverage
  + T PhasePeak
  + T PhaseTrough
  + SinglePhasicMeasure<T> PhaseSingle
  + TriphasicMeasure<T> PhaseSplit
  + PhasicMeasure<T> PhaseAbs

  + PhasicMeasure<U> ConvertPrimitiveTo<U>()
}

class CompositePhasicMeasure<T>
{
  + List<PhasicMeasure<T>> Measures
}

class TriPhasicMeasure<T>
{
  + T ValueL1
  + T ValueL2
  + T ValueL3
}

class SinglePhasicMeasure<T>
{
  + T Value
}

class NullPhasicMeasure<T>

CompositePhasicMeasure *-- "0..N" PhasicMeasure

PhasicMeasure <|-- SinglePhasicMeasure
PhasicMeasure <|-- TriPhasicMeasure
PhasicMeasure <|-- NullPhasicMeasure
PhasicMeasure <|-- CompositePhasicMeasure
```

- Direction: a measure can be an import or export measure and these correspond
  to user consumption and production. It can also be any duplex measure if it is
  a measure of current or voltage since these are not directional.

```plantuml
class DuplexMeasure<T>
{
  + {static} DuplexMeasure<T> Null

  + PhasicMeasure<T> DuplexNet
  + PhasicMeasure<T> DuplexAny
  + PhasicMeasure<T> DuplexImport
  + PhasicMeasure<T> DuplexExport
  + DuplexMeasure<T> DuplexAbs
  + DuplexMeasure<T> DuplexSum

  + DuplexMeasure<U> ConvertPrimitiveTo<U>()
}

class CompositeDuplexMeasure<T>
{
  + List<DuplexMeasure<T>> Measures
}

class NullDuplexMeasure<T>

class ImportExportDuplexMeasure<T>
{
  + PhasicMeasure<T> Import
  + PhasicMeasure<T> Export
}

class AnyDuplexMeasure<T>
{
  + PhasicMeasure<T> Value
}

class NetDuplexMeasure<T>
{
  + PhasicMeasure<T> TrueNet
}

CompositeDuplexMeasure *-- "0..N" DuplexMeasure

DuplexMeasure <|-- ImportExportDuplexMeasure
DuplexMeasure <|-- AnyDuplexMeasure
DuplexMeasure <|-- NetDuplexMeasure
DuplexMeasure <|-- NullDuplexMeasure
DuplexMeasure <|-- CompositeDuplexMeasure
```

- Tariff: a measure can be a high tariff and a low tariff or a single tariff
  measure. This is used to calculate the cost of the measure depending on the
  time of day.

```plantuml
class TariffMeasure<T>
{
  + {static} TariffMeasure<T> Null

  + DuplexMeasure<T> TariffUnary
  + BinaryTariffMeasure<T> TariffBinary
  + TariffMeasure<T> TariffAbs
  + DuplexMeasure<T> TariffSum

  + TariffMeasure<U> ConvertPrimitiveTo<U>()
}

class CompositeTariffMeasure<T>
{
  + List<TariffMeasure<T>> Measures
}

class BinaryTariffMeasure<T>
{
  + DuplexMeasure<T> T1
  + DuplexMeasure<T> T2
}

class NullTariffMeasure<T>

class UnaryTariffMeasure<T>
{
  + DuplexMeasure<T> T0
}

CompositeTariffMeasure *-- "0..N" TariffMeasure

TariffMeasure <|-- UnaryTariffMeasure
TariffMeasure <|-- BinaryTariffMeasure
TariffMeasure <|-- NullTariffMeasure
TariffMeasure <|-- CompositeTariffMeasure
```

The measure structure is such that the tariff hierarchy classes contain
directional hierarchy classes which contain phase hierarchy classes. All three
of these class hierarchies also contain a null class which is used to represent
a measure that is not set. These hierarchies also contain a composite class
which is used to represent a measure that is a combination of two or more
measures. This is used to represent the different ways in which a measure is
stored or calculated for better accuracy. For example, we might store a measure
as three phases but also as a single phase and for some calculations one is more
accurate than the other.

There are two more top-level class hierarchies:

- Span: a measure can be a measure over a certain time span. This is used to
  calculate the costs over a span of time.

```plantuml
class SpanningMeasure<T>
{
  + {static} SpanningMeasure<T> Null

  + TariffMeasure<T> SpanMin
  + TariffMeasure<T> SpanMax
  + TariffMeasure<T> SpanAvg
  + TariffMeasure<T> SpanPeak
  + TariffMeasure<T> SpanDiff

  + SpanningMeasure<U> ConvertPrimitiveTo<U>()
}

class NullSpanningMeasure<T>

class MinMaxSpanningMeasure<T>
{
  + TariffMeasure<T> TrueMin
  + TariffMeasure<T> TrueMax
}

class AvgSpanningMeasure<T>
{
  + TariffMeasure<T> TrueAvg
}

class PeakSpanningMeasure<T>
{
  + TariffMeasure<T> TruePeak
}

SpanningMeasure <|-- MinMaxSpanningMeasure
SpanningMeasure <|-- AvgSpanningMeasure
SpanningMeasure <|-- PeakSpanningMeasure
SpanningMeasure <|-- NullSpanningMeasure
```

- Expenditure: a measure can be an amount used to calculate costs. For network
  user invoice calculations this means it can be either used to calculate supply
  or usage costs and is used to represent these two different types of costs.

```plantuml
class ExpenditureMeasure<T>
{
  + {static} ExpenditureMeasure<T> Null

  + TariffMeasure<T> ExpenditureSupply
  + TariffMeasure<T> ExpenditureUsage
  + TariffMeasure<T> ExpenditureSum

  + ExpenditureMeasure<U> ConvertPrimitiveTo<U>()
}

class NullExpenditureMeasure<T>

class SupplyExpenditureMeasure<T>
{
  + TariffMeasure<T> TrueSupply
}

class UsageExpenditureMeasure<T>
{
  + TariffMeasure<T> TrueUsage
}

class DualExpenditureMeasure<T>
{
  + TariffMeasure<T> TrueSupply
  + TariffMeasure<T> TrueUsage
}

ExpenditureMeasure <|-- SupplyExpenditureMeasure
ExpenditureMeasure <|-- UsageExpenditureMeasure
ExpenditureMeasure <|-- DualExpenditureMeasure
ExpenditureMeasure <|-- NullExpenditureMeasure
```

## `Ozds.Business.Models`

Contains business models that are used to represent entities in the database.
Entities in the database are represented as entity classes in the
`Ozds.Data.Entities` namespace which are then converted to business models in
this namespace. The reasoning is that the database entity classes have special
fields or properties that instruct Entity Framework Core how to handle database
operations that should not be exposed to the rest of the application. On the
other hand, business models have special fields and properties and implement
interfaces which should not be represented in the database.

Models are divided into a couple of class hierarchies:

- Auditable:
- Events:
- Measurements:
- Aggregates:
- Invoices:
- Network user calculations:

## `Ozds.Business.Mutations`

## `Ozds.Business.Queries`

## `Ozds.Business.Time`
