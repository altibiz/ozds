using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

using static Ozds.Business.Extensions.PrimitiveRoundingExtensions;

namespace Ozds.Business.Test.Sentinel;

public static class MidpointRoundingSentinel
{

  public static void InjectInvoiceSentinel(
      CalculatedNetworkUserInvoiceModel model,
      decimal sentinel = 2.685M // NOTE: default sentinel value
                                // that differs between bankers and away from zero rounding
    )
  {

    model.Invoice
      .ArchivedRegulatoryCatalogue.TaxRate_Percent = 0M;

    var meteredCalculations = model
      .Calculations
      .OfType<MeteredNetworkUserCalculationModel>()
      .ToList();

    if (meteredCalculations.Count == 0)
    {
      return;
    }

    foreach (var calculation in meteredCalculations)
    {
      InjectCalculationSentinelInternal(calculation);
    }

    var target = meteredCalculations[0];
    target.UsageMeterFee.Total_EUR = sentinel;

    target.UsageFeeTotal_EUR = Round(sentinel, 2);
    target.SupplyFeeTotal_EUR = 0M;
    target.Total_EUR = target.UsageFeeTotal_EUR;
  }

  public static void InjectCalculationSentinel(
    MeteredNetworkUserCalculationModel model,
    decimal sentinel = 2.685M
  )
  {
    InjectCalculationSentinelInternal(model);

    model.UsageMeterFee.Total_EUR = sentinel;

    model.UsageFeeTotal_EUR = Round(sentinel, 2);
    model.SupplyFeeTotal_EUR = 0M;
    model.Total_EUR = model.UsageFeeTotal_EUR;
  }
  private static void InjectCalculationSentinelInternal(
    MeteredNetworkUserCalculationModel model
  )
  {
    model.UsageMeterFee.Total_EUR = 0M;

    model.SupplyActiveEnergyTotalImportT1.Total_EUR = 0M;
    model.SupplyActiveEnergyTotalImportT2.Total_EUR = 0M;
    model.SupplyBusinessUsageFee.Total_EUR = 0M;
    model.SupplyRenewableEnergyFee.Total_EUR = 0M;

    switch (model)
    {
      case BlueLowNetworkUserCalculationModel c:
        c.UsageActiveEnergyTotalImportT0.Total_EUR = 0M;
        c.UsageReactiveEnergyTotalRampedT0.Total_EUR = 0M;
        break;

      case WhiteLowNetworkUserCalculationModel c:
        c.UsageActiveEnergyTotalImportT1.Total_EUR = 0M;
        c.UsageActiveEnergyTotalImportT2.Total_EUR = 0M;
        c.UsageReactiveEnergyTotalRampedT0.Total_EUR = 0M;
        break;

      case RedLowNetworkUserCalculationModel c:
        c.UsageActiveEnergyTotalImportT1.Total_EUR = 0M;
        c.UsageActiveEnergyTotalImportT2.Total_EUR = 0M;
        c.UsageReactiveEnergyTotalRampedT0.Total_EUR = 0M;
        c.UsageActivePowerTotalImportT1Peak.Total_EUR = 0M;
        break;

      case WhiteMediumNetworkUserCalculationModel c:
        c.UsageActiveEnergyTotalImportT1.Total_EUR = 0M;
        c.UsageActiveEnergyTotalImportT2.Total_EUR = 0M;
        c.UsageReactiveEnergyTotalRampedT0.Total_EUR = 0M;
        c.UsageActivePowerTotalImportT1Peak.Total_EUR = 0M;
        break;
    }

    model.UsageFeeTotal_EUR = 0M;
    model.SupplyFeeTotal_EUR = 0M;
    model.Total_EUR = 0M;
  }
}
