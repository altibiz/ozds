using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class FinancialEntity : IdentifiableEntity, IFinancialEntity
{
  public virtual RepresentativeEntity? IssuedBy { get; set; }
  public string? RepresentativeId { get; set; }

  public DateTimeOffset IssuedOn { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public string? IssuedById { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public decimal Total_EUR { get; set; }

  public abstract decimal TaxRate_Percent { get; }

  public abstract decimal Tax_EUR { get; }

  public abstract decimal TotalWithTax_EUR { get; }

  public string Remark { get; set; } = string.Empty;
}

public class FinancialEntityTypeHierarchyConfiguration
  : EntityTypeHierarchyConfiguration<FinancialEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(FinancialEntity.TaxRate_Percent));
    builder.Ignore(nameof(FinancialEntity.Tax_EUR));
    builder.Ignore(nameof(FinancialEntity.TotalWithTax_EUR));

    builder.HasIndex(nameof(FinancialEntity.FromDate));

    builder
      .Property<DateTimeOffset>(nameof(FinancialEntity.FromDate))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .Property<DateTimeOffset>(nameof(FinancialEntity.ToDate))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .Property<DateTimeOffset>(nameof(FinancialEntity.IssuedOn))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .HasOne(nameof(CalculationEntity.IssuedBy))
      .WithMany()
      .HasForeignKey(nameof(CalculationEntity.IssuedById));

    builder
      .MonetaryValue(
        nameof(FinancialEntity.Total_EUR),
        "total_eur"
      );

    builder.Ignore(nameof(FinancialEntity.RepresentativeId));
  }
}
