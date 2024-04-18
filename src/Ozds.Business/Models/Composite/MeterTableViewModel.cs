using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record MeterTableViewModel(
  LocationModel Location,
  NetworkUserModel NetworkUser,
  MeterModel Meter,
  List<AggregateModel> Aggregates
);
