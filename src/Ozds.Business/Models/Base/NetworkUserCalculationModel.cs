using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class NetworkUserCalculationModel : INetworkUserCalculation
{
  [Required] public required DateTimeOffset FromDate { get; set; } = default!;
  [Required] public required DateTimeOffset ToDate { get; set; } = default!;

  [Required]
  public required DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  [Required] public required string? IssuedById { get; set; } = default!;

  [Required] public required string Id { get; init; } = default!;
  [Required] public required string Title { get; set; } = default!;

  [Required] public required string MeterId { get; set; } = default!;

  [Required]
  public required string MeasurementLocationId { get; set; } = default!;

  [Required] public required string NetworkUserCatalogueId { get; set; } = default!;

  [Required]
  public required string NetworkUserInvoiceId { get; set; } = default!;

  [Required] public required MeterModel ArchivedMeter { get; set; } = default!;

  [Required]
  public required NetworkUserMeasurementLocationModel
    ArchivedMeasurementLocation
  { get; set; } = default!;

  [Required]
  public required NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue { get; set; } = default!;

  [Required]
  public required RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue { get; set; } = default!;

  public abstract string Kind { get; }

  public abstract SpanningMeasure<decimal> ActiveEnergyAmount_Wh { get; }

  public abstract TariffMeasure<decimal> ActiveEnergyPrice_EUR { get; }

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

  public abstract TariffMeasure<decimal> ReactiveEnergyPrice_EUR { get; }

  public abstract SpanningMeasure<decimal> ActivePowerAmount_W { get; }

  public abstract TariffMeasure<decimal> ActivePowerPrice_EUR { get; }

  public virtual TariffMeasure<decimal> Total_EUR
  {
    get
    {
      return
        ActiveEnergyAmount_Wh.SpanDiff * ActiveEnergyPrice_EUR +
        ActivePowerAmount_W.SpanPeak * ActivePowerPrice_EUR +
        ReactiveEnergyRampedAmount_Wh * ReactiveEnergyPrice_EUR;
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
