using System.Numerics;

namespace Ozds.Business.Math;

#pragma warning disable S3060

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
    Measures = measures.SelectMany(
      measure => measure switch
      {
        CompositePhasicMeasure<T> composite => composite.Measures,
        _ => [measure]
      }).ToList();
  }

  public List<PhasicMeasure<T>> Measures { get; set; }

  public CompositePhasicMeasure<T> Select(
    Func<PhasicMeasure<T>, PhasicMeasure<T>> selector)
  {
    return new CompositePhasicMeasure<T>(Measures.Select(selector).ToList());
  }

  public CompositePhasicMeasure<T> Zip(
    PhasicMeasure<T> other,
    Func<PhasicMeasure<T>, PhasicMeasure<T>, PhasicMeasure<T>> selector)
  {
    return other switch
    {
      CompositePhasicMeasure<T> otherComposite => new CompositePhasicMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositePhasicMeasure<T>(
        Measures
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

public record class SinglePhasicMeasureSum<T>(T Value)
  : SinglePhasicMeasure<T>(Value)
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

  public T PhaseSum()
  {
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite =>
          composite.Measures.OrderBy(
            measure => measure switch
            {
              SinglePhasicMeasureSum<T> => 0,
              TriPhasicMeasure<T> => 1,
              _ => 2
            })
            .Select(measure => measure.PhaseSum())
            .FirstOrDefault(
            value => !EqualityComparer<T>.Default.Equals(
              value, (T)Convert.ChangeType(0, typeof(T))),
            (T)Convert.ChangeType(0, typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 + tri.ValueL2 + tri.ValueL3,
        SinglePhasicMeasureSum<T> single => single.Value,
        _ => (T)Convert.ChangeType(0, typeof(T))
      };
    }
  }

  public T PhaseAverage()
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite =>
        composite.Measures.OrderBy(
          measure => measure switch
          {
            TriPhasicMeasure<T> => 0,
            SinglePhasicMeasureSum<T> => 1,
            _ => 2
          })
          .Select(measure => measure.PhaseAverage())
          .FirstOrDefault(
          value => !EqualityComparer<T>.Default.Equals(
            value, (T)Convert.ChangeType(0, typeof(T))),
          (T)Convert.ChangeType(0, typeof(T))),
      TriPhasicMeasure<T> tri => (tri.ValueL1 + tri.ValueL2 + tri.ValueL3)
        / (T)Convert.ChangeType(3, typeof(T)),
      SinglePhasicMeasureSum<T> single => single.Value,
      _ => (T)Convert.ChangeType(0, typeof(T))
    };
  }

  public T PhasePeak()
  {
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite =>
          composite.Measures.OrderBy(
            measure => measure switch
            {
              TriPhasicMeasure<T> => 0,
              SinglePhasicMeasureSum<T> => 1,
              _ => 2
            })
            .Select(measure => measure.PhasePeak())
            .FirstOrDefault(
            value => !EqualityComparer<T>.Default.Equals(
              value, (T)Convert.ChangeType(0, typeof(T))),
            (T)Convert.ChangeType(0, typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 > tri.ValueL2
          ? tri.ValueL1 > tri.ValueL3 ? tri.ValueL1 : tri.ValueL3
          : tri.ValueL2 > tri.ValueL3
            ? tri.ValueL2
            : tri.ValueL3,
        SinglePhasicMeasureSum<T> single => single.Value,
        _ => (T)Convert.ChangeType(0, typeof(T))
      };
    }
  }

  public T PhaseTrough()
  {
    {
      return this switch
      {
        CompositePhasicMeasure<T> composite =>
          composite.Measures.OrderBy(
            measure => measure switch
            {
              TriPhasicMeasure<T> => 0,
              SinglePhasicMeasureSum<T> => 1,
              _ => 2
            })
            .Select(measure => measure.PhaseTrough())
            .FirstOrDefault(
            value => !EqualityComparer<T>.Default.Equals(
              value, (T)Convert.ChangeType(0, typeof(T))),
            (T)Convert.ChangeType(0, typeof(T))),
        TriPhasicMeasure<T> tri => tri.ValueL1 < tri.ValueL2
          ? tri.ValueL1 < tri.ValueL3 ? tri.ValueL1 : tri.ValueL3
          : tri.ValueL2 < tri.ValueL3
            ? tri.ValueL2
            : tri.ValueL3,
        SinglePhasicMeasureSum<T> single => single.Value,
        _ => (T)Convert.ChangeType(0, typeof(T))
      };
    }
  }

  public SinglePhasicMeasureSum<T> PhaseSingle()
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Measures.OrderBy(
          measure => measure switch
          {
            SinglePhasicMeasureSum<T> => 0,
            TriPhasicMeasure<T> => 1,
            _ => 2
          })
          .Select(measure => measure.PhaseSingle())
          .FirstOrDefault(
          single => !EqualityComparer<T>.Default.Equals(
            single.Value, (T)Convert.ChangeType(0, typeof(T))),
          new SinglePhasicMeasureSum<T>(default)),
      SinglePhasicMeasureSum<T> single => single,
      _ => new SinglePhasicMeasureSum<T>(default)
    };
  }

  public TriPhasicMeasure<T> PhaseSplit()
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Measures.OrderBy(
          measure => measure switch
          {
            TriPhasicMeasure<T> => 0,
            SinglePhasicMeasureSum<T> => 1,
            _ => 2
          })
          .Select(measure => measure.PhaseSplit())
          .FirstOrDefault(
          tri => !EqualityComparer<T>.Default.Equals(
              tri.ValueL1, (T)Convert.ChangeType(0, typeof(T))) &&
            !EqualityComparer<T>.Default.Equals(
              tri.ValueL2, (T)Convert.ChangeType(0, typeof(T))) &&
            !EqualityComparer<T>.Default.Equals(
              tri.ValueL3, (T)Convert.ChangeType(0, typeof(T))),
          new TriPhasicMeasure<T>(
            (T)Convert.ChangeType(0, typeof(T)),
            (T)Convert.ChangeType(0, typeof(T)),
            (T)Convert.ChangeType(0, typeof(T)))),
      SinglePhasicMeasureSum<T> single => new TriPhasicMeasure<T>(
        single.Value / (T)Convert.ChangeType(3, typeof(T)),
        single.Value / (T)Convert.ChangeType(3, typeof(T)),
        single.Value / (T)Convert.ChangeType(3, typeof(T))),
      TriPhasicMeasure<T> tri => tri,
      _ => new TriPhasicMeasure<T>(
        (T)Convert.ChangeType(0, typeof(T)),
        (T)Convert.ChangeType(0, typeof(T)),
        (T)Convert.ChangeType(0, typeof(T)))
    };
  }

  public PhasicMeasure<T> PhaseAbs()
  {
    {
      return Select(
        value =>
          (T)Convert.ChangeType(
            System.Math.Abs(Convert.ToDecimal(value)),
            typeof(T))
      );
    }
  }

  public PhasicMeasure<TConverted> ConvertPrimitiveTo<TConverted>()
    where TConverted : struct,
    IComparisonOperators<TConverted, TConverted, bool>,
    IAdditionOperators<TConverted, TConverted, TConverted>,
    ISubtractionOperators<TConverted, TConverted, TConverted>,
    IMultiplyOperators<TConverted, TConverted, TConverted>,
    IDivisionOperators<TConverted, TConverted, TConverted>
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => new
        CompositePhasicMeasure<TConverted>(
          composite.Measures.Select(
              measure => measure.ConvertPrimitiveTo<TConverted>())
            .ToList()),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<TConverted>(
        (TConverted)Convert.ChangeType(tri.ValueL1, typeof(TConverted)),
        (TConverted)Convert.ChangeType(tri.ValueL2, typeof(TConverted)),
        (TConverted)Convert.ChangeType(tri.ValueL3, typeof(TConverted))),
      SinglePhasicMeasureSum<T> single => new
        SinglePhasicMeasureSum<TConverted>(
          (TConverted)Convert.ChangeType(single.Value, typeof(TConverted))),
      _ => new NullPhasicMeasure<TConverted>()
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
      SinglePhasicMeasureSum<T> single => new SinglePhasicMeasureSum<T>(
        selector(single.Value)),
      _ => Null
    };
  }

  public PhasicMeasure<T> Multiply(T rhs)
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Select(
        measure =>
          measure * rhs),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<T>(
        tri.ValueL1 * rhs,
        tri.ValueL2 * rhs, tri.ValueL3 * rhs),
      SinglePhasicMeasureSum<T> single => new SinglePhasicMeasureSum<T>(
        single.Value *
        rhs),
      _ => Null
    };
  }

  public PhasicMeasure<T> Divide(T rhs)
  {
    return this switch
    {
      CompositePhasicMeasure<T> composite => composite.Select(
        measure =>
          measure / rhs),
      TriPhasicMeasure<T> tri => new TriPhasicMeasure<T>(
        tri.ValueL1 / rhs,
        tri.ValueL2 / rhs, tri.ValueL3 / rhs),
      SinglePhasicMeasureSum<T> single => new SinglePhasicMeasureSum<T>(
        single.Value /
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
        TriPhasicMeasure<T>(
          triLhs.ValueL1 + triRhs.ValueL1,
          triLhs.ValueL2 + triRhs.ValueL2, triLhs.ValueL3 + triRhs.ValueL3),
      (SinglePhasicMeasureSum<T> singleLhs, SinglePhasicMeasureSum<T> singleRhs)
        =>
        new SinglePhasicMeasureSum<T>(singleLhs.Value + singleRhs.Value),
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
        TriPhasicMeasure<T>(
          triLhs.ValueL1 - triRhs.ValueL1,
          triLhs.ValueL2 - triRhs.ValueL2, triLhs.ValueL3 - triRhs.ValueL3),
      (SinglePhasicMeasureSum<T> singleLhs, SinglePhasicMeasureSum<T> singleRhs)
        =>
        new SinglePhasicMeasureSum<T>(singleLhs.Value - singleRhs.Value),
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
        TriPhasicMeasure<T>(
          triLhs.ValueL1 * triRhs.ValueL1,
          triLhs.ValueL2 * triRhs.ValueL2, triLhs.ValueL3 * triRhs.ValueL3),
      (SinglePhasicMeasureSum<T> singleLhs, SinglePhasicMeasureSum<T> singleRhs)
        =>
        new SinglePhasicMeasureSum<T>(singleLhs.Value * singleRhs.Value),
      _ => Null
    };
  }
}
