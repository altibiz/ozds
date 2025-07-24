using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public string MeasurementValidatorId { get; }

  public string? MessengerId { get; }

  public decimal ConnectionPower_W { get; }

  public ICapabilities Capabilities { get; }

  public HashSet<PhaseModel> Phases { get; }

  public PeriodModel MaxInactivityPeriod { get; }
}
