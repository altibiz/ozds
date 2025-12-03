namespace Ozds.Document.Entities;

public abstract class CalculationEntity : FinancialEntity
{
  public DateTimeOffset RequestedFromDate { get; set; } = default!;

  public DateTimeOffset RequestedToDate { get; set; } = default!;

  public DateTimeOffset MeteredFromDate { get; set; } = default!;

  public DateTimeOffset MeteredToDate { get; set; } = default!;

  public override decimal TaxRate_Percent
  {
    get { return 0.0M; }
  }

  public override decimal Tax_EUR
  {
    get { return 0.0M; }
  }

  public override decimal TotalWithTax_EUR
  {
    get { return Total_EUR; }
  }
}
