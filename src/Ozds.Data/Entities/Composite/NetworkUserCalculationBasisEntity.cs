using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record NetworkUserCalculationBasisEntity(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<AggregateEntity> Aggregates,
  LocationEntity Location,
  NetworkUserEntity NetworkUser,
  NetworkUserMeasurementLocationEntity MeasurementLocation,
  NetworkUserCatalogueEntity UsageNetworkUserCatalogue,
  RegulatoryCatalogueEntity SupplyRegulatoryCatalogue,
  MeterEntity Meter
);
