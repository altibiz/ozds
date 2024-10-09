using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record DetailedMeasurementLocationEntity(
  LocationEntity Location,
  NetworkUserEntity? NetworkUser,
  MeasurementLocationEntity MeasurementLocation,
  MeterEntity Meter,
  IEnumerable<MeasurementEntity> Measurements,
  IEnumerable<AggregateEntity> MonthlyAggregates
);
