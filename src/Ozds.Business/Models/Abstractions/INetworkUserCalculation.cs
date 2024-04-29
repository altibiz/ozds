using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Abstractions;

public interface INetworkUserCalculation : ICalculation
{
  public string UsageNetworkUserCatalogueId { get; }
  public string SupplyRegulatoryCatalogueId { get; }
  public string NetworkUserInvoiceId { get; }
  public string NetworkUserMeasurementLocationId { get; }
  public string Kind { get; }
  public NetworkUserCatalogueModel ArchivedUsageNetworkUserCatalogue { get; }
  public RegulatoryCatalogueModel ArchivedSupplyRegulatoryCatalogue { get; }

  public NetworkUserMeasurementLocationModel
    ArchivedNetworkUserMeasurementLocation { get; }

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
