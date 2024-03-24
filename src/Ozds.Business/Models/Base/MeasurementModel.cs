using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract record MeasurementModel<T>(
  string MeterId,
  DateTimeOffset Timestamp
) : IMeasurement
  where T : IMeasurementValidator
{
  public abstract TariffMeasure Current_A { get; }

  public abstract TariffMeasure Voltage_V { get; }

  public abstract TariffMeasure ActivePower_W { get; }

  public abstract TariffMeasure ReactivePower_VAR { get; }

  public abstract TariffMeasure ApparentPower_VA { get; }

  public abstract TariffMeasure ActiveEnergyCumulative_Wh { get; }

  public abstract TariffMeasure ReactiveEnergyCumulative_VARh { get; }

  public abstract TariffMeasure ApparentEnergyCumulative_VAh { get; }

  public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}
