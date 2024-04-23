using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record MeterTableViewModel(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  IMeasurementLocation MeasurementLocation,
  IMeter Meter,
  IEnumerable<IAggregate> Aggregates
);
