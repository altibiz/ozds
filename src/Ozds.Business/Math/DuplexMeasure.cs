using System.Numerics;

namespace Ozds.Business.Math;

public record ImportExportDuplexMeasure(PhasicMeasure Import, PhasicMeasure Export) : DuplexMeasure;

public record NetDuplexMeasure(PhasicMeasure TrueNet) : DuplexMeasure;

public record DuplexMeasure() :
  IAdditionOperators<DuplexMeasure, float, DuplexMeasure>,
  ISubtractionOperators<DuplexMeasure, float, DuplexMeasure>,
  IMultiplyOperators<DuplexMeasure, float, DuplexMeasure>,
  IDivisionOperators<DuplexMeasure, float, DuplexMeasure>,
  IAdditionOperators<DuplexMeasure, PhasicMeasure, DuplexMeasure>,
  ISubtractionOperators<DuplexMeasure, PhasicMeasure, DuplexMeasure>,
  IMultiplyOperators<DuplexMeasure, PhasicMeasure, DuplexMeasure>,
  IDivisionOperators<DuplexMeasure, PhasicMeasure, DuplexMeasure>,
  IAdditionOperators<DuplexMeasure, DuplexMeasure, DuplexMeasure>,
  ISubtractionOperators<DuplexMeasure, DuplexMeasure, DuplexMeasure>,
  IMultiplyOperators<DuplexMeasure, DuplexMeasure, DuplexMeasure>,
  IDivisionOperators<DuplexMeasure, DuplexMeasure, DuplexMeasure>
{
  public static readonly DuplexMeasure Null = new();

  public PhasicMeasure DuplexNet => this switch
  {
    ImportExportDuplexMeasure importExport => importExport.Import - importExport.Export,
    NetDuplexMeasure net => net.TrueNet,
    _ => PhasicMeasure.Null
  };

  public static DuplexMeasure operator +(DuplexMeasure lhs, float rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import + rhs,
      importExport.Export + rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet + rhs),
    _ => new()
  };

  public static DuplexMeasure operator -(DuplexMeasure lhs, float rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import - rhs,
      importExport.Export - rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet - rhs),
    _ => new()
  };

  public static DuplexMeasure operator *(DuplexMeasure lhs, float rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import * rhs,
      importExport.Export * rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet * rhs),
    _ => new()
  };

  public static DuplexMeasure operator /(DuplexMeasure lhs, float rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import / rhs,
      importExport.Export / rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet / rhs),
    _ => new()
  };

  public static DuplexMeasure operator +(DuplexMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import + rhs,
      importExport.Export + rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet + rhs),
    _ => new()
  };

  public static DuplexMeasure operator -(DuplexMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import - rhs,
      importExport.Export - rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet - rhs),
    _ => new()
  };

  public static DuplexMeasure operator *(DuplexMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import * rhs,
      importExport.Export * rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet * rhs),
    _ => new()
  };

  public static DuplexMeasure operator /(DuplexMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    ImportExportDuplexMeasure importExport => new ImportExportDuplexMeasure(
      importExport.Import / rhs,
      importExport.Export / rhs
    ),
    NetDuplexMeasure net => new NetDuplexMeasure(net.TrueNet / rhs),
    _ => new()
  };

  public static DuplexMeasure operator +(DuplexMeasure lhs, DuplexMeasure rhs) => (lhs, rhs) switch
  {
    (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure rhsImportExport) => new ImportExportDuplexMeasure(
      lhsImportExport.Import + rhsImportExport.Import,
      lhsImportExport.Export + rhsImportExport.Export
    ),
    (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) => new NetDuplexMeasure(lhsNet.TrueNet + rhsNet.TrueNet),
    _ => new()
  };

  public static DuplexMeasure operator -(DuplexMeasure lhs, DuplexMeasure rhs) => (lhs, rhs) switch
  {
    (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure rhsImportExport) => new ImportExportDuplexMeasure(
      lhsImportExport.Import - rhsImportExport.Import,
      lhsImportExport.Export - rhsImportExport.Export
    ),
    (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) => new NetDuplexMeasure(lhsNet.TrueNet - rhsNet.TrueNet),
    _ => new()
  };

  public static DuplexMeasure operator *(DuplexMeasure lhs, DuplexMeasure rhs) => (lhs, rhs) switch
  {
    (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure rhsImportExport) => new ImportExportDuplexMeasure(
      lhsImportExport.Import * rhsImportExport.Import,
      lhsImportExport.Export * rhsImportExport.Export
    ),
    (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) => new NetDuplexMeasure(lhsNet.TrueNet * rhsNet.TrueNet),
    _ => new()
  };

  public static DuplexMeasure operator /(DuplexMeasure lhs, DuplexMeasure rhs) => (lhs, rhs) switch
  {
    (ImportExportDuplexMeasure lhsImportExport, ImportExportDuplexMeasure rhsImportExport) => new ImportExportDuplexMeasure(
      lhsImportExport.Import / rhsImportExport.Import,
      lhsImportExport.Export / rhsImportExport.Export
    ),
    (NetDuplexMeasure lhsNet, NetDuplexMeasure rhsNet) => new NetDuplexMeasure(lhsNet.TrueNet / rhsNet.TrueNet),
    _ => new()
  };
}
