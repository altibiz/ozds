using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record AnalysisBasisEntity(
  LocationEntity Location,
  NetworkUserEntity? NetworkUser,
  MeasurementLocationEntity MeasurementLocation,
  MeterEntity Meter,
  List<CalculationEntity> Calculations,
  List<InvoiceEntity> Invoices,
  MeasurementEntity LastMeasurement,
  List<AggregateEntity> MonthlyAggregates
);
