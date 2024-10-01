using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface ICalculationItem : IModel
{
  public SpanningMeasure<decimal> Amount { get; }

  public ExpenditureMeasure<decimal> Price { get; }

  public decimal Total { get; }

  public string Kind { get; }
}
