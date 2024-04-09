using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeterModel : MeterModel
{
  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
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
