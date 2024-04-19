using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record MeterTableViewModel(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  MeasurementLocationModel MeasurementLocation,
  MeterModel Meter,
  IEnumerable<AggregateModel> Aggregates
);
