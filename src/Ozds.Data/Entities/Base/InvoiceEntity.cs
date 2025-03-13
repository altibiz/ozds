using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : FinancialEntity, IInvoiceEntity
{
  public decimal InvoiceTaxRate_Percent { get; set; } = default!;

  public decimal InvoiceTax_EUR { get; set; } = default!;

  public decimal InvoiceTotalWithTax_EUR { get; set; } = default!;

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

public class
  InvoiceEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  InvoiceEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .MonetaryValue(
        nameof(InvoiceEntity.InvoiceTaxRate_Percent),
        "tax_rate_percent"
      );

    builder
      .MonetaryValue(
        nameof(InvoiceEntity.InvoiceTax_EUR),
        "tax_eur"
      );

    builder
      .MonetaryValue(
        nameof(InvoiceEntity.InvoiceTotalWithTax_EUR),
        "total_with_tax_eur"
      );
  }
}
