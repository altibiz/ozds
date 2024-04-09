using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record NetworkUserCalculationBasisModel(
  List<AggregateModel> Aggregates,
  LocationModel Location,
  NetworkUserModel NetworkUser,
  NetworkUserMeasurementLocationModel MeasurementLocation,
  CatalogueModel Catalogue,
  MeterModel Meter
);
