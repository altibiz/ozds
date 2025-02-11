namespace Ozds.Document.Entities;

public abstract class InvoiceEntity : FinancialEntity
{
  public string Id { get; set; } = default!;

  public decimal InvoiceTaxRate_Percent { get; set; }

  public decimal InvoiceTax_EUR { get; set; }

  public decimal InvoiceTotalWithTax_EUR { get; set; }

  public override decimal TaxRate_Percent
  {
    get { return InvoiceTaxRate_Percent; }
  }

  public override decimal Tax_EUR
  {
    get { return InvoiceTax_EUR; }
  }

  public override decimal TotalWithTax_EUR
  {
    get { return InvoiceTotalWithTax_EUR; }
  }
}
