using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationItemModel : ICalculationItem
{
  public abstract SpanningMeasure<decimal> Amount { get; }

  public abstract ExpenditureMeasure<decimal> Price { get; }

  public abstract string Kind { get; }
}
