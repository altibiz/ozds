using System.Numerics;

namespace Ozds.Business.Math;

public record class CompositePhasicMeasure<T>
  : PhasicMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public CompositePhasicMeasure(List<PhasicMeasure<T>> measures)
  {
    Measures = measures.SelectMany(measure => measure switch
    {
      CompositePhasicMeasure<T> composite => composite.Measures,
      _ => [measure]
    }).ToList();
  }

  public List<PhasicMeasure<T>> Measures { get; set; }

  public U FromMostAccurate<U>(Func<PhasicMeasure<T>, U> selector, U @default)
  {
    return Measures.FirstOrDefault(measure =>
        measure is TriPhasicMeasure<T> or SinglePhasicMeasure<T>) is
      { } singleOrTri
      ? selector(singleOrTri)
      : @default;
  }

  public CompositePhasicMeasure<T> Select(
    Func<PhasicMeasure<T>, PhasicMeasure<T>> selector)
  {
    return new CompositePhasicMeasure<T>(Measures.Select(selector).ToList());
  }

  public CompositePhasicMeasure<T> Zip(PhasicMeasure<T> other,
    Func<PhasicMeasure<T>, PhasicMeasure<T>, PhasicMeasure<T>> selector)
  {
    return other switch
    {
      CompositePhasicMeasure<T> otherComposite => new CompositePhasicMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositePhasicMeasure<T>(Measures
        .Zip(Enumerable.Repeat(other, Measures.Count), selector)
        .ToList())
    };
  }
}

