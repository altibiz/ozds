namespace Ozds.Business.Models.Abstractions;

public interface IFinancial : IIdentifiable, IReadonly
{
  public DateTimeOffset IssuedOn { get; }

  public string? IssuedById { get; }

  public DateTimeOffset FromDate { get; }

  public DateTimeOffset ToDate { get; }

  public decimal Total_EUR { get; }

  public decimal TaxRate_Percent { get; }

  public decimal Tax_EUR { get; }

  public decimal TotalWithTax_EUR { get; }
}
