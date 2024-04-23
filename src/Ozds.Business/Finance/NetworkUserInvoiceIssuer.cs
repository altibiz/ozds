using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Agnostic;

// TODO: fix getting representative id

namespace Ozds.Business.Finance;

public class NetworkUserInvoiceIssuer
{
  private readonly AgnosticNetworkUserCalculationCalculator
    _calculationCalculator;

  private readonly IHttpContextAccessor _httpContextAccessor;

  public NetworkUserInvoiceIssuer(
    AgnosticNetworkUserCalculationCalculator calculationCalculator,
    IHttpContextAccessor httpContextAccessor
  )
  {
    _calculationCalculator = calculationCalculator;
    _httpContextAccessor = httpContextAccessor;
  }

  public CalculatedNetworkUserInvoiceModel Issue(
    NetworkUserInvoiceIssuingBasisModel basis
  )
  {
    var now = DateTimeOffset.UtcNow;
    var issuer = GetRepresentativeId();

    var calculations = basis.NetworkUserCalculationBases
      .Select(_calculationCalculator.Calculate)
      .ToList();

    var usageActiveEnergyTotalImportT0Fee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageActiveEnergyTotalImportT0CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageActiveEnergyTotalImportT1Fee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageActiveEnergyTotalImportT1CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageActiveEnergyTotalImportT2Fee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageActiveEnergyTotalImportT2CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageActivePowerTotalImportT1PeakFee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageActivePowerTotalImportT1PeakCalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageReactiveEnergyTotalRampedT0Fee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageReactiveEnergyTotalRampedT0CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageMeterFee = calculations
      .SelectMany(calculation => calculation.UsageItems
        .OfType<UsageMeterFeeCalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var usageFeeTotal = usageActiveEnergyTotalImportT0Fee
                        + usageActiveEnergyTotalImportT1Fee
                        + usageActiveEnergyTotalImportT2Fee
                        + usageActivePowerTotalImportT1PeakFee
                        + usageReactiveEnergyTotalRampedT0Fee
                        + usageMeterFee;

    var supplyActiveEnergyTotalImportT1Fee = calculations
      .SelectMany(calculation => calculation.SupplyItems
        .OfType<SupplyActiveEnergyTotalImportT1CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var supplyActiveEnergyTotalImportT2Fee = calculations
      .SelectMany(calculation => calculation.SupplyItems
        .OfType<SupplyActiveEnergyTotalImportT2CalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var supplyBusinessUsageFee = calculations
      .SelectMany(calculation => calculation.SupplyItems
        .OfType<SupplyBusinessUsageFeeCalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var supplyRenewableEnergyFee = calculations
      .SelectMany(calculation => calculation.SupplyItems
        .OfType<SupplyRenewableEnergyFeeCalculationItemModel>())
      .Sum(item => item.Total_EUR);

    var supplyFeeTotal = supplyActiveEnergyTotalImportT1Fee
                         + supplyActiveEnergyTotalImportT2Fee
                         + supplyBusinessUsageFee
                         + supplyRenewableEnergyFee;

    var total = usageFeeTotal + supplyFeeTotal;
    var tax = total * basis.RegulatoryCatalogue.TaxRate_Percent / 100M;
    var totalWithTax = total + tax;

    var initial = new NetworkUserInvoiceModel
    {
      Id = default!,
      Title = issuer is null
        ? $"Invoice for {basis.NetworkUser.Title} at {basis.Location.Title}"
        : $"Invoice for {basis.NetworkUser.Title} at {basis.Location.Title} issued by {issuer}",
      IssuedById = issuer,
      IssuedOn = now,
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
      Tax_EUR = tax,
      TotalWithTax_EUR = totalWithTax
    };

    return new CalculatedNetworkUserInvoiceModel(
      Invoice: initial,
      Calculations: calculations
    );
  }

  private string? GetRepresentativeId()
  {
    if (_httpContextAccessor.HttpContext is not { } httpContextAccessor)
    {
      return null;
    }

    return null;
    // return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
  }
}
