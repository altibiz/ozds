namespace Ozds.Business.Math;

public record class CompositePhasicMeasure(List<PhasicMeasure> Measures)
  : PhasicMeasure
{
  public T FromMostAccurate<T>(Func<PhasicMeasure, T> selector, T @default)
  {
    return Measures.FirstOrDefault(measure => measure is TriPhasicMeasure) is
      { } tri
      ? selector(tri)
      : Measures.FirstOrDefault(measure => measure is SinglePhasicMeasure) is
        { } single
        ? selector(single)
        : @default;
  }

  public CompositePhasicMeasure Select(
    Func<PhasicMeasure, PhasicMeasure> selector)
  {
    return new CompositePhasicMeasure(Measures.Select(selector).ToList());
  }

  public CompositePhasicMeasure Zip(PhasicMeasure other,
    Func<PhasicMeasure, PhasicMeasure, PhasicMeasure> selector)
  {
    return other switch
    {
      CompositePhasicMeasure otherComposite => new CompositePhasicMeasure(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositePhasicMeasure(Measures
        .Zip(Enumerable.Repeat(other, Measures.Count), selector)
        .ToList())
    };
  }
}

public record class TriPhasicMeasure(
  float ValueL1,
  float ValueL2,
  float ValueL3)
  : PhasicMeasure;

public record class SinglePhasicMeasure(float Value) : PhasicMeasure;

public record class NullPhasicMeasure : PhasicMeasure;

public abstract record class PhasicMeasure
{
  public static readonly PhasicMeasure Null = new NullPhasicMeasure();

  public float PhaseSum
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure composite => composite.FromMostAccurate(
          measure => measure.PhaseSum, 0),
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
        CompositePhasicMeasure composite => composite.FromMostAccurate(
          measure => measure.PhaseAverage, 0),
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
        CompositePhasicMeasure composite => composite.FromMostAccurate(
          measure => measure.PhasePeak, 0),
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

  public float PhaseTrough
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure composite => composite.FromMostAccurate(
          measure => measure.PhaseTrough, 0),
        TriPhasicMeasure tri => tri.ValueL1 < tri.ValueL2
          ? tri.ValueL1 < tri.ValueL3
            ? tri.ValueL1
            : tri.ValueL3
          : tri.ValueL2 < tri.ValueL3
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
      CompositePhasicMeasure composite => composite.Select(measure =>
        measure + rhs),
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
      CompositePhasicMeasure composite => composite.Select(measure =>
        measure - rhs),
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
      CompositePhasicMeasure composite => composite.Select(measure =>
        measure * rhs),
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
      CompositePhasicMeasure composite => composite.Select(measure =>
        measure / rhs),
      TriPhasicMeasure tri => new TriPhasicMeasure(tri.ValueL1 / rhs,
        tri.ValueL2 / rhs, tri.ValueL3 / rhs),
      SinglePhasicMeasure single => new SinglePhasicMeasure(single.Value / rhs),
      _ => Null
    };
  }

  public static PhasicMeasure operator +(float lhs, PhasicMeasure rhs)
  {
    return rhs switch
    {
      CompositePhasicMeasure composite => composite.Select(measure =>
        lhs + composite),
      TriPhasicMeasure tri => new TriPhasicMeasure(lhs + tri.ValueL1,
        lhs + tri.ValueL2, lhs + tri.ValueL3),
      SinglePhasicMeasure single => new SinglePhasicMeasure(lhs + single.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator -(float lhs, PhasicMeasure rhs)
  {
    return rhs switch
    {
      CompositePhasicMeasure composite => composite.Select(measure =>
        lhs - composite),
      TriPhasicMeasure tri => new TriPhasicMeasure(lhs - tri.ValueL1,
        lhs - tri.ValueL2, lhs - tri.ValueL3),
      SinglePhasicMeasure single => new SinglePhasicMeasure(lhs - single.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator *(float lhs, PhasicMeasure rhs)
  {
    return rhs switch
    {
      CompositePhasicMeasure composite => composite.Select(measure =>
        lhs * composite),
      TriPhasicMeasure tri => new TriPhasicMeasure(lhs * tri.ValueL1,
        lhs * tri.ValueL2, lhs * tri.ValueL3),
      SinglePhasicMeasure single => new SinglePhasicMeasure(lhs * single.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator /(float lhs, PhasicMeasure rhs)
  {
    return rhs switch
    {
      CompositePhasicMeasure composite => composite.Select(measure =>
        lhs / composite),
      TriPhasicMeasure tri => new TriPhasicMeasure(lhs / tri.ValueL1,
        lhs / tri.ValueL2, lhs / tri.ValueL3),
      SinglePhasicMeasure single => new SinglePhasicMeasure(lhs / single.Value),
      _ => Null
    };
  }

  public static PhasicMeasure operator +(PhasicMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositePhasicMeasure compositeRhs) =>
        compositeRhs.Zip(lhs, (rhs, lhs) => lhs + rhs),
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
      (CompositePhasicMeasure compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositePhasicMeasure compositeRhs) =>
        compositeRhs.Zip(lhs, (rhs, lhs) => lhs - rhs),
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
      (CompositePhasicMeasure compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositePhasicMeasure compositeRhs) =>
        compositeRhs.Zip(lhs, (rhs, lhs) => lhs * rhs),
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
      (CompositePhasicMeasure compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositePhasicMeasure compositeRhs) =>
        compositeRhs.Zip(lhs, (rhs, lhs) => lhs / rhs),
      (TriPhasicMeasure triLhs, TriPhasicMeasure triRhs) => new
        TriPhasicMeasure(triLhs.ValueL1 / triRhs.ValueL1,
          triLhs.ValueL2 / triRhs.ValueL2, triLhs.ValueL3 / triRhs.ValueL3),
      (SinglePhasicMeasure singleLhs, SinglePhasicMeasure singleRhs) =>
        new SinglePhasicMeasure(singleLhs.Value / singleRhs.Value),
      _ => Null
    };
  }
}
