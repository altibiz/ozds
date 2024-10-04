using Ozds.Business.Activation.Base;
using Ozds.Business.Activation.Complex;
using Ozds.Business.Models;

namespace Ozds.Business.Activation;

public class PidgeonMessengerModelActivator
  : ModelActivator<PidgeonMessengerModel>
{
  public override PidgeonMessengerModel ActivateConcrete()
  {
    return New();
  }

  public static PidgeonMessengerModel New()
  {
    return new PidgeonMessengerModel
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
      LocationId = default!,
      MaxInactivityPeriod = PeriodModelActivator.New(),
      PushDelayPeriod = PeriodModelActivator.New()
    };
  }
}
