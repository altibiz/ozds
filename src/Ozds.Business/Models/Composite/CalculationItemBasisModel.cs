using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Composite;

public class CalculationItemBasisModel : IComposite
{
  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public DateTimeOffset MeasuredFromDate { get; set; }

  public DateTimeOffset MeasuredToDate { get; set; }

  public DateTimeOffset BilledFromDate { get; set; }

  public DateTimeOffset BilledToDate { get; set; }

  public List<AggregateModel> Aggregates { get; set; } = default!;

  public decimal Price_EUR { get; set; }
}
