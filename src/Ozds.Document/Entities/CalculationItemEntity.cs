namespace Ozds.Document.Entities;

public abstract class CalculationItemEntity
{
  public abstract decimal Total { get; }

  public decimal Price_EUR { get; set; }

  public decimal Total_EUR { get; set; }
}
