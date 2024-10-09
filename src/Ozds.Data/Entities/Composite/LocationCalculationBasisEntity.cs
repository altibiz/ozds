using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record LocationNetworkUserCalculationBasisEntity(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  LocationEntity Location,
  LocationMeasurementLocationEntity MeasurementLocation,
  MeterEntity Meter,
  List<AggregateEntity> Aggregates
);
