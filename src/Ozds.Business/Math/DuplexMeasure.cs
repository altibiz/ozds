namespace Ozds.Business.Math;

public record class CompositeDuplexMeasure(List<DuplexMeasure> Measures)
  : DuplexMeasure
{
  public T FromMostAccurate<T>(Func<DuplexMeasure, T> selector, T @default)
  {
    return Measures.FirstOrDefault(measure =>
      measure is ImportExportDuplexMeasure) is { } importExport
      ? selector(importExport)
      : Measures.FirstOrDefault(measure => measure is NetDuplexMeasure) is
      { } net
        ? selector(net)
        : Measures.FirstOrDefault(measure => measure is AnyDuplexMeasure) is
        { } any
          ? selector(any)
          : @default;
  }

  public CompositeDuplexMeasure Select(
    Func<DuplexMeasure, DuplexMeasure> selector)
  {
    return new CompositeDuplexMeasure(Measures.Select(selector).ToList());
  }

  public CompositeDuplexMeasure Zip(PhasicMeasure other,
    Func<DuplexMeasure, PhasicMeasure, DuplexMeasure> selector)
  {
    return other switch
    {
      CompositePhasicMeasure otherComposite => new CompositeDuplexMeasure(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeDuplexMeasure(Measures
        .Select(measure => selector(measure, other)).ToList())
    };
  }

  public CompositeDuplexMeasure Zip(DuplexMeasure other,
    Func<DuplexMeasure, DuplexMeasure, DuplexMeasure> selector)
  {
    return other switch
    {
      CompositeDuplexMeasure otherComposite => new CompositeDuplexMeasure(
        Measures.Zip(otherComposite.Measures, selector).ToList()),
      _ => new CompositeDuplexMeasure(Measures
        .Select(measure => selector(measure, other)).ToList())
    };
  }
}

public static class CompositeDuplexMeasureExtensions
{
  public static CompositeDuplexMeasure ZipDuplex(
    this CompositePhasicMeasure lhs, DuplexMeasure rhs,
    Func<PhasicMeasure, DuplexMeasure, DuplexMeasure> selector)
  {
    return rhs switch
    {
      CompositeDuplexMeasure rhsComposite => new CompositeDuplexMeasure(
        lhs.Measures.Zip(rhsComposite.Measures, selector).ToList()),
      _ => new CompositeDuplexMeasure(lhs.Measures
        .Select(measure => selector(measure, rhs)).ToList())
    };
  }
}

public record class ImportExportDuplexMeasure(
  PhasicMeasure Import,
  PhasicMeasure Export) : DuplexMeasure;

public record class AnyDuplexMeasure(PhasicMeasure Value) : DuplexMeasure;

public record class NetDuplexMeasure(PhasicMeasure TrueNet) : DuplexMeasure;

public record class NullDuplexMeasure : DuplexMeasure;

public abstract record class DuplexMeasure
{
  public static readonly DuplexMeasure Null = new NullDuplexMeasure();

  public PhasicMeasure DuplexNet
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure composite => composite.FromMostAccurate(
          measure => measure.DuplexNet, PhasicMeasure.Null),
        ImportExportDuplexMeasure importExport => importExport.Import -
                                                  importExport.Export,
        NetDuplexMeasure net => net.TrueNet,
        _ => PhasicMeasure.Null
      };
    }
  }

  public PhasicMeasure DuplexAny
  {
    get
    {
      return this switch
      {
        CompositeDuplexMeasure composite => composite.FromMostAccurate(
          measure => measure.DuplexAny, PhasicMeasure.Null),
        ImportExportDuplexMeasure importExport => importExport.Import -
                                                  importExport.Export,
        NetDuplexMeasure net => net.TrueNet,
        AnyDuplexMeasure net => net.Value,
        _ => PhasicMeasure.Null
      };
    }
  }

  public static DuplexMeasure operator +(DuplexMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        measure + rhs),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        importExport.Import + rhs,
        importExport.Export + rhs
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet + rhs),
      AnyDuplexMeasure any => new AnyDuplexMeasure(any.Value + rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator -(DuplexMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        measure - rhs),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        importExport.Import - rhs,
        importExport.Export - rhs
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet - rhs),
      AnyDuplexMeasure any => new AnyDuplexMeasure(any.Value - rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator *(DuplexMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        measure * rhs),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        importExport.Import * rhs,
        importExport.Export * rhs
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet * rhs),
      AnyDuplexMeasure any => new AnyDuplexMeasure(any.Value * rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator /(DuplexMeasure lhs, float rhs)
  {
    return lhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        measure / rhs),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        importExport.Import / rhs,
        importExport.Export / rhs
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet / rhs),
      AnyDuplexMeasure any => new AnyDuplexMeasure(any.Value / rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator +(float lhs, DuplexMeasure rhs)
  {
    return rhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        lhs + composite),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        lhs + importExport.Import,
        lhs + importExport.Export
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(lhs + net.TrueNet),
      AnyDuplexMeasure any => new AnyDuplexMeasure(lhs + any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator -(float lhs, DuplexMeasure rhs)
  {
    return rhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        lhs - composite),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        lhs - importExport.Import,
        lhs - importExport.Export
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(lhs - net.TrueNet),
      AnyDuplexMeasure any => new AnyDuplexMeasure(lhs - any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator *(float lhs, DuplexMeasure rhs)
  {
    return rhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        lhs * composite),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        lhs * importExport.Import,
        lhs * importExport.Export
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(lhs * net.TrueNet),
      AnyDuplexMeasure any => new AnyDuplexMeasure(lhs * any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator /(float lhs, DuplexMeasure rhs)
  {
    return rhs switch
    {
      CompositeDuplexMeasure composite => composite.Select(measure =>
        lhs / composite),
      ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
        lhs / importExport.Import,
        lhs / importExport.Export
      ),
      NetDuplexMeasure net => new NetDuplexMeasure(lhs / net.TrueNet),
      AnyDuplexMeasure any => new AnyDuplexMeasure(lhs / any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator +(DuplexMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipDuplex(lhs,
        (rhs, lhs) => lhs + rhs),
      (ImportExportDuplexMeasure importExport, _) => new
        ImportExportDuplexMeasure(
          importExport.Import + rhs,
          importExport.Export + rhs
        ),
      (NetDuplexMeasure net, _) => new NetDuplexMeasure(net.TrueNet + rhs),
      (AnyDuplexMeasure any, _) => new AnyDuplexMeasure(any.Value + rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator -(DuplexMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipDuplex(lhs,
        (rhs, lhs) => lhs - rhs),
      (ImportExportDuplexMeasure importExport, _) => new
        ImportExportDuplexMeasure(
          importExport.Import - rhs,
          importExport.Export - rhs
        ),
      (NetDuplexMeasure net, _) => new NetDuplexMeasure(net.TrueNet - rhs),
      (AnyDuplexMeasure any, _) => new AnyDuplexMeasure(any.Value - rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator *(DuplexMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipDuplex(lhs,
        (rhs, lhs) => lhs * rhs),
      (ImportExportDuplexMeasure importExport, _) => new
        ImportExportDuplexMeasure(
          importExport.Import * rhs,
          importExport.Export * rhs
        ),
      (NetDuplexMeasure net, _) => new NetDuplexMeasure(net.TrueNet * rhs),
      (AnyDuplexMeasure any, _) => new AnyDuplexMeasure(any.Value * rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator /(DuplexMeasure lhs, PhasicMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure composite, _) => composite.Zip(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositePhasicMeasure composite) => composite.ZipDuplex(lhs,
        (rhs, lhs) => lhs / rhs),
      (ImportExportDuplexMeasure importExport, _) => new
        ImportExportDuplexMeasure(
          importExport.Import / rhs,
          importExport.Export / rhs
        ),
      (NetDuplexMeasure net, _) => new NetDuplexMeasure(net.TrueNet / rhs),
      (AnyDuplexMeasure any, _) => new AnyDuplexMeasure(any.Value / rhs),
      _ => Null
    };
  }

  public static DuplexMeasure operator +(PhasicMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipDuplex(rhs,
        (lhs, rhs) => lhs + rhs),
      (_, CompositeDuplexMeasure composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs + rhs),
      (_, ImportExportDuplexMeasure importExport) => new
        ImportExportDuplexMeasure(
          lhs + importExport.Import,
          lhs + importExport.Export
        ),
      (_, NetDuplexMeasure net) => new NetDuplexMeasure(lhs + net.TrueNet),
      (_, AnyDuplexMeasure any) => new AnyDuplexMeasure(lhs + any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator -(PhasicMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipDuplex(rhs,
        (lhs, rhs) => lhs - rhs),
      (_, CompositeDuplexMeasure composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs - rhs),
      (_, ImportExportDuplexMeasure importExport) => new
        ImportExportDuplexMeasure(
          lhs - importExport.Import,
          lhs - importExport.Export
        ),
      (_, NetDuplexMeasure net) => new NetDuplexMeasure(lhs - net.TrueNet),
      (_, AnyDuplexMeasure any) => new AnyDuplexMeasure(lhs - any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator *(PhasicMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipDuplex(rhs,
        (lhs, rhs) => lhs * rhs),
      (_, CompositeDuplexMeasure composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs * rhs),
      (_, ImportExportDuplexMeasure importExport) => new
        ImportExportDuplexMeasure(
          lhs * importExport.Import,
          lhs * importExport.Export
        ),
      (_, NetDuplexMeasure net) => new NetDuplexMeasure(lhs * net.TrueNet),
      (_, AnyDuplexMeasure any) => new AnyDuplexMeasure(lhs * any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator /(PhasicMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositePhasicMeasure composite, _) => composite.ZipDuplex(rhs,
        (lhs, rhs) => lhs / rhs),
      (_, CompositeDuplexMeasure composite) => composite.Zip(lhs,
        (rhs, lhs) => lhs / rhs),
      (_, ImportExportDuplexMeasure importExport) => new
        ImportExportDuplexMeasure(
          lhs / importExport.Import,
          lhs / importExport.Export
        ),
      (_, NetDuplexMeasure net) => new NetDuplexMeasure(lhs / net.TrueNet),
      (_, AnyDuplexMeasure any) => new AnyDuplexMeasure(lhs / any.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator +(DuplexMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs + rhs),
      (_, CompositeDuplexMeasure rhsComposite) =>
        rhsComposite.Zip(lhs, (rhs, lhs) => lhs + rhs),
      (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure
        rhsImportExport) => new ImportExportDuplexMeasure(
          lhsImportExport.Import + rhsImportExport.Import,
          lhsImportExport.Export + rhsImportExport.Export
        ),
      (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) =>
        new NetDuplexMeasure(lhsNet.TrueNet + rhsNet.TrueNet),
      (AnyDuplexMeasure lhsAny, AnyDuplexMeasure rhsAny) =>
        new AnyDuplexMeasure(lhsAny.Value + rhsAny.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator -(DuplexMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs - rhs),
      (_, CompositeDuplexMeasure rhsComposite) =>
        rhsComposite.Zip(lhs, (rhs, lhs) => lhs - rhs),
      (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure
        rhsImportExport) => new ImportExportDuplexMeasure(
          lhsImportExport.Import - rhsImportExport.Import,
          lhsImportExport.Export - rhsImportExport.Export
        ),
      (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) =>
        new NetDuplexMeasure(lhsNet.TrueNet - rhsNet.TrueNet),
      (AnyDuplexMeasure lhsAny, AnyDuplexMeasure rhsAny) =>
        new AnyDuplexMeasure(lhsAny.Value - rhsAny.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator *(DuplexMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs * rhs),
      (_, CompositeDuplexMeasure rhsComposite) =>
        rhsComposite.Zip(lhs, (rhs, lhs) => lhs * rhs),
      (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure
        rhsImportExport) => new ImportExportDuplexMeasure(
          lhsImportExport.Import * rhsImportExport.Import,
          lhsImportExport.Export * rhsImportExport.Export
        ),
      (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) =>
        new NetDuplexMeasure(lhsNet.TrueNet * rhsNet.TrueNet),
      (AnyDuplexMeasure lhsAny, AnyDuplexMeasure rhsAny) =>
        new AnyDuplexMeasure(lhsAny.Value * rhsAny.Value),
      _ => Null
    };
  }

  public static DuplexMeasure operator /(DuplexMeasure lhs, DuplexMeasure rhs)
  {
    return (lhs, rhs) switch
    {
      (CompositeDuplexMeasure lhsComposite, _) =>
        lhsComposite.Zip(rhs, (lhs, rhs) => lhs / rhs),
      (_, CompositeDuplexMeasure rhsComposite) =>
        rhsComposite.Zip(lhs, (rhs, lhs) => lhs / rhs),
      (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure
        rhsImportExport) => new ImportExportDuplexMeasure(
          lhsImportExport.Import / rhsImportExport.Import,
          lhsImportExport.Export / rhsImportExport.Export
        ),
      (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) =>
        new NetDuplexMeasure(lhsNet.TrueNet / rhsNet.TrueNet),
      (AnyDuplexMeasure lhsAny, AnyDuplexMeasure rhsAny) =>
        new AnyDuplexMeasure(lhsAny.Value / rhsAny.Value),
      _ => Null
    };
  }
}
