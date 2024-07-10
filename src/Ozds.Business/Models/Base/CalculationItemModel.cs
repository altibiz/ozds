using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

#pragma warning disable S1694
public abstract class CalculationItemModel : ICalculationItem
#pragma warning restore S1694
{
  public abstract SpanningMeasure<decimal> Amount { get; }

  public abstract ExpenditureMeasure<decimal> Price { get; }

  public abstract decimal Total { get; }

  public abstract string Kind { get; }
}
