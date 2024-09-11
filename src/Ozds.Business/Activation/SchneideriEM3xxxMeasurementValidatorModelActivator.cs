using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class SchneideriEM3xxxMeasurementValidatorModelActivator : ModelActivator<SchneideriEM3xxxMeasurementValidatorModel>
{
  public override SchneideriEM3xxxMeasurementValidatorModel ActivateConcrete()
  {
    return New();
  }

  public static SchneideriEM3xxxMeasurementValidatorModel New()
  {
    return new SchneideriEM3xxxMeasurementValidatorModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      MinVoltage_V = default,
      MaxVoltage_V = default,
      MinCurrent_A = default,
      MaxCurrent_A = default,
      MinActivePower_W = default,
      MaxActivePower_W = default,
      MinReactivePower_VAR = default,
      MaxReactivePower_VAR = default,
      MinApparentPower_VA = default,
      MaxApparentPower_VA = default
    };
  }
}
