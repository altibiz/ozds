using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record LocationNetworkUserCalculationBasisModel(
  DateTimeOffset FromDate,
  DateTimeOffset ToDate,
  LocationModel Location,
  LocationMeasurementLocationModel MeasurementLocation,
  INetworkUserCatalogue NetworkUserCatalogue,
  IMeter Meter,
  List<IAggregate> Aggregates
);
