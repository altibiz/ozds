using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record CalculationBasisModel(
  NetworkUserModel NetworkUser,
  NetworkUserMeasurementLocationModel MeasurementLocation,
  IMeter Meter,
  List<IAggregate> Aggregates
);
