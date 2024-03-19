namespace Ozds.Business.Math;

public record TriPhasicMeasure(float ValueL1, float ValueL2, float ValueL3) : PhasicMeasure;

public record SinglePhasicMeasure(float Value) : PhasicMeasure;

public record PhasicMeasure()
{
  public static readonly PhasicMeasure Null = new();

  public float PhaseSum => this switch
  {
    TriPhasicMeasure tri => tri.ValueL1 + tri.ValueL2 + tri.ValueL3,
    SinglePhasicMeasure single => single.Value,
    _ => 0
  };

  public float PhaseAverage => this switch
  {
    TriPhasicMeasure tri => (tri.ValueL1 + tri.ValueL2 + tri.ValueL3) / 3,
    SinglePhasicMeasure single => single.Value,
    _ => 0
  };

  public float PhasePeak => this switch
  {
    TriPhasicMeasure tri => tri.ValueL1 > tri.ValueL2
      ? tri.ValueL1 > tri.ValueL3
        ? tri.ValueL1
        : tri.ValueL3
      : tri.ValueL2 > tri.ValueL3
        ? tri.ValueL2
        : tri.ValueL3,
    SinglePhasicMeasure single => single.Value,
    _ => 0
  };

  public static PhasicMeasure operator +(PhasicMeasure lhs, float rhs) => lhs switch
  {
    TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 + rhs, tri.ValueL2 + rhs, tri.ValueL3 + rhs),
    SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value + rhs),
    _ => Null,
  };

  public static PhasicMeasure operator -(PhasicMeasure lhs, float rhs) => lhs switch
  {
    TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 - rhs, tri.ValueL2 - rhs, tri.ValueL3 - rhs),
    SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value - rhs),
    _ => Null,
  };

  public static PhasicMeasure operator *(PhasicMeasure lhs, float rhs) => lhs switch
  {
    TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 * rhs, tri.ValueL2 * rhs, tri.ValueL3 * rhs),
    SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value * rhs),
    _ => Null,
  };

  public static PhasicMeasure operator /(PhasicMeasure lhs, float rhs) => lhs switch
  {
    TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 / rhs, tri.ValueL2 / rhs, tri.ValueL3 / rhs),
    SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value / rhs),
    _ => Null,
  };

  public static PhasicMeasure operator +(PhasicMeasure lhs, PhasicMeasure rhs) => (lhs, rhs) switch
  {
    (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new TriPhasicMeasure(triLhs.ValueL1 + triRhs.ValueL1, triLhs.ValueL2 + triRhs.ValueL2, triLhs.ValueL3 + triRhs.ValueL3),
    (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) => new SinglePhasicMeasure(singleLhs.Value + singleRhs.Value),
    _ => Null,
  };

  public static PhasicMeasure operator -(PhasicMeasure lhs, PhasicMeasure rhs) => (lhs, rhs) switch
  {
    (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new TriPhasicMeasure(triLhs.ValueL1 - triRhs.ValueL1, triLhs.ValueL2 - triRhs.ValueL2, triLhs.ValueL3 - triRhs.ValueL3),
    (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) => new SinglePhasicMeasure(singleLhs.Value - singleRhs.Value),
    _ => Null,
  };

  public static PhasicMeasure operator *(PhasicMeasure lhs, PhasicMeasure rhs) => (lhs, rhs) switch
  {
    (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new TriPhasicMeasure(triLhs.ValueL1 * triRhs.ValueL1, triLhs.ValueL2 * triRhs.ValueL2, triLhs.ValueL3 * triRhs.ValueL3),
    (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) => new SinglePhasicMeasure(singleLhs.Value * singleRhs.Value),
    _ => Null,
  };

  public static PhasicMeasure operator /(PhasicMeasure lhs, PhasicMeasure rhs) => (lhs, rhs) switch
  {
    (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new TriPhasicMeasure(triLhs.ValueL1 / triRhs.ValueL1, triLhs.ValueL2 / triRhs.ValueL2, triLhs.ValueL3 / triRhs.ValueL3),
    (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) => new SinglePhasicMeasure(singleLhs.Value / singleRhs.Value),
    _ => Null,
  };
}
