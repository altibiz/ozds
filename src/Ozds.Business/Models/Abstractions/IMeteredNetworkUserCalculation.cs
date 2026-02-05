using Ozds.Business.Math;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models.Abstractions;

public interface IMeteredNetworkUserCalculation : INetworkUserCalculation
{
  public UsageMeterFeeCalculationItemModel UsageMeterFee { get; }

  public SupplyActiveEnergyTotalImportT1CalculationItemModel SupplyActiveEnergyTotalImportT1 { get; }

  public SupplyActiveEnergyTotalImportT2CalculationItemModel SupplyActiveEnergyTotalImportT2 { get; }

  public SupplyBusinessUsageCalculationItemModel SupplyBusinessUsageFee { get; }

  public SupplyRenewableEnergyCalculationItemModel SupplyRenewableEnergyFee { get; }

  public decimal UsageFeeTotal_EUR { get; }
  public decimal SupplyFeeTotal_EUR { get; }

  public IEnumerable<ICalculationItem> UsageItems { get; }
  public IEnumerable<ICalculationItem> SupplyItems { get; }

  public SpanningMeasure<decimal> ActiveEnergyAmount_Wh { get; }
  public ExpenditureMeasure<decimal> ActiveEnergyPrice_EUR { get; }
  public SpanningMeasure<decimal> ReactiveEnergyAmount_Wh { get; }
  public TariffMeasure<decimal> ReactiveEnergyRampedAmount_Wh { get; }
  public ExpenditureMeasure<decimal> ReactiveEnergyPrice_EUR { get; }
  public SpanningMeasure<decimal> ActivePowerAmount_W { get; }
  public ExpenditureMeasure<decimal> ActivePowerPrice_EUR { get; }
  public ExpenditureMeasure<decimal> ComputedTotal_EUR { get; }
}
