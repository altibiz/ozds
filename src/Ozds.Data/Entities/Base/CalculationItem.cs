using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public abstract class CalculationItemEntity : ICalculationItemEntity
{
  public decimal Price_EUR { get; set; }

  public decimal Total_EUR { get; set; }
}
