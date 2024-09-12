using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation;

public class
  SchneideriEM3xxxMeterModelActivator : ModelActivator<
  SchneideriEM3xxxMeterModel>
{
  public override SchneideriEM3xxxMeterModel ActivateConcrete()
  {
    return New();
  }

  public static SchneideriEM3xxxMeterModel New()
  {
    return new SchneideriEM3xxxMeterModel
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
      MeasurementValidatorId = default!
    };
  }
}
