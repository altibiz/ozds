using System.Numerics;

namespace Ozds.Business.Math;

// TODO: convert properties to methods and create proper class hierarchy
#pragma warning disable S2365
#pragma warning disable S3060

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
    Measures = measures.SelectMany(
      measure => measure switch
      {
        CompositeTariffMeasure<T> composite => composite.Measures,
        _ => [measure]
      }).ToList();
  }

  public List<TariffMeasure<T>> Measures { get; set; }

  public CompositeTariffMeasure<T> Select(
    Func<TariffMeasure<T>, TariffMeasure<T>> selector)
  {
    return new CompositeTariffMeasure<T>(Measures.Select(selector).ToList());
  }

  public CompositeTariffMeasure<T> Zip(
    TariffMeasure<T> other,
    Func<TariffMeasure<T>, TariffMeasure<T>, TariffMeasure<T>> selector)
  {
    return other switch
    {
      CompositeTariffMeasure<T> otherComposite => new CompositeTariffMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure<T>(
        Measures
          .Zip(Enumerable.Repeat(other, Measures.Count), selector)
          .ToList())
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

  public DuplexMeasure<T> TariffUnary()
  {
    {
      return this switch
      {
        CompositeTariffMeasure<T> composite =>
          new CompositeDuplexMeasure<T>(
            composite.Measures.Select(
              measure => measure.TariffUnary()).ToList()),
        BinaryTariffMeasure<T> binary => binary.T1.Add(binary.T2),
        UnaryTariffMeasure<T> unary => unary.T0,
        _ => DuplexMeasure<T>.Null
      };
    }
  }

  public BinaryTariffMeasure<T> TariffBinary()
  {
    {
      return this switch
      {
        CompositeTariffMeasure<T> composite =>
          composite.Measures.FirstOrDefault(
              measure => measure is BinaryTariffMeasure<T>) is
            BinaryTariffMeasure<T>
            binary
            ? binary
            : new BinaryTariffMeasure<T>(
              DuplexMeasure<T>.Null,
              DuplexMeasure<T>.Null),
        BinaryTariffMeasure<T> binary => binary,
        _ => new BinaryTariffMeasure<T>(
          DuplexMeasure<T>.Null,
          DuplexMeasure<T>.Null)
      };
    }
  }

  public TariffMeasure<TConverted> ConvertPrimitiveTo<TConverted>()
    where TConverted : struct,
    IComparisonOperators<TConverted, TConverted, bool>,
    IAdditionOperators<TConverted, TConverted, TConverted>,
    ISubtractionOperators<TConverted, TConverted, TConverted>,
    IMultiplyOperators<TConverted, TConverted, TConverted>,
    IDivisionOperators<TConverted, TConverted, TConverted>
  {
    return this switch
    {
      CompositeTariffMeasure<T> composite => new
        CompositeTariffMeasure<TConverted>(
          composite.Measures.Select(
              measure => measure.ConvertPrimitiveTo<TConverted>())
            .ToList()),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<TConverted>(
        binary.T1.ConvertPrimitiveTo<TConverted>(),
        binary.T2.ConvertPrimitiveTo<TConverted>()),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<TConverted>(
        unary.T0.ConvertPrimitiveTo<TConverted>()),
      _ => TariffMeasure<TConverted>.Null
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

  public TariffMeasure<T> Multiply(T rhs)
  {
    return this switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(
        measure =>
          measure.Multiply(rhs)),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(
        unary.T0.Multiply(rhs)),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1.Multiply(rhs),
        binary.T2.Multiply(rhs)),
      _ => Null
    };
  }

  public TariffMeasure<T> Divide(T rhs)
  {
    return this switch
    {
      CompositeTariffMeasure<T> composite => composite.Select(
        measure =>
          measure.Divide(rhs)),
      UnaryTariffMeasure<T> unary => new UnaryTariffMeasure<T>(
        unary.T0.Divide(rhs)),
      BinaryTariffMeasure<T> binary => new BinaryTariffMeasure<T>(
        binary.T1.Divide(rhs),
        binary.T2.Divide(rhs)),
      _ => Null
    };
  }

  public TariffMeasure<T> Add(TariffMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(
        rhs,
        (lhs, rhs) => lhs.Add(rhs)),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(
        this,
        (rhs, lhs) => lhs.Add(rhs)),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0.Add(right.T0)),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(left.T1.Add(right.T1), left.T2.Add(right.T2)),
      _ => Null
    };
  }

  public TariffMeasure<T> Subtract(TariffMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(
        rhs,
        (lhs, rhs) => lhs.Subtract(rhs)),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(
        this,
        (rhs, lhs) => lhs.Subtract(rhs)),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0.Subtract(right.T0)),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(
          left.T1.Subtract(right.T1),
          left.T2.Subtract(right.T2)),
      _ => Null
    };
  }

  public TariffMeasure<T> Multiply(TariffMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeTariffMeasure<T> composite, _) => composite.Zip(
        rhs,
        (lhs, rhs) => lhs.Multiply(rhs)),
      (_, CompositeTariffMeasure<T> composite) => composite.Zip(
        this,
        (rhs, lhs) => lhs.Multiply(rhs)),
      (UnaryTariffMeasure<T> left, UnaryTariffMeasure<T> right) =>
        new UnaryTariffMeasure<T>(left.T0.Multiply(right.T0)),
      (BinaryTariffMeasure<T> left, BinaryTariffMeasure<T> right) => new
        BinaryTariffMeasure<T>(
          left.T1.Multiply(right.T1),
          left.T2.Multiply(right.T2)),
      _ => Null
    };
  }
}
