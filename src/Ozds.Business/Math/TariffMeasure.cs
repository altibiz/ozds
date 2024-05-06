using System.Numerics;

namespace Ozds.Business.Math;

public record class CompositeTariffMeasure<T>
  : TariffMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public CompositeTariffMeasure(List<TariffMeasure<T>> measures)
  {
    Measures = measures.SelectMany(measure => measure switch
    {
      CompositeTariffMeasure<T> composite => composite.Measures,
      _ => [measure]
    }).ToList();
  }

  public List<TariffMeasure<T>> Measures { get; set; }

  public U FromMostAccurate<U>(Func<TariffMeasure<T>, U> selector, U @default)
  {
    return Measures.FirstOrDefault(measure => measure is BinaryTariffMeasure<T>)
      is
      { } binary
      ? selector(binary)
      : Measures.FirstOrDefault(measure => measure is UnaryTariffMeasure<T>) is
        { } unary
        ? selector(unary)
        : @default;
  }

  public CompositeTariffMeasure<T> Select(
    Func<TariffMeasure<T>, TariffMeasure<T>> selector)
  {
    return new CompositeTariffMeasure<T>(Measures.Select(selector).ToList());
  }

  public CompositeTariffMeasure<T> Zip(PhasicMeasure<T> other,
    Func<TariffMeasure<T>, PhasicMeasure<T>, TariffMeasure<T>> selector)
  {
    return other switch
    {
      CompositePhasicMeasure<T> otherComposite => new CompositeTariffMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(Measures
        .Zip(Enumerable.Repeat(other, Measures.Count), selector)
        .ToList())
    };
  }

  public CompositeTariffMeasure<T> Zip(DuplexMeasure<T> other,
    Func<TariffMeasure<T>, DuplexMeasure<T>, TariffMeasure<T>> selector)
  {
    return other switch
    {
      CompositeDuplexMeasure<T> otherComposite => new CompositeTariffMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(Measures
        .Zip(Enumerable.Repeat(other, Measures.Count), selector)
        .ToList())
    };
  }

  public CompositeTariffMeasure<T> Zip(TariffMeasure<T> other,
    Func<TariffMeasure<T>, TariffMeasure<T>, TariffMeasure<T>> selector)
  {
    return other switch
    {
      CompositeTariffMeasure<T> otherComposite => new CompositeTariffMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(Measures
        .Zip(Enumerable.Repeat(other, Measures.Count), selector)
        .ToList())
    };
  }
}

public static class CompositeTariffMeasureExtensions
{
  public static CompositeTariffMeasure<T> ZipTariff<T>(
    this CompositePhasicMeasure<T> lhs, TariffMeasure<T> rhs,
    Func<PhasicMeasure<T>, TariffMeasure<T>, TariffMeasure<T>> selector)
    where T : struct,
    IComparisonOperators<T, T, bool>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> rhsComposite => new CompositeTariffMeasure<T>(
        lhs.Measures.Zip(rhsComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(lhs.Measures
        .Select(measure => selector(measure, rhs)).ToList())
    };
  }

  public static CompositeTariffMeasure<T> ZipTariff<T>(
    this CompositeDuplexMeasure<T> lhs, TariffMeasure<T> rhs,
    Func<DuplexMeasure<T>, TariffMeasure<T>, TariffMeasure<T>> selector)
    where T : struct,
    IComparisonOperators<T, T, bool>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> rhsComposite => new CompositeTariffMeasure<T>(
        lhs.Measures.Zip(rhsComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(lhs.Measures
        .Select(measure => selector(measure, rhs)).ToList())
    };
  }
}

