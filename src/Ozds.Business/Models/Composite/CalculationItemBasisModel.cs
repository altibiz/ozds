using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class CalculationItemBasisModel : IComposite
{
  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public List<AggregateModel> Aggregates { get; set; } = default!;

  public decimal Price_EUR { get; set; }
}
