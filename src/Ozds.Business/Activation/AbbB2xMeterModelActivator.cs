using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation;

public class AbbB2xMeterModelActivator : ModelActivator<AbbB2xMeterModel>
{
  public override AbbB2xMeterModel ActivateConcrete()
  {
    return New();
  }

  public static AbbB2xMeterModel New()
  {
    return new AbbB2xMeterModel
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
      ConnectionPower_W = default,
      Phases = default!,
      MessengerId = default!,
      MeasurementValidatorId = default!,
      Kind = default!
    };
  }
}
