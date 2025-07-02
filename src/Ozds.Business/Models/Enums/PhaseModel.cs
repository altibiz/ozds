using Ozds.Business.Extensions;
using Ozds.Business.Math;
using Ozds.Business.Models.Enums;
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

  public static string ToTitle(this PhaseModel phase)
  {
    return phase switch
    {
      PhaseModel.L1 => "L1",
      PhaseModel.L2 => "L2",
      PhaseModel.L3 => "L3",
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }

  public static string ToColor(this PhaseModel phase, int index = 0)
  {
    return phase switch
    {
      PhaseModel.L1 => index.ToPhaseColors()[0],
      PhaseModel.L2 => index.ToPhaseColors()[1],
      PhaseModel.L3 => index.ToPhaseColors()[2],
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }

  public static decimal GetMeasure(
    this PhasicMeasure<decimal> phasic,
    PhaseModel? phase,
    MeasureModel? measure
  )
  {
    return phase switch
    {
      PhaseModel.L1 => phasic.PhaseSplit().ValueL1,
      PhaseModel.L2 => phasic.PhaseSplit().ValueL2,
      PhaseModel.L3 => phasic.PhaseSplit().ValueL3,
      _ => measure switch
      {
        MeasureModel.Voltage
          or MeasureModel.Current
          or MeasureModel.ActivePower
          or MeasureModel.ReactivePower
          or MeasureModel.ApparentPower => phasic.PhaseAverage(),
        MeasureModel.ActiveEnergy
          or MeasureModel.ReactiveEnergy
          or MeasureModel.ApparentEnergy => phasic.PhaseSum(),
        _ => phasic.PhaseSum()
      }
    };
  }
}
