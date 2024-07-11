using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

// TODO: get issuer
// TODO: add assertion that totals match

namespace Ozds.Business.Finance;

public class NetworkUserInvoiceCalculator(
  AgnosticNetworkUserCalculationCalculator calculationCalculator
)
{
  private readonly AgnosticNetworkUserCalculationCalculator
    _calculationCalculator = calculationCalculator;

  public CalculatedNetworkUserInvoiceModel Calculate(
    NetworkUserInvoiceIssuingBasisModel basis
  )
  {
    var calculations = basis.NetworkUserCalculationBases
      .Select(_calculationCalculator.Calculate)
      .ToList();

    var usageActiveEnergyTotalImportT0Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageActiveEnergyTotalImportT0CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageActiveEnergyTotalImportT1Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageActiveEnergyTotalImportT1CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageActiveEnergyTotalImportT2Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageActiveEnergyTotalImportT2CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageActivePowerTotalImportT1PeakFee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageActivePowerTotalImportT1PeakCalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageReactiveEnergyTotalRampedT0Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageReactiveEnergyTotalRampedT0CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageMeterFee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.UsageItems
            .OfType<UsageMeterFeeCalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var usageFeeTotal = System.Math.Round(
      usageActiveEnergyTotalImportT0Fee
      + usageActiveEnergyTotalImportT1Fee
      + usageActiveEnergyTotalImportT2Fee
      + usageActivePowerTotalImportT1PeakFee
      + usageReactiveEnergyTotalRampedT0Fee
      + usageMeterFee,
      2);

    var supplyActiveEnergyTotalImportT1Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.SupplyItems
            .OfType<SupplyActiveEnergyTotalImportT1CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var supplyActiveEnergyTotalImportT2Fee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.SupplyItems
            .OfType<SupplyActiveEnergyTotalImportT2CalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var supplyBusinessUsageFee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.SupplyItems
            .OfType<SupplyBusinessUsageCalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var supplyRenewableEnergyFee = System.Math.Round(
      calculations
        .SelectMany(
          calculation => calculation.SupplyItems
            .OfType<SupplyRenewableEnergyCalculationItemModel>())
        .Sum(item => item.Total),
      2);

    var supplyFeeTotal = System.Math.Round(
      supplyActiveEnergyTotalImportT1Fee
      + supplyActiveEnergyTotalImportT2Fee
      + supplyBusinessUsageFee
      + supplyRenewableEnergyFee,
      2);

    var total = System.Math.Round(usageFeeTotal + supplyFeeTotal, 2);
    var taxRate = System.Math.Round(
      basis.RegulatoryCatalogue.TaxRate_Percent,
      2);
    var tax = System.Math.Round(total * taxRate / 100M, 2);
    var totalWithTax = System.Math.Round(total + tax, 2);

    var initial = new NetworkUserInvoiceModel
    {
      Id = default!,
      Title =
        $"Invoice for {basis.NetworkUser.Title} at {basis.Location.Title}",
      IssuedById = default!,
      IssuedOn = DateTimeOffset.UtcNow,
      NetworkUserId = basis.NetworkUser.Id,
      FromDate = basis.FromDate,
      ToDate = basis.ToDate,
      ArchivedNetworkUser = basis.NetworkUser,
      ArchivedLocation = basis.Location,
      ArchivedRegulatoryCatalogue = basis.RegulatoryCatalogue,
      UsageActiveEnergyTotalImportT0Fee_EUR = usageActiveEnergyTotalImportT0Fee,
      UsageActiveEnergyTotalImportT1Fee_EUR = usageActiveEnergyTotalImportT1Fee,
      UsageActiveEnergyTotalImportT2Fee_EUR = usageActiveEnergyTotalImportT2Fee,
      UsageActivePowerTotalImportT1PeakFee_EUR =
        usageActivePowerTotalImportT1PeakFee,
      UsageReactiveEnergyTotalRampedT0Fee_EUR =
        usageReactiveEnergyTotalRampedT0Fee,
      UsageMeterFee_EUR = usageMeterFee,
      UsageFeeTotal_EUR = usageFeeTotal,
      SupplyActiveEnergyTotalImportT1Fee_EUR =
        supplyActiveEnergyTotalImportT1Fee,
      SupplyActiveEnergyTotalImportT2Fee_EUR =
        supplyActiveEnergyTotalImportT2Fee,
      SupplyBusinessUsageFee_EUR = supplyBusinessUsageFee,
      SupplyRenewableEnergyFee_EUR = supplyRenewableEnergyFee,
      SupplyFeeTotal_EUR = supplyFeeTotal,
      Total_EUR = total,
      TaxRate_Percent = taxRate,
      Tax_EUR = tax,
      TotalWithTax_EUR = totalWithTax
    };

    return new CalculatedNetworkUserInvoiceModel(
      Invoice: initial,
      Calculations: calculations
    );
  }
}
