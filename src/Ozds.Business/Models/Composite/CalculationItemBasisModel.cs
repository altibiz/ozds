using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class CalculationItemBasisModel : IComposite
{
  public List<AggregateModel> Aggregates { get; set; } = default!;

  public decimal Price_EUR { get; set; }
}
