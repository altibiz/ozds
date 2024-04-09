using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record NetworkUserNetworkUserCalculationBasisModel(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  List<AggregateModel> Aggregates,
  LocationModel Location,
  NetworkUserModel NetworkUser,
  NetworkUserMeasurementLocationModel MeasurementLocation,
  CatalogueModel Catalogue,
  MeterModel Meter
);
