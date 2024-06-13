using System.Numerics;

namespace Ozds.Business.Math;

public record class CompositeDuplexMeasure<T>
  : DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public CompositeDuplexMeasure(List<DuplexMeasure<T>> measures)
  {
    Measures = measures.SelectMany(measure => measure switch
    {
      CompositeDuplexMeasure<T> composite => composite.Measures,
      _ => [measure]
    }).ToList();
  }

  public List<DuplexMeasure<T>> Measures { get; set; }

  public U FromMostAccurate<U>(Func<DuplexMeasure<T>, U> selector, U @default)
  {
    return Measures.FirstOrDefault(measure =>
      measure is ImportExportDuplexMeasure<T>) is { } importExport
      ? selector(importExport)
      : Measures.FirstOrDefault(measure => measure is NetDuplexMeasure<T>) is
        { } net
        ? selector(net)
        : Measures.FirstOrDefault(measure => measure is AnyDuplexMeasure<T>) is
          { } any
          ? selector(any)
          : @default;
  }

  public CompositeDuplexMeasure<T> Select(
    Func<DuplexMeasure<T>, DuplexMeasure<T>> selector)
  {
    return new CompositeDuplexMeasure<T>(Measures.Select(selector).ToList());
  }

  public CompositeDuplexMeasure<T> Zip(DuplexMeasure<T> other,
    Func<DuplexMeasure<T>, DuplexMeasure<T>, DuplexMeasure<T>> selector)
  {
    return other switch
    {
      CompositeDuplexMeasure<T> otherComposite => new CompositeDuplexMeasure<T>(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeDuplexMeasure<T>(Measures
        .Select(measure => selector(measure, other)).ToList())
    };
  }
}

