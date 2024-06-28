# Ozds.Data

Ovaj projekt sadrži sve entitete i upite za bazu podataka. Entiteti se nalaze u
imenskom prostoru `Entities`, a upiti su funkcije u klasi `OzdsDbClient`. Klasa
je djelomična i podijeljena je u konceptualno slične datoteke (npr. svi upiti za
brojila su u datoteci OzdsDbClientMeters.cs).

## Shema

[The schema](../../schema.md) se generira korištenjem `mermerd` kada se migrira
na najnoviju migraciju generiranu pomoću `dotnet ef`.

## Ozds.Data.Entities

Sadrži entitete koji se koriste za predstavljanje tablica u bazi podataka.

Postoji nekoliko marker sučelja koja se koriste za implementaciju određene
funkcionalnosti ili presretača u `Ozds.Business`:

- Readonly: marker sučelje koje se koristi za implementaciju entiteta samo za
  čitanje bacanjem iznimki pri mutacijama

```plantuml
@startuml

interface IReadonly

@enduml
```

- Aggregate: marker sučelje koje se koristi za implementaciju agregacije
  mjerenja. Za sada se mjerenja agregiraju u četvrt-satnim, dnevnim i mjesečnim
  intervalima.

```plantuml
@startuml

interface IReadonly

enum IntervalEntity
{
  QuarterHourly
  Daily
  Monthly
}

interface IAggregateEntity
{
  + DateTimeOffset Timestamp
  + long Count
  + IntervalEntity Interval
  + string MeterId
}

IReadonly <|-- IAggregateEntity
IAggregateEntity *-- IntervalEntity

@enduml
```

- Measurement: marker sučelje koje se koristi za implementaciju agregacija
  mjerenja.

```plantuml
@startuml

interface IMeasurementEntity
{
  + DateTimeOffset Timestamp
  + string MeterId
}

@enduml
```

Osim marker sučelja, entiteti se mogu grupirati u nekoliko hijerarhija klasa.
Virtualna svojstva na klasama entiteta su navigacijska svojstva prema drugim
entitetima s kojima imaju odnos i nisu pohranjena u bazi podataka. Ove
hijerarhije olakšavaju grupiranje entiteta koji imaju slične mape prema
tablicama baze podataka:

- Auditable: entiteti koji se mogu revidirati. Ovi entiteti također imaju
  funkcionalnost mekog brisanja implementiranu putem presretača.

```plantuml
@startuml

abstract class AuditableEntity
{
  + DateTimeOffset CreatedOn
  + string CreatedById
  + <<virtual>> RepresentativeEntity CreatedBy
  + DateTimeOffset? LastUpdatedOn
  + string? LastUpdatedById
  + <<virtual>> RepresentativeEntity? LastUpdatedBy
  + bool IsDeleted
  + DateTimeOffset? DeletedOn
  + string? DeletedById
  + <<virtual>> RepresentativeEntity? DeletedBy
  + string Id
  + string Title
}

@enduml
```

- Event: entiteti koji predstavljaju događaje.

```plantuml
@startuml

abstract class EventEntity
{
  + string Id
  + string Title
  + DateTimeOffset Timestamp
  + LevelEntity Level
  + string Description
}

enum LevelEntity
{
  Trace
  Debug
  Information
  Warning
  Error
  Critical
}

EventEntity *-- "1" LevelEntity

@enduml
```

- Measurements: entiteti koji predstavljaju mjerenja. Konkreti tipovi mjerenja
  imaju navigacijska svojstva prema specifičnim tipovima brojila.

```plantuml
@startuml

abstract class MeasurementEntity
{
  + DateTimeOffset Timestamp
  + string MeterId
}

@enduml
```

- Aggregates: entiteti koji predstavljaju agregirana mjerenja. Vremenski žig je
  početak intervala, a broj je broj mjerenja u tom intervalu. Konkreti tipovi
  agregata imaju navigacijska svojstva prema specifičnim tipovima brojila.

```plantuml
@startuml

abstract class AggregateEntity
{
  + DateTimeOffset Timestamp
  + long Count
  + IntervalEntity Interval
  + string MeterId
}

enum IntervalEntity
{
  QuarterHourly
  Daily
  Monthly
}

AggregateEntity *-- "1" IntervalEntity

@enduml
```

- Calculations: entiteti koji predstavljaju izračune. Izračuni koji nemaju
  izdavatelja su automatski izdani od strane poslužitelja.

```plantuml
@startuml

abstract class CalculationEntity
{
  + string Id
  + string Title
  + DateTimeOffset IssuedOn
  + string? IssuedById
  + <<virtual>> RepresentativeEntity? IssuedBy
  + DateTimeOffset FromDate
  + DateTimeOffset ToDate
  + string MeterId
  + <<virtual>> MeterEntity Meter
  + decimal Total_EUR
  + MeterEntity ArchivedMeter
}

@enduml
```

- Invoices: entiteti koji predstavljaju račune.

```plantuml
@startuml

abstract class InvoiceEntity
{
  + string Id
  + string Title
  + DateTimeOffset IssuedOn
  + string? IssuedById
  + <<virtual>> RepresentativeEntity? IssuedBy
  + DateTimeOffset FromDate
  + DateTimeOffset ToDate
  + decimal Total_EUR
  + decimal Tax_EUR
  + decimal TotalWithTax_EUR
}

@enduml
```

## Ozds.Data.Migrations

Ovaj imenski prostor sadrži generirane migracije za bazu podataka. Migracije se
generiraju korištenjem `dotnet ef` i nalaze se u direktoriju `Migrations`.