public record class BinaryTariffMeasure<T>(
  DuplexMeasure<T> T1,
  DuplexMeasure<T> T2)
  : TariffMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class UnaryTariffMeasure<T>(DuplexMeasure<T> T0)
  : TariffMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NullTariffMeasure<T> : TariffMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public abstract record class TariffMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public static readonly TariffMeasure<T> Null = new NullTariffMeasure<T>();

  public DuplexMeasure<T> TariffUnary
  {
    get
    {
      return this switch
      {
        CompositeTariffMeasure<T> composite =>
          new CompositeDuplexMeasure<T>(
            composite.Measures.Select(
              measure => measure.TariffUnary).ToList()),
        BinaryTariffMeasure<T> binary => binary.T1 + binary.T2,
        UnaryTariffMeasure<T> unary => unary.T0,
        _ => DuplexMeasure<T>.Null
      };
    }
  }

  public BinaryTariffMeasure<T> TariffBinary
  {
    get
    {
      return this switch
      {
        CompositeTariffMeasure<T> composite =>
          composite.Measures.FirstOrDefault(
              measure => measure is BinaryTariffMeasure<T>) is
            BinaryTariffMeasure<T>
            binary
            ? binary
            : new BinaryTariffMeasure<T>(DuplexMeasure<T>.Null,
              DuplexMeasure<T>.Null),
        BinaryTariffMeasure<T> binary => binary,
        _ => new BinaryTariffMeasure<T>(DuplexMeasure<T>.Null,
          DuplexMeasure<T>.Null)
      };
    }
  }

  public TariffMeasure<T> TariffAbs
  {
    get { return Select(phasic => phasic.DuplexAbs); }
  }

  public DuplexMeasure<T> TariffSum
  {
    get
    {
      return this switch
      {
        CompositeTariffMeasure<T> composite => composite.Measures.Aggregate(
          DuplexMeasure<T>.Null, (sum, measure) => sum + measure.TariffUnary),
        BinaryTariffMeasure<T> binary => binary.T1 + binary.T2,
        UnaryTariffMeasure<T> unary => unary.T0,
        _ => DuplexMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<U> ConvertPrimitiveTo<U>()
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    return this switch
    {
      CompositeTariffMeasure<T> composite => new CompositeTariffMeasure<U>(
        composite.Measures.Select(measure => measure.ConvertPrimitiveTo<U>())
          .ToList()),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<U>(
        binary.T1.ConvertPrimitiveTo<U>(), binary.T2.ConvertPrimitiveTo<U>()),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<U>(
        unary.T0.ConvertPrimitiveTo<U>()),
      _ => TariffMeasure<U>.Null
    };
  }

  public TariffMeasure<T> Select(
    Func<DuplexMeasure<T>, DuplexMeasure<T>> selector)
  {
    return this switch
    {
      CompositeTariffMeasure<T> composite => new CompositeTariffMeasure<T>(
        composite.Measures.Select(measure => measure.Select(selector))
          .ToList()),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        selector(binary.T1), selector(binary.T2)),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(
        selector(unary.T0)),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(TariffMeasure<T> lhs, T rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        measure + rhs),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(unary.T0 + rhs),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1 + rhs,
        binary.T2 + rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(TariffMeasure<T> lhs, T rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        measure - rhs),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(unary.T0 - rhs),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1 - rhs,
        binary.T2 - rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(TariffMeasure<T> lhs, T rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        measure * rhs),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(unary.T0 * rhs),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1 * rhs,
        binary.T2 * rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(TariffMeasure<T> lhs, T rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        measure / rhs),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(unary.T0 / rhs),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1 / rhs,
        binary.T2 / rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(T lhs, TariffMeasure<T> rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        lhs + measure),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(lhs + unary.T0),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        lhs + binary.T1,
        lhs + binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(T lhs, TariffMeasure<T> rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        lhs - measure),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(lhs - unary.T0),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        lhs - binary.T1,
        lhs - binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(T lhs, TariffMeasure<T> rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        lhs * measure),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(lhs * unary.T0),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        lhs * binary.T1,
        lhs * binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(T lhs, TariffMeasure<T> rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(measure =>
        lhs / measure),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(lhs / unary.T0),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        lhs / binary.T1,
        lhs / binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(TariffMeasure<T> lhs,
    PhasicMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositePhasicMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs + rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 + rhs, binary.T2 + rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 +
        rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(TariffMeasure<T> lhs,
    PhasicMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositePhasicMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs - rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 - rhs, binary.T2 - rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 -
        rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(TariffMeasure<T> lhs,
    PhasicMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositePhasicMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs * rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 * rhs, binary.T2 * rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 *
        rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(TariffMeasure<T> lhs,
    PhasicMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositePhasicMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs / rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 / rhs, binary.T2 / rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 /
        rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(PhasicMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs + rhs),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs + binary.T1, lhs + binary.T2),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs + unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(PhasicMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs - rhs),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs - binary.T1, lhs - binary.T2),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs - unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(PhasicMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs * rhs),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs * binary.T1, lhs * binary.T2),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs * unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(PhasicMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs / rhs),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs / binary.T1, lhs * binary.T2),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs / unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(TariffMeasure<T> lhs,
    DuplexMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositeDuplexMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs + rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 +
        rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 + rhs,
        binary.T2 + rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(TariffMeasure<T> lhs,
    DuplexMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositeDuplexMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs - rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 -
        rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 - rhs,
        binary.T2 - rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(TariffMeasure<T> lhs,
    DuplexMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositeDuplexMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs * rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 *
        rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 * rhs,
        binary.T2 * rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(TariffMeasure<T> lhs,
    DuplexMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositeDuplexMeasure<T> composite) => composite.ZipTariff(lhs,
        (rhs, lhs) => lhs / rhs),
      (UnaryTariffMeasure<T> unary, _) => new UnaryTariffMeasure<T>(unary.T0 /
        rhs),
      (BinaryTariffMeasure<T> binary, _) => new BinaryTariffMeasure<T>(
        binary.T1 / rhs,
        binary.T2 / rhs),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(DuplexMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs + rhs),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs + unary.T0),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs + binary.T1,
        lhs + binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(DuplexMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs - rhs),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs - unary.T0),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs - binary.T1,
        lhs - binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(DuplexMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs * rhs),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs * unary.T0),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs * binary.T1,
        lhs * binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(DuplexMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure<T> composite, _) => composite.ZipTariff(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs / rhs),
      (_, UnaryTariffMeasure<T> unary) => new UnaryTariffMeasure<T>(
        lhs / unary.T0),
      (_, BinaryTariffMeasure<T> binary) => new BinaryTariffMeasure<T>(
        lhs / binary.T1,
        lhs / binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator +(TariffMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs + rhs),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0 + right.T0),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(left.T1 + right.T1, left.T2 + right.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator -(TariffMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs - rhs),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0 - right.T0),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(left.T1 - right.T1, left.T2 - right.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator *(TariffMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs * rhs),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0 * right.T0),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(left.T1 * right.T1, left.T2 * right.T2),
      _ => Null
    };
  }

  public static TariffMeasure<T> operator /(TariffMeasure<T> lhs,
    TariffMeasure<T> rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs / rhs),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0 / right.T0),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(left.T1 / right.T1, left.T2 / right.T2),
      _ => Null
    };
  }
}
