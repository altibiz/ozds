namespace Ozds.Document.Entities;

public abstract class FinancialEntity
{
  public DateTimeOffset IssuedOn { get; set; }

  public string? IssuedById { get; set; }

  public DateTimeOffset FromDate { get; set; }

  public DateTimeOffset ToDate { get; set; }

  public decimal Total_EUR { get; set; }

  public abstract decimal TaxRate_Percent { get; }

  public abstract decimal Tax_EUR { get; }

  public abstract decimal TotalWithTax_EUR { get; }
}
