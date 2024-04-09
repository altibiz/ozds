using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Abstractions;

public interface ICalculation : IReadonly, IIdentifiable
{
  public DateTimeOffset IssuedOn { get; }
  public string? IssuedById { get; }
  public string MeterId { get; }
  public string MeasurementLocationId { get; }
  public string CatalogueId { get; }
  public IMeter ArchivedMeter { get; }
  public MeasurementLocationModel ArchivedMeasurementLocation { get; }
  public CatalogueModel ArchivedCatalogue { get; }

  public SpanningMeasure<decimal> ActiveEnergyAmount_Wh { get; }

  public TariffMeasure<decimal> ActiveEnergyPrice_EUR { get; }

  public SpanningMeasure<decimal> ReactiveEnergyAmount_Wh { get; }

  public TariffMeasure<decimal> ReactiveEnergyRampedAmount_Wh { get; }

  public TariffMeasure<decimal> ReactiveEnergyPrice_EUR { get; }

  public SpanningMeasure<decimal> ActivePowerAmount_W { get; }

  public TariffMeasure<decimal> ActivePowerPrice_EUR { get; }

  public TariffMeasure<decimal> Total_EUR { get; }
}
