using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record LocationCalculationBasisModel(
  LocationModel Location,
  LocationMeasurementLocationModel MeasurementLocation,
  CatalogueModel Catalogue,
  MeterModel Meter,
  List<AggregateModel> Aggregates
);
