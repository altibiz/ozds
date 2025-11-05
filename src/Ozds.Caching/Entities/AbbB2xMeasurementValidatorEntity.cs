using Ozds.Caching.Entities.Base;

namespace Ozds.Caching.Entities;

public class AbbB2xMeasurementValidatorEntity : MeasurementValidatorEntity
{
  public decimal MinVoltage_V { get; set; } = default!;

  public decimal MaxVoltage_V { get; set; } = default!;

  public decimal MinCurrent_A { get; set; } = default!;

  public decimal MaxCurrent_A { get; set; } = default!;

  public decimal MinActivePower_W { get; set; } = default!;

  public decimal MaxActivePower_W { get; set; } = default!;

  public decimal MinReactivePower_VAR { get; set; } = default!;

  public decimal MaxReactivePower_VAR { get; set; } = default!;
}
