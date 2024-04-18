using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface ICalculationItem
{
  public SpanningMeasure<decimal> Amount { get; }

  public ExpenditureMeasure<decimal> Price { get; }

  public string Kind { get; }
}
