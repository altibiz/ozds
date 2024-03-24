using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface IMeasurement : IValidatableObject, IModel
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure Current_A { get; }

  public TariffMeasure Voltage_V { get; }

  public TariffMeasure ActivePower_W { get; }

  public TariffMeasure ReactivePower_VAR { get; }

  public TariffMeasure ApparentPower_VA { get; }

  public TariffMeasure ActiveEnergy_Wh { get; }

  public TariffMeasure ReactiveEnergy_VARh { get; }

  public TariffMeasure ApparentEnergy_VAh { get; }
}
