using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface ICalculationItem
{
  public SpanningMeasure<decimal> Amount { get; }

  public TariffMeasure<decimal> Price { get; }
}
