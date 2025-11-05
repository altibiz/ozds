using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : ITrackableIdentifiable, ICachedIdentifiable
{
  public string MeasurementValidatorId { get; }

  public string? MessengerId { get; }

  public decimal ConnectionPower_W { get; }

  public ICapabilities Capabilities { get; }

  public HashSet<PhaseModel> Phases { get; }

  public PeriodModel MaxInactivityPeriod { get; }
}
