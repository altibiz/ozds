using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface IMeasurement : IValidatableObject, IReadonly
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure<float> Current_A { get; }

  public TariffMeasure<float> Voltage_V { get; }

  public TariffMeasure<float> ActivePower_W { get; }

  public TariffMeasure<float> ReactivePower_VAR { get; }

  public TariffMeasure<float> ApparentPower_VA { get; }

  public TariffMeasure<float> ActiveEnergy_Wh { get; }

  public TariffMeasure<float> ReactiveEnergy_VARh { get; }

  public TariffMeasure<float> ApparentEnergy_VAh { get; }
}
