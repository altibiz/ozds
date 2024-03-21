using System.Numerics;

namespace Ozds.Business.Math;

public record class CompositeTariffMeasure(List<TariffMeasure> Measures) : TariffMeasure
{
  public T FromMostAccurate<T>(Func<TariffMeasure, T> selector, T @default) =>
    Measures.FirstOrDefault(measure => measure is BinaryTariffMeasure) is { } binary
      ? selector(binary)
      : Measures.FirstOrDefault(measure => measure is UnaryTariffMeasure) is { } unary
      ? selector(unary)
      : @default;

  public CompositeTariffMeasure Select(Func<TariffMeasure, TariffMeasure> selector) =>
    new(Measures.Select(selector).ToList());

  public CompositeTariffMeasure Zip(PhasicMeasure other,
    Func<TariffMeasure, PhasicMeasure, TariffMeasure> selector) =>
    other switch
    {
      CompositePhasicMeasure otherComposite => new(Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new(Measures.Zip(Enumerable.Repeat(other, Measures.Count), selector).ToList())
    };

  public CompositeTariffMeasure Zip(DuplexMeasure other,
    Func<TariffMeasure, DuplexMeasure, TariffMeasure> selector) =>
    other switch
    {
      CompositeDuplexMeasure otherComposite => new(Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new(Measures.Zip(Enumerable.Repeat(other, Measures.Count), selector).ToList())
    };

  public CompositeTariffMeasure Zip(TariffMeasure other,
    Func<TariffMeasure, TariffMeasure, TariffMeasure> selector) =>
    other switch
    {
      CompositeTariffMeasure otherComposite => new(Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new(Measures.Zip(Enumerable.Repeat(other, Measures.Count), selector).ToList())
    };
}

public static class CompositeTariffMeasureExtensions
{
  public static CompositeTariffMeasure ZipTariff(this CompositePhasicMeasure lhs, TariffMeasure rhs,
    Func<PhasicMeasure, TariffMeasure, TariffMeasure> selector) =>
    rhs switch
    {
      CompositeTariffMeasure rhsComposite => new CompositeTariffMeasure(lhs.Measures.Zip(rhsComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure(lhs.Measures.Select(measure => selector(measure, rhs)).ToList())
    };

  public static CompositeTariffMeasure ZipTariff(this CompositeDuplexMeasure lhs, TariffMeasure rhs,
    Func<DuplexMeasure, TariffMeasure, TariffMeasure> selector) =>
    rhs switch
    {
      CompositeTariffMeasure rhsComposite => new CompositeTariffMeasure(lhs.Measures.Zip(rhsComposite.Measures, selector).ToList()),
      _ => new CompositeTariffMeasure(lhs.Measures.Select(measure => selector(measure, rhs)).ToList())
    };
}

public record class BinaryTariffMeasure(DuplexMeasure T1, DuplexMeasure T2)
  : TariffMeasure;

public record class UnaryTariffMeasure(DuplexMeasure T0) : TariffMeasure;

public record class NullTariffMeasure() : TariffMeasure;

public abstract record class TariffMeasure
{
  public static readonly TariffMeasure Null = new NullTariffMeasure();

  public DuplexMeasure TariffUnary
  {
    get
    {
      return this switch
      {
        CompositeTariffMeasure composite => composite.Measures.FirstOrDefault(measure => measure is UnaryTariffMeasure) is UnaryTariffMeasure unary
          ? unary.T0
          : DuplexMeasure.Null,
        BinaryTariffMeasure binary => binary.T1 + binary.T2,
        UnaryTariffMeasure unary => unary.T0,
        _ => DuplexMeasure.Null
      };
    }
  }

  public BinaryTariffMeasure TariffBinary
  {
    get
    {
      return this switch
      {
        CompositeTariffMeasure composite => composite.Measures.FirstOrDefault(measure => measure is BinaryTariffMeasure) is BinaryTariffMeasure binary
          ? binary
          : new BinaryTariffMeasure(DuplexMeasure.Null, DuplexMeasure.Null),
        BinaryTariffMeasure binary => binary,
        _ => new BinaryTariffMeasure(DuplexMeasure.Null, DuplexMeasure.Null)
      };
    }
  }

  public static TariffMeasure operator +(TariffMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => measure + rhs),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 + rhs),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 + rhs,
        binary.T2 + rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator -(TariffMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => measure - rhs),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 - rhs),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 - rhs,
        binary.T2 - rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator *(TariffMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => measure * rhs),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 * rhs),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 * rhs,
        binary.T2 * rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator /(TariffMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => measure / rhs),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 / rhs),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 / rhs,
        binary.T2 / rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator +(float lhs, TariffMeasure rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => lhs + measure),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(lhs + unary.T0),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(lhs + binary.T1,
        lhs + binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator -(float lhs, TariffMeasure rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => lhs - measure),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(lhs - unary.T0),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(lhs - binary.T1,
        lhs - binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator *(float lhs, TariffMeasure rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => lhs * measure),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(lhs * unary.T0),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(lhs * binary.T1,
        lhs * binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator /(float lhs, TariffMeasure rhs)
  {
    return rhs switch
    {
      CompositeTariffMeasure composite => composite.Select(measure => lhs / measure),
      UnaryTariffMeasure unary => new UnaryTariffMeasure(lhs / unary.T0),
      BinaryTariffMeasure binary => new BinaryTariffMeasure(lhs / binary.T1,
        lhs / binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator +(TariffMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs + rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 + rhs, binary.T2 + rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 + rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator -(TariffMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs - rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 - rhs, binary.T2 - rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 - rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator *(TariffMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs * rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 * rhs, binary.T2 * rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 * rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator /(TariffMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs / rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 / rhs, binary.T2 / rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 / rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator +(PhasicMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs + rhs),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs + binary.T1, lhs + binary.T2),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs + unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure operator -(PhasicMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs - rhs),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs - binary.T1, lhs - binary.T2),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs - unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure operator *(PhasicMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs * rhs),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs * binary.T1, lhs * binary.T2),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs * unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure operator /(PhasicMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs / rhs),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs / binary.T1, lhs * binary.T2),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs / unary.T0),
      _ => Null
    };
  }

  public static TariffMeasure operator +(TariffMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositeDuplexMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs + rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 + rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 + rhs,
        binary.T2 + rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator -(TariffMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositeDuplexMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs - rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 - rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 - rhs,
        binary.T2 - rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator *(TariffMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositeDuplexMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs * rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 * rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 * rhs,
        binary.T2 * rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator /(TariffMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeTariffMeasure composite, _) => composite.Zip(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositeDuplexMeasure composite) => composite.ZipTariff(lhs, (rhs, lhs) => lhs / rhs),
      (UnaryTariffMeasure unary, _) => new UnaryTariffMeasure(unary.T0 / rhs),
      (BinaryTariffMeasure binary, _) => new BinaryTariffMeasure(binary.T1 / rhs,
        binary.T2 / rhs),
      _ => Null
    };
  }

  public static TariffMeasure operator +(DuplexMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs + rhs),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs + unary.T0),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs + binary.T1,
        lhs + binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator -(DuplexMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs - rhs),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs - unary.T0),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs - binary.T1,
        lhs - binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator *(DuplexMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs * rhs),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs * unary.T0),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs * binary.T1,
        lhs * binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator /(DuplexMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.ZipTariff(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositeTariffMeasure composite) => composite.Zip(lhs, (rhs, lhs) => lhs / rhs),
      (_, UnaryTariffMeasure unary) => new UnaryTariffMeasure(lhs / unary.T0),
      (_, BinaryTariffMeasure binary) => new BinaryTariffMeasure(lhs / binary.T1,
        lhs / binary.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator +(TariffMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (UnaryTariffMeasure left, UnaryTariffMeasure right) =>
        new UnaryTariffMeasure(left.T0 + right.T0),
      (BinaryTariffMeasure left, BinaryTariffMeasure right) => new
        BinaryTariffMeasure(left.T1 + right.T1, left.T2 + right.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator -(TariffMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (UnaryTariffMeasure left, UnaryTariffMeasure right) =>
        new UnaryTariffMeasure(left.T0 - right.T0),
      (BinaryTariffMeasure left, BinaryTariffMeasure right) => new
        BinaryTariffMeasure(left.T1 - right.T1, left.T2 - right.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator *(TariffMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (UnaryTariffMeasure left, UnaryTariffMeasure right) =>
        new UnaryTariffMeasure(left.T0 * right.T0),
      (BinaryTariffMeasure left, BinaryTariffMeasure right) => new
        BinaryTariffMeasure(left.T1 * right.T1, left.T2 * right.T2),
      _ => Null
    };
  }

  public static TariffMeasure operator /(TariffMeasure lhs, TariffMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (UnaryTariffMeasure left, UnaryTariffMeasure right) =>
        new UnaryTariffMeasure(left.T0 / right.T0),
      (BinaryTariffMeasure left, BinaryTariffMeasure right) => new
        BinaryTariffMeasure(left.T1 / right.T1, left.T2 / right.T2),
      _ => Null
    };
  }
}
