using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record NetworkUserCalculationBasisModel(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<AggregateModel> Aggregates,
  LocationModel Location,
  NetworkUserModel NetworkUser,
  NetworkUserMeasurementLocationModel MeasurementLocation,
  NetworkUserCatalogueModel UsageNetworkUserCatalogue,
  RegulatoryCatalogueModel SupplyRegulatoryCatalogue,
  MeterModel Meter
);
