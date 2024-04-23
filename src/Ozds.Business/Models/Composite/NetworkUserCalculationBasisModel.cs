using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record NetworkUserCalculationBasisModel(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<IAggregate> Aggregates,
  LocationModel Location,
  NetworkUserModel NetworkUser,
  NetworkUserMeasurementLocationModel MeasurementLocation,
  INetworkUserCatalogue UsageNetworkUserCatalogue,
  RegulatoryCatalogueModel SupplyRegulatoryCatalogue,
  IMeter Meter
);
