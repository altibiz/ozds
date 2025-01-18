using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : FinancialEntity, IInvoiceEntity
{
  public decimal InvoiceTaxRate_Percent { get; set; } = default!;

  public decimal InvoiceTax_EUR { get; set; } = default!;

  public decimal InvoiceTotalWithTax_EUR { get; set; } = default!;

  public override decimal TaxRate_Percent => InvoiceTaxRate_Percent;

  public override decimal Tax_EUR => InvoiceTax_EUR;

  public override decimal TotalWithTax_EUR => InvoiceTotalWithTax_EUR;
}

public class
  InvoiceEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  InvoiceEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(InvoiceEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(entity);

    builder
      .Property(nameof(InvoiceEntity.InvoiceTaxRate_Percent))
      .HasColumnName("tax_rate_percent");

    builder
      .Property(nameof(InvoiceEntity.InvoiceTax_EUR))
      .HasColumnName("tax_eur");

    builder
      .Property(nameof(InvoiceEntity.InvoiceTotalWithTax_EUR))
      .HasColumnName("total_with_tax_eur");
  }
}
