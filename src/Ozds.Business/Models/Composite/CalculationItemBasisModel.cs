using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class CalculationItemBasisModel
{
  public List<AggregateModel> Aggregates { get; set; } = default!;

  public decimal Price { get; set; }
}
