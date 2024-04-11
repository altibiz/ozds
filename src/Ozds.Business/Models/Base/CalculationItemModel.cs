using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class CalculationItemModel : ICalculationItem
{
  public abstract SpanningMeasure<decimal> Amount { get; }

  public abstract TariffMeasure<decimal> Price { get; }
}
