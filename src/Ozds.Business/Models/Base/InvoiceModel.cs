using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class InvoiceModel : FinancialModel, IInvoice
{
  [Required]
  public required decimal InvoiceTaxRate_Percent { get; set; }

  [Required]
  public required decimal InvoiceTax_EUR { get; set; }

  [Required]
  public required decimal InvoiceTotalWithTax_EUR { get; set; }

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
