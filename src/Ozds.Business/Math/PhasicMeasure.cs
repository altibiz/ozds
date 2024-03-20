using System.Numerics;

namespace Ozds.Business.Math;

public record class TriPhasicMeasure(float ValueL1, float ValueL2, float ValueL3)
  : PhasicMeasure;

public record class SinglePhasicMeasure(float Value) : PhasicMeasure;

public record class PhasicMeasure :
  IAdditionOperators<PhasicMeasure, float, PhasicMeasure>,
  ISubtractionOperators<PhasicMeasure, float, PhasicMeasure>,
  IMultiplyOperators<PhasicMeasure, float, PhasicMeasure>,
  IDivisionOperators<PhasicMeasure, float, PhasicMeasure>,
  IAdditionOperators<PhasicMeasure, PhasicMeasure, PhasicMeasure>,
  ISubtractionOperators<PhasicMeasure, PhasicMeasure, PhasicMeasure>,
  IMultiplyOperators<PhasicMeasure, PhasicMeasure, PhasicMeasure>,
  IDivisionOperators<PhasicMeasure, PhasicMeasure, PhasicMeasure>
{
  public static readonly PhasicMeasure Null = new();

  public float PhaseSum
  {
    get
    {
      return this switch
      {
        TriPhasicMeasure tri => tri.ValueL1 + tri.ValueL2 + tri.ValueL3,
        SinglePhasicMeasure single => single.Value,
        _ => 0
      };
    }
  }

  public float PhaseAverage
  {
    get
    {
      return this switch
      {
        TriPhasicMeasure tri => (tri.ValueL1 + tri.ValueL2 + tri.ValueL3) / 3,
        SinglePhasicMeasure single => single.Value,
        _ => 0
      };
    }
  }

  public float PhasePeak
  {
    get
    {
      return this switch
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
    }
  }

  public static PhasicMeasure operator +(PhasicMeasure lhs, float rhs)
  {
    return lhs switch
    {
      TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 + rhs,
        tri.ValueL2 + rhs, tri.ValueL3 + rhs),
      SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value + rhs),
      _ => Null
    };
  }

  public static PhasicMeasure operator -(PhasicMeasure lhs, float rhs)
  {
    return lhs switch
    {
      TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 - rhs,
        tri.ValueL2 - rhs, tri.ValueL3 - rhs),
      SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value - rhs),
      _ => Null
    };
  }

  public static PhasicMeasure operator *(PhasicMeasure lhs, float rhs)
  {
    return lhs switch
    {
      TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 * rhs,
        tri.ValueL2 * rhs, tri.ValueL3 * rhs),
      SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value * rhs),
      _ => Null
    };
  }

  public static PhasicMeasure operator /(PhasicMeasure lhs, float rhs)
  {
    return lhs switch
    {
      TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 / rhs,
        tri.ValueL2 / rhs, tri.ValueL3 / rhs),
      SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value / rhs),
      _ => Null
    };
  }

  public static PhasicMeasure operator +(PhasicMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new
        TriPhasicMeasure(triLhs.ValueL1 + triRhs.ValueL1,
          triLhs.ValueL2 + triRhs.ValueL2, triLhs.ValueL3 + triRhs.ValueL3),
      (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) =>
        new SinglePhasicMeasure(singleLhs.Value + singleRhs.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator -(PhasicMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new
        TriPhasicMeasure(triLhs.ValueL1 - triRhs.ValueL1,
          triLhs.ValueL2 - triRhs.ValueL2, triLhs.ValueL3 - triRhs.ValueL3),
      (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) =>
        new SinglePhasicMeasure(singleLhs.Value - singleRhs.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator *(PhasicMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new
        TriPhasicMeasure(triLhs.ValueL1 * triRhs.ValueL1,
          triLhs.ValueL2 * triRhs.ValueL2, triLhs.ValueL3 * triRhs.ValueL3),
      (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) =>
        new SinglePhasicMeasure(singleLhs.Value * singleRhs.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator /(PhasicMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new
        TriPhasicMeasure(triLhs.ValueL1 / triRhs.ValueL1,
          triLhs.ValueL2 / triRhs.ValueL2, triLhs.ValueL3 / triRhs.ValueL3),
      (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) =>
        new SinglePhasicMeasure(singleLhs.Value / singleRhs.Value),
      _ => Null
    };
  }
}
