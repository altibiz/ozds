using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public record CalculationItemBasisModel(
  List<AggregateModel> Aggregates,
  decimal Price
);
