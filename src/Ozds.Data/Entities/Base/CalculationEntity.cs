using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class CalculationEntity : FinancialEntity, ICalculationEntity
{
  public virtual MeterEntity Meter { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public override decimal TaxRate_Percent => 0.0M;

  public override decimal Tax_EUR => 0.0M;

  public override decimal TotalWithTax_EUR => Total_EUR;
}

public class
  CalculationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    CalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(CalculationEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(entity);

    builder
      .HasOne(nameof(CalculationEntity.Meter))
      .WithMany(nameof(MeterEntity.NetworkUserCalculations))
      .HasForeignKey(nameof(CalculationEntity.MeterId));

    builder
      .ArchivedProperty(nameof(CalculationEntity.ArchivedMeter));
  }
}
