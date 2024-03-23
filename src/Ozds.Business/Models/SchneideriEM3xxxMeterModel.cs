using System.ComponentModel.DataAnnotations;
using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models;

public record SchneideriEM3xxxMeterModel(
  string Id,
  bool IsDeleted,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId,
  float MinVoltage_V,
  float MaxVoltage_V,
  float MinCurrent_A,
  float MaxCurrent_A,
  float MinActivePower_W,
  float MaxActivePower_W,
  float MinReactivePower_VAR,
  float MaxReactivePower_VAR,
  float MinApparentPower_VAR,
  float MaxApparentPower_VAR,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : MeterModel(
  Id,
  IsDeleted,
  NetworkUserMeasurementLocationId,
  LocationMeasurementLocationId
)
{
  public override ICapabilities Capabilities => new SchneideriEM3xxxCapabilities();

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}