public record class TriPhasicMeasure<T>(
  T ValueL1,
  T ValueL2,
  T ValueL3)
  : PhasicMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class SinglePhasicMeasure<T>(T Value) : PhasicMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NullPhasicMeasure<T> : PhasicMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public abstract record class PhasicMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public static readonly PhasicMeasure<T> Null = new NullPhasicMeasure<T>();

  public T PhaseSum
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhaseSum,
          (T)Convert.ChangeType((T)Convert.ChangeType(0, typeof(T)),
            typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 + tri.ValueL2 + tri.ValueL3,
        SinglePhasicMeasure<T> single => single.Value,
        _ => (T)Convert.ChangeType((T)Convert.ChangeType(0, typeof(T)),
          typeof(T))
      };
    }
  }

  public T PhaseAverage
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhaseAverage,
          (T)Convert.ChangeType((T)Convert.ChangeType(0, typeof(T)),
            typeof(T))),
        TriPhasicMeasure<T> tri => (tri.ValueL1 + tri.ValueL2 + tri.ValueL3) /
                                   (T)Convert.ChangeType(3, typeof(T)),
        SinglePhasicMeasure<T> single => single.Value,
        _ => (T)Convert.ChangeType((T)Convert.ChangeType(0, typeof(T)),
          typeof(T))
      };
    }
  }

  public T PhasePeak
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhasePeak, (T)Convert.ChangeType(0, typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 > tri.ValueL2
          ? tri.ValueL1 > tri.ValueL3
            ? tri.ValueL1
            : tri.ValueL3
          : tri.ValueL2 > tri.ValueL3
            ? tri.ValueL2
            : tri.ValueL3,
        SinglePhasicMeasure<T> single => single.Value,
        _ => (T)Convert.ChangeType(0, typeof(T))
      };
    }
  }

  public T PhaseTrough
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhaseTrough, (T)Convert.ChangeType(0, typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 < tri.ValueL2
          ? tri.ValueL1 < tri.ValueL3
            ? tri.ValueL1
            : tri.ValueL3
          : tri.ValueL2 < tri.ValueL3
            ? tri.ValueL2
            : tri.ValueL3,
        SinglePhasicMeasure<T> single => single.Value,
        _ => (T)Convert.ChangeType(0, typeof(T))
      };
    }
  }

  public SinglePhasicMeasure<T> PhaseSingle
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhaseSingle, new SinglePhasicMeasure<T>(default)),
        SinglePhasicMeasure<T> single => single,
        _ => new SinglePhasicMeasure<T>(default)
      };
    }
  }

  public TriPhasicMeasure<T> PhaseSplit
  {
    get
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.PhaseSplit, new TriPhasicMeasure<T>(default,
            default,
            default)),
        SinglePhasicMeasure<T> single => new TriPhasicMeasure<T>(single.Value,
          single.Value, single.Value),
        TriPhasicMeasure<T> tri => tri,
        _ => new TriPhasicMeasure<T>(default, default, default)
      };
    }
  }

  public PhasicMeasure<T> PhaseAbs
  {
    get
    {
      return Select(
        value =>
          (T)Convert.ChangeType(System.Math.Abs(Convert.ToDecimal(value)),
            typeof(T))
      );
    }
  }

  public PhasicMeasure<U> ConvertPrimitiveTo<U>()
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => new CompositePhasicMeasure<U>(
        composite.Measures.Select(measure => measure.ConvertPrimitiveTo<U>())
          .ToList()),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<U>(
        (U)Convert.ChangeType(tri.ValueL1, typeof(U)),
        (U)Convert.ChangeType(tri.ValueL2, typeof(U)),
        (U)Convert.ChangeType(tri.ValueL3, typeof(U))),
      SinglePhasicMeasure<T> single => new SinglePhasicMeasure<U>(
        (U)Convert.ChangeType(single.Value, typeof(U))),
      _ => new NullPhasicMeasure<U>()
    };
  }

  public PhasicMeasure<T> Select(Func<T, T> selector)
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => new CompositePhasicMeasure<T>(
        composite.Measures.Select(measure => measure.Select(selector))
          .ToList()),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<T>(
        selector(tri.ValueL1),
        selector(tri.ValueL2),
        selector(tri.ValueL3)),
      SinglePhasicMeasure<T> single => new SinglePhasicMeasure<T>(
        selector(single.Value)),
      _ => Null
    };
  }

  public PhasicMeasure<T> Multiply(T rhs)
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Select(measure =>
        measure * rhs),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<T>(tri.ValueL1 * rhs,
        tri.ValueL2 * rhs, tri.ValueL3 * rhs),
      SinglePhasicMeasure<T> single => new SinglePhasicMeasure<T>(single.Value *
        rhs),
      _ => Null
    };
  }

  public PhasicMeasure<T> Divide(T rhs)
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Select(measure =>
        measure / rhs),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<T>(tri.ValueL1 / rhs,
        tri.ValueL2 / rhs, tri.ValueL3 / rhs),
      SinglePhasicMeasure<T> single => new SinglePhasicMeasure<T>(single.Value /
        rhs),
      _ => Null
    };
  }

  public PhasicMeasure<T> Add(PhasicMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositePhasicMeasure<T> compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs.Add(rhs)),
      (_, CompositePhasicMeasure<T> compositeRhs) =>
        compositeRhs.Zip(this, (rhs, lhs) => lhs.Add(rhs)),
      (TriPhasicMeasure<T> triLhs, TriPhasicMeasure<T> triRhs) => new
        TriPhasicMeasure<T>(triLhs.ValueL1 + triRhs.ValueL1,
          triLhs.ValueL2 + triRhs.ValueL2, triLhs.ValueL3 + triRhs.ValueL3),
      (SinglePhasicMeasure<T> singleLhs, SinglePhasicMeasure<T> singleRhs) =>
        new SinglePhasicMeasure<T>(singleLhs.Value + singleRhs.Value),
      _ => Null
    };
  }

  public PhasicMeasure<T> Subtract(PhasicMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositePhasicMeasure<T> compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs.Subtract(rhs)),
      (_, CompositePhasicMeasure<T> compositeRhs) =>
        compositeRhs.Zip(this, (rhs, lhs) => lhs.Subtract(rhs)),
      (TriPhasicMeasure<T> triLhs, TriPhasicMeasure<T> triRhs) => new
        TriPhasicMeasure<T>(triLhs.ValueL1 - triRhs.ValueL1,
          triLhs.ValueL2 - triRhs.ValueL2, triLhs.ValueL3 - triRhs.ValueL3),
      (SinglePhasicMeasure<T> singleLhs, SinglePhasicMeasure<T> singleRhs) =>
        new SinglePhasicMeasure<T>(singleLhs.Value - singleRhs.Value),
      _ => Null
    };
  }

  public PhasicMeasure<T> Multiply(PhasicMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositePhasicMeasure<T> compositeLhs, _) =>
        compositeLhs.Zip(rhs, (lhs, rhs) => lhs.Multiply(rhs)),
      (_, CompositePhasicMeasure<T> compositeRhs) =>
        compositeRhs.Zip(this, (rhs, lhs) => lhs.Multiply(rhs)),
      (TriPhasicMeasure<T> triLhs, TriPhasicMeasure<T> triRhs) => new
        TriPhasicMeasure<T>(triLhs.ValueL1 * triRhs.ValueL1,
          triLhs.ValueL2 * triRhs.ValueL2, triLhs.ValueL3 * triRhs.ValueL3),
      (SinglePhasicMeasure<T> singleLhs, SinglePhasicMeasure<T> singleRhs) =>
        new SinglePhasicMeasure<T>(singleLhs.Value * singleRhs.Value),
      _ => Null
    };
  }
}
