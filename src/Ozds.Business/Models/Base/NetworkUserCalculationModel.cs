using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;

// TODO: Total_EUR as composite of this and going through items once
// expenditure math is done

namespace Ozds.Business.Models.Base;

public abstract class NetworkUserCalculationModel : INetworkUserCalculation
{
  [Required] public required DateTimeOffset FromDate { get; set; } = default!;
  [Required] public required DateTimeOffset ToDate { get; set; } = default!;

  [Required]
  public required UsageMeterFeeCalculationItemModel
    UsageMeterFee
  { get; set; } = default!;

  [Required]
  public required SupplyActiveEnergyTotalImportT1CalculationItemModel
    SupplyActiveEnergyTotalImportT1
  { get; set; } = default!;

  [Required]
  public required SupplyActiveEnergyTotalImportT2CalculationItemModel
    SupplyActiveEnergyTotalImportT2
  { get; set; } = default!;

  [Required]
  public required SupplyBusinessUsageCalculationItemModel
    SupplyBusinessUsageFee
  { get; set; } = default!;

  [Required]
  public required SupplyRenewableEnergyCalculationItemModel
    SupplyRenewableEnergyFee
  { get; set; } = default!;

  protected abstract IEnumerable<ICalculationItem> AdditionalUsageItems { get; }

  [Required]
  public required DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  [Required] public required string? IssuedById { get; set; } = default!;

  [Required] public required string Id { get; init; } = default!;
  [Required] public required string Title { get; set; } = default!;

  [Required] public required string MeterId { get; set; } = default!;

  [Required]
  public required string MeasurementLocationId { get; set; } = default!;

  [Required]
  public required string UsageNetworkUserCatalogueId { get; set; } = default!;

  [Required]
  public required string SupplyRegulatoryCatalogueId { get; set; } = default!;

  [Required]
  public required string NetworkUserInvoiceId { get; set; } = default!;

  [Required] public required IMeter ArchivedMeter { get; set; } = default!;

  [Required]
  public required NetworkUserMeasurementLocationModel
    ArchivedMeasurementLocation
  { get; set; } = default!;

  [Required]
  public required RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  [Required] public required decimal UsageFeeTotal_EUR { get; set; }

  [Required] public required decimal SupplyFeeTotal_EUR { get; set; }

  [Required] public required decimal Total_EUR { get; set; }

  public abstract NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue { get; }

  public abstract string Kind { get; }

  public virtual IEnumerable<ICalculationItem> UsageItems
  {
    get
    {
      return AdditionalUsageItems
        .ToArray()
        .Concat(new ICalculationItem[]
        {
          UsageMeterFee
        });
    }
  }

  public virtual IEnumerable<ICalculationItem> SupplyItems
  {
    get
    {
      return new ICalculationItem[]
      {
        SupplyActiveEnergyTotalImportT1,
        SupplyActiveEnergyTotalImportT2,
        SupplyBusinessUsageFee,
        SupplyRenewableEnergyFee
      };
    }
  }

  public abstract SpanningMeasure<decimal> ActiveEnergyAmount_Wh { get; }

  public abstract ExpenditureMeasure<decimal> ActiveEnergyPrice_EUR { get; }

  public abstract SpanningMeasure<decimal> ReactiveEnergyAmount_Wh { get; }

  public virtual TariffMeasure<decimal> ReactiveEnergyRampedAmount_Wh
  {
    get
    {
      var result =
        ReactiveEnergyAmount_Wh.SpanDiff.Select(duplex =>
          new AnyDuplexMeasure<decimal>(duplex.DuplexAbs.DuplexSum)) -
        ActiveEnergyAmount_Wh.SpanDiff.Select(duplex =>
          new AnyDuplexMeasure<decimal>(duplex.DuplexAny));

      return result
        .Select(duplex => duplex.DuplexAny.PhaseSum < 0
          ? DuplexMeasure<decimal>.Null
          : duplex);
    }
  }

  public abstract ExpenditureMeasure<decimal> ReactiveEnergyPrice_EUR { get; }

  public abstract SpanningMeasure<decimal> ActivePowerAmount_W { get; }

  public abstract ExpenditureMeasure<decimal> ActivePowerPrice_EUR { get; }

  public virtual ExpenditureMeasure<decimal> ComputedTotal_EUR
  {
    get
    {
      return new DualExpenditureMeasure<decimal>(
        ActiveEnergyAmount_Wh.SpanDiff *
        ActiveEnergyPrice_EUR.ExpenditureUsage +
        ReactiveEnergyRampedAmount_Wh *
        ReactiveEnergyPrice_EUR.ExpenditureUsage +
        ActivePowerAmount_W.SpanDiff * ActivePowerPrice_EUR.ExpenditureUsage,
        ActiveEnergyAmount_Wh.SpanDiff *
        ActiveEnergyPrice_EUR.ExpenditureSupply +
        ReactiveEnergyRampedAmount_Wh *
        ReactiveEnergyPrice_EUR.ExpenditureSupply +
        ActivePowerAmount_W.SpanDiff * ActivePowerPrice_EUR.ExpenditureSupply
      );
    }
  }

  public virtual IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    if (validationContext.MemberName is null ||
        validationContext.MemberName == nameof(IssuedOn))
    {
      if (IssuedOn > DateTimeOffset.UtcNow)
      {
        yield return new ValidationResult("IssuedOn must be in the past",
          new[] { nameof(IssuedOn) });
      }
    }
  }
}

public abstract class NetworkUserCalculationModel<TNetworkUserCatalogue>
  : NetworkUserCalculationModel, INetworkUserCalculation
  where TNetworkUserCatalogue : NetworkUserCatalogueModel
{
  [Required]
  public required TNetworkUserCatalogue ConcreteArchivedUsageNetworkUserCatalogue
  {
    get;
    set;
  } = default!;

  public override NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue =>
    ArchivedUsageNetworkUserCatalogue;
}