public record class ImportExportDuplexMeasure<T>(
  PhasicMeasure<T> Import,
  PhasicMeasure<T> Export) : DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class AnyDuplexMeasure<T>(PhasicMeasure<T> Value)
  : DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NetDuplexMeasure<T>(PhasicMeasure<T> TrueNet)
  : DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NullDuplexMeasure<T> : DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public abstract record class DuplexMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public static readonly DuplexMeasure<T> Null = new NullDuplexMeasure<T>();

  public PhasicMeasure<T> DuplexNet
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.DuplexNet, PhasicMeasure<T>.Null),
        ImportExportDuplexMeasure<T> importExport => importExport.Import
          .Subtract(importExport.Export),
        NetDuplexMeasure<T> net => net.TrueNet,
        _ => PhasicMeasure<T>.Null
      };
    }
  }

  public PhasicMeasure<T> DuplexAny
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.DuplexAny, PhasicMeasure<T>.Null),
        NetDuplexMeasure<T> net => net.TrueNet,
        AnyDuplexMeasure<T> net => net.Value,
        _ => PhasicMeasure<T>.Null
      };
    }
  }

  public PhasicMeasure<T> DuplexImport
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.DuplexImport, PhasicMeasure<T>.Null),
        ImportExportDuplexMeasure<T> importExport => importExport.Import,
        _ => PhasicMeasure<T>.Null
      };
    }
  }

  public PhasicMeasure<T> DuplexExport
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.DuplexExport, PhasicMeasure<T>.Null),
        ImportExportDuplexMeasure<T> importExport => importExport.Export,
        _ => PhasicMeasure<T>.Null
      };
    }
  }

  public DuplexMeasure<T> DuplexAbs
  {
    get { return Select(phasic => phasic.PhaseAbs); }
  }

  public PhasicMeasure<T> DuplexSum
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure<T> composite => composite.FromMostAccurate(
          measure => measure.DuplexSum, PhasicMeasure<T>.Null),
        ImportExportDuplexMeasure<T> importExport => importExport.Import
          .Add(importExport.Export),
        NetDuplexMeasure<T> net => net.TrueNet,
        AnyDuplexMeasure<T> any => any.Value,
        _ => PhasicMeasure<T>.Null
      };
    }
  }

  public DuplexMeasure<U> ConvertPrimitiveTo<U>()
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    return this switch
    {
      CompositeDuplexMeasure<T> composite => new CompositeDuplexMeasure<U>(
        composite.Measures.Select(measure => measure.ConvertPrimitiveTo<U>())
          .ToList()),
      ImportExportDuplexMeasure<T> importExport => new
        ImportExportDuplexMeasure<U>(
          importExport.Import.ConvertPrimitiveTo<U>(),
          importExport.Export.ConvertPrimitiveTo<U>()
        ),
      NetDuplexMeasure<T> net => new NetDuplexMeasure<U>(
        net.TrueNet.ConvertPrimitiveTo<U>()),
      AnyDuplexMeasure<T> any => new AnyDuplexMeasure<U>(
        any.Value.ConvertPrimitiveTo<U>()),
      _ => DuplexMeasure<U>.Null
    };
  }

  // DO NOT TEST
  public DuplexMeasure<T> Select(
    Func<PhasicMeasure<T>, PhasicMeasure<T>> selector)
  {
    return this switch
    {
      CompositeDuplexMeasure<T> composite => composite.Select(measure =>
        measure.Select(selector)),
      ImportExportDuplexMeasure<T> importExport => new
        ImportExportDuplexMeasure<T>(
          selector(importExport.Import),
          selector(importExport.Export)
        ),
      NetDuplexMeasure<T> net => new NetDuplexMeasure<T>(selector(net.TrueNet)),
      AnyDuplexMeasure<T> any => new AnyDuplexMeasure<T>(selector(any.Value)),
      _ => Null
    };
  }

  public DuplexMeasure<T> Multiply(T rhs)
  {
    return this switch
    {
      CompositeDuplexMeasure<T> composite => composite.Select(measure =>
        measure.Multiply(rhs)),
      ImportExportDuplexMeasure<T> importExport => new
        ImportExportDuplexMeasure<T>(
          importExport.Import.Multiply(rhs),
          importExport.Export.Multiply(rhs)
        ),
      NetDuplexMeasure<T> net => new NetDuplexMeasure<T>(
        net.TrueNet.Multiply(rhs)),
      AnyDuplexMeasure<T> any => new AnyDuplexMeasure<T>(
        any.Value.Multiply(rhs)),
      _ => Null
    };
  }

  public DuplexMeasure<T> Divide(T rhs)
  {
    return this switch
    {
      CompositeDuplexMeasure<T> composite => composite.Select(measure =>
        measure.Divide(rhs)),
      ImportExportDuplexMeasure<T> importExport => new
        ImportExportDuplexMeasure<T>(
          importExport.Import.Divide(rhs),
          importExport.Export.Divide(rhs)
        ),
      NetDuplexMeasure<T> net => new NetDuplexMeasure<T>(
        net.TrueNet.Divide(rhs)),
      AnyDuplexMeasure<T> any => new AnyDuplexMeasure<T>(any.Value.Divide(rhs)),
      _ => Null
    };
  }

  public DuplexMeasure<T> Add(DuplexMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeDuplexMeasure<T> lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs.Add(rhs)),
      (_, CompositeDuplexMeasure<T> rhsComposite) =>
        rhsComposite.Zip(this, (rhs, lhs) => lhs.Add(rhs)),
      (ImportExportDuplexMeasure<T> lhsImportExport,
        ImportExportDuplexMeasure<T>
        rhsImportExport) => new ImportExportDuplexMeasure<T>(
          lhsImportExport.Import.Add(rhsImportExport.Import),
          lhsImportExport.Export.Add(rhsImportExport.Export)
        ),
      (NetDuplexMeasure<T> lhsNet, NetDuplexMeasure<T> rhsNet) =>
        new NetDuplexMeasure<T>(lhsNet.TrueNet.Add(rhsNet.TrueNet)),
      (AnyDuplexMeasure<T> lhsAny, AnyDuplexMeasure<T> rhsAny) =>
        new AnyDuplexMeasure<T>(lhsAny.Value.Add(rhsAny.Value)),
      _ => Null
    };
  }

  public DuplexMeasure<T> Subtract(DuplexMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeDuplexMeasure<T> lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs.Subtract(rhs)),
      (_, CompositeDuplexMeasure<T> rhsComposite) =>
        rhsComposite.Zip(this, (rhs, lhs) => lhs.Subtract(rhs)),
      (ImportExportDuplexMeasure<T> lhsImportExport,
        ImportExportDuplexMeasure<T>
        rhsImportExport) => new ImportExportDuplexMeasure<T>(
          lhsImportExport.Import.Subtract(rhsImportExport.Import),
          lhsImportExport.Export.Subtract(rhsImportExport.Export)
        ),
      (NetDuplexMeasure<T> lhsNet, NetDuplexMeasure<T> rhsNet) =>
        new NetDuplexMeasure<T>(lhsNet.TrueNet.Subtract(rhsNet.TrueNet)),
      (AnyDuplexMeasure<T> lhsAny, AnyDuplexMeasure<T> rhsAny) =>
        new AnyDuplexMeasure<T>(lhsAny.Value.Subtract(rhsAny.Value)),
      _ => Null
    };
  }

  public DuplexMeasure<T> Multiply(DuplexMeasure<T> rhs)
  {
    return (this, rhs) switch
    {
      (CompositeDuplexMeasure<T> lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs.Multiply(rhs)),
      (_, CompositeDuplexMeasure<T> rhsComposite) =>
        rhsComposite.Zip(this, (rhs, lhs) => lhs.Multiply(rhs)),
      (ImportExportDuplexMeasure<T> lhsImportExport,
        ImportExportDuplexMeasure<T>
        rhsImportExport) => new ImportExportDuplexMeasure<T>(
          lhsImportExport.Import.Multiply(rhsImportExport.Import),
          lhsImportExport.Export.Multiply(rhsImportExport.Export)
        ),
      (NetDuplexMeasure<T> lhsNet, NetDuplexMeasure<T> rhsNet) =>
        new NetDuplexMeasure<T>(lhsNet.TrueNet.Multiply(rhsNet.TrueNet)),
      (AnyDuplexMeasure<T> lhsAny, AnyDuplexMeasure<T> rhsAny) =>
        new AnyDuplexMeasure<T>(lhsAny.Value.Multiply(rhsAny.Value)),
      _ => Null
    };
  }
}
