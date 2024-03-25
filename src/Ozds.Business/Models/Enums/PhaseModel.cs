using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models;

public enum PhaseModel
{
  L1,
  L2,
  L3
}

public static class PhaseModelExtensions
{
  public static PhaseModel ToModel(this PhaseEntity phase)
  {
    return phase switch
    {
      PhaseEntity.L1 => PhaseModel.L1,
      PhaseEntity.L2 => PhaseModel.L2,
      PhaseEntity.L3 => PhaseModel.L3,
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }

  public static PhaseEntity ToEntity(this PhaseModel phase)
  {
    return phase switch
    {
      PhaseModel.L1 => PhaseEntity.L1,
      PhaseModel.L2 => PhaseEntity.L2,
      PhaseModel.L3 => PhaseEntity.L3,
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }
}
