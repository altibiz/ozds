using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Composite;

public record CalculationItemBasisModel(
  List<IAggregate> Aggregates,
  decimal Price
);
