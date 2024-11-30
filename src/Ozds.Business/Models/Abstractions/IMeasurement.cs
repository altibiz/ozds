using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface IMeasurement : IReadonly
{
  public string MeterId { get; }

  public string MeasurementLocationId { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure<decimal> Current_A { get; }

  public TariffMeasure<decimal> Voltage_V { get; }

  public TariffMeasure<decimal> ActivePower_W { get; }

  public TariffMeasure<decimal> ReactivePower_VAR { get; }

  public TariffMeasure<decimal> ApparentPower_VA { get; }

  public TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  public TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  public TariffMeasure<decimal> ApparentEnergy_VAh { get; }
}
