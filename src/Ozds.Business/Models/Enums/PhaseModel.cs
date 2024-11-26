using Ozds.Business.Math;
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
      PhaseModel.L1 => Colors[index % Colors.Length][0],
      PhaseModel.L2 => Colors[index % Colors.Length][1],
      PhaseModel.L3 => Colors[index % Colors.Length][2],
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }

  public static decimal GetMeasure(
    this PhasicMeasure<decimal> phasic,
    PhaseModel? phase
  )
  {
    return phase switch
    {
      PhaseModel.L1 => phasic.PhaseSplit().ValueL1,
      PhaseModel.L2 => phasic.PhaseSplit().ValueL2,
      PhaseModel.L3 => phasic.PhaseSplit().ValueL3,
      _ => phasic.PhaseSum()
    };
  }

  // NOTE: generate { |$i| if $i <= 10 { { out: (["#FB8C00", "#E91E63", "#20F97B"] | each { |x| pastel rotate ($i * 10) $x | pastel format hex }), next: ($i + 1) } } } 1 | to json
  private static readonly string[][] Colors = [
    [
      "#fbd500",
      "#e91e25",
      "#20f9bc"
    ],
    [
      "#d5fb00",
      "#e9541e",
      "#20f5f9"
    ],
    [
      "#8afb00",
      "#e9911e",
      "#20b4f9"
    ],
    [
      "#3ffb00",
      "#e9ce1e",
      "#2073f9"
    ],
    [
      "#00fb0d",
      "#c7e91e",
      "#2032f9"
    ],
    [
      "#00fb58",
      "#8ae91e",
      "#4f20f9"
    ],
    [
      "#00fba3",
      "#4de91e",
      "#9020f9"
    ],
    [
      "#00fbee",
      "#1ee92c",
      "#d120f9"
    ],
    [
      "#00bcfb",
      "#1ee969",
      "#f920e0"
    ],
    [
      "#0071fb",
      "#1ee9a5",
      "#f9209f"
    ]
  ];
}
