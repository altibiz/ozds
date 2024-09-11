using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class AbbB2xMeasurementValidatorModelActivator : ModelActivator<AbbB2xMeasurementValidatorModel>
{
  public override AbbB2xMeasurementValidatorModel ActivateConcrete()
  {
    return New();
  }

  public static AbbB2xMeasurementValidatorModel New()
  {
    return new AbbB2xMeasurementValidatorModel
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
      MaxReactivePower_VAR = default
    };
  }
}
