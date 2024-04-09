using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record LocationNetworkUserCalculationBasisModel(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  LocationModel Location,
  LocationMeasurementLocationModel MeasurementLocation,
  CatalogueModel Catalogue,
  MeterModel Meter,
  List<AggregateModel> Aggregates
);
