using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;

// TODO: ArchivedNetworkUserMeasurementLocation

namespace Ozds.Business.Models.Base;

public abstract class NetworkUserCalculationModel : CalculationModel,
  INetworkUserCalculation
{
  [Required]
  public required UsageMeterFeeCalculationItemModel
    UsageMeterFee { get; set; } = default!;

  [Required]
  public required SupplyActiveEnergyTotalImportT1CalculationItemModel
    SupplyActiveEnergyTotalImportT1 { get; set; } = default!;

  [Required]
  public required SupplyActiveEnergyTotalImportT2CalculationItemModel
    SupplyActiveEnergyTotalImportT2 { get; set; } = default!;

  [Required]
  public required SupplyBusinessUsageCalculationItemModel
    SupplyBusinessUsageFee { get; set; } = default!;

  [Required]
  public required SupplyRenewableEnergyCalculationItemModel
    SupplyRenewableEnergyFee { get; set; } = default!;

  protected abstract IEnumerable<ICalculationItem> AdditionalUsageItems { get; }

  [Required]
  public required string NetworkUserMeasurementLocationId { get; set; } =
    default!;

  [Required]
  public required NetworkUserMeasurementLocationModel
    ArchivedNetworkUserMeasurementLocation { get; set; } = default!;

  [Required]
  public required string UsageNetworkUserCatalogueId { get; set; } = default!;

  [Required]
  public required string SupplyRegulatoryCatalogueId { get; set; } = default!;

  [Required]
  public required string NetworkUserInvoiceId { get; set; } = default!;

  [Required]
  public required RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue
  {
    get;
    set;
  } = default!;

  [Required]
  public required decimal UsageFeeTotal_EUR { get; set; }

  [Required]
  public required decimal SupplyFeeTotal_EUR { get; set; }

  public abstract NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue
  {
    get;
  }

  public abstract string Kind { get; }

  public virtual IEnumerable<ICalculationItem> UsageItems
  {
    get
    {
      return AdditionalUsageItems
        .AsEnumerable()
        .Concat(
          new ICalculationItem[]
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
      var result = ReactiveEnergyAmount_Wh
        .SpanDiff()
        .Select(
          duplex =>
            new AnyDuplexMeasure<decimal>(duplex.DuplexAbs().DuplexSum()))
        .Subtract(
          ActiveEnergyAmount_Wh
            .SpanDiff()
            .Select(
              duplex =>
                new AnyDuplexMeasure<decimal>(duplex.DuplexImport()))
            .Multiply(0.33M));

      return result
        .Select(
          duplex => duplex.DuplexAny().PhaseSum() < 0
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
        ActiveEnergyAmount_Wh.SpanDiff()
          .Multiply(ActiveEnergyPrice_EUR.ExpenditureUsage())
          .Add(
            ReactiveEnergyRampedAmount_Wh.Multiply(
              ReactiveEnergyPrice_EUR
                .ExpenditureUsage()))
          .Add(
            ActivePowerAmount_W.SpanDiff().Multiply(
              ActivePowerPrice_EUR
                .ExpenditureUsage())),
        ActiveEnergyAmount_Wh.SpanDiff()
          .Multiply(ActiveEnergyPrice_EUR.ExpenditureSupply())
          .Add(
            ReactiveEnergyRampedAmount_Wh.Multiply(
              ReactiveEnergyPrice_EUR
                .ExpenditureSupply()))
          .Add(
            ActivePowerAmount_W.SpanDiff().Multiply(
              ActivePowerPrice_EUR
                .ExpenditureSupply()))
      );
    }
  }
}

public abstract class NetworkUserCalculationModel<TNetworkUserCatalogue>
  : NetworkUserCalculationModel, INetworkUserCalculation
  where TNetworkUserCatalogue : NetworkUserCatalogueModel
{
  [Required]
  public required TNetworkUserCatalogue
    ConcreteArchivedUsageNetworkUserCatalogue { get; set; } = default!;

  public override NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue
  {
    get { return ConcreteArchivedUsageNetworkUserCatalogue; }
  }
}
