using System.Numerics;

namespace Ozds.Business.Math;

public record BinaryTariffMeasure(DuplexMeasure T1, DuplexMeasure T2) : TariffMeasure;

public record UnaryTariffMeasure(DuplexMeasure T0) : TariffMeasure;

public record TariffMeasure() :
  IAdditionOperators<TariffMeasure, float, TariffMeasure>,
  ISubtractionOperators<TariffMeasure, float, TariffMeasure>,
  IMultiplyOperators<TariffMeasure, float, TariffMeasure>,
  IDivisionOperators<TariffMeasure, float, TariffMeasure>,
  IAdditionOperators<TariffMeasure, PhasicMeasure, TariffMeasure>,
  ISubtractionOperators<TariffMeasure, PhasicMeasure, TariffMeasure>,
  IMultiplyOperators<TariffMeasure, PhasicMeasure, TariffMeasure>,
  IDivisionOperators<TariffMeasure, PhasicMeasure, TariffMeasure>,
  IAdditionOperators<TariffMeasure, DuplexMeasure, TariffMeasure>,
  ISubtractionOperators<TariffMeasure, DuplexMeasure, TariffMeasure>,
  IMultiplyOperators<TariffMeasure, DuplexMeasure, TariffMeasure>,
  IDivisionOperators<TariffMeasure, DuplexMeasure, TariffMeasure>,
  IAdditionOperators<TariffMeasure, TariffMeasure, TariffMeasure>,
  ISubtractionOperators<TariffMeasure, TariffMeasure, TariffMeasure>,
  IMultiplyOperators<TariffMeasure, TariffMeasure, TariffMeasure>,
  IDivisionOperators<TariffMeasure, TariffMeasure, TariffMeasure>
{
  public static readonly TariffMeasure Null = new();

  public DuplexMeasure Unary => this switch
  {
    BinaryTariffMeasure binary => binary.T1 + binary.T2,
    UnaryTariffMeasure unary => unary.T0,
    _ => DuplexMeasure.Null
  };

  public BinaryTariffMeasure Binary => this switch
  {
    BinaryTariffMeasure binary => binary,
    _ => new BinaryTariffMeasure(DuplexMeasure.Null, DuplexMeasure.Null)
  };

  public static TariffMeasure operator +(TariffMeasure lhs, float rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 + rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 + rhs, binary.T2 + rhs),
    _ => Null
  };

  public static TariffMeasure operator -(TariffMeasure lhs, float rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 - rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 - rhs, binary.T2 - rhs),
    _ => Null
  };

  public static TariffMeasure operator *(TariffMeasure lhs, float rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 * rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 * rhs, binary.T2 * rhs),
    _ => Null
  };

  public static TariffMeasure operator /(TariffMeasure lhs, float rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 / rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 / rhs, binary.T2 / rhs),
    _ => Null
  };

  public static TariffMeasure operator +(TariffMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 + rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 + rhs, binary.T2 + rhs),
    _ => Null
  };

  public static TariffMeasure operator -(TariffMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 - rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 - rhs, binary.T2 - rhs),
    _ => Null
  };

  public static TariffMeasure operator *(TariffMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 * rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 * rhs, binary.T2 * rhs),
    _ => Null
  };

  public static TariffMeasure operator /(TariffMeasure lhs, PhasicMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 / rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 / rhs, binary.T2 / rhs),
    _ => Null
  };

  public static TariffMeasure operator +(TariffMeasure lhs, DuplexMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 + rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 + rhs, binary.T2 + rhs),
    _ => Null
  };

  public static TariffMeasure operator -(TariffMeasure lhs, DuplexMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 - rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 - rhs, binary.T2 - rhs),
    _ => Null
  };

  public static TariffMeasure operator *(TariffMeasure lhs, DuplexMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 * rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 * rhs, binary.T2 * rhs),
    _ => Null
  };

  public static TariffMeasure operator /(TariffMeasure lhs, DuplexMeasure rhs) => lhs switch
  {
    UnaryTariffMeasure unary => new UnaryTariffMeasure(unary.T0 / rhs),
    BinaryTariffMeasure binary => new BinaryTariffMeasure(binary.T1 / rhs, binary.T2 / rhs),
    _ => Null
  };

  public static TariffMeasure operator +(TariffMeasure lhs, TariffMeasure rhs) => (lhs, rhs) switch
  {
    (UnaryTariffMeasure left, UnaryTariffMeasure right) => new UnaryTariffMeasure(left.T0 + right.T0),
    (BinaryTariffMeasure left, BinaryTariffMeasure right) => new BinaryTariffMeasure(left.T1 + right.T1, left.T2 + right.T2),
    _ => Null
  };

  public static TariffMeasure operator -(TariffMeasure lhs, TariffMeasure rhs) => (lhs, rhs) switch
  {
    (UnaryTariffMeasure left, UnaryTariffMeasure right) => new UnaryTariffMeasure(left.T0 - right.T0),
    (BinaryTariffMeasure left, BinaryTariffMeasure right) => new BinaryTariffMeasure(left.T1 - right.T1, left.T2 - right.T2),
    _ => Null
  };

  public static TariffMeasure operator *(TariffMeasure lhs, TariffMeasure rhs) => (lhs, rhs) switch
  {
    (UnaryTariffMeasure left, UnaryTariffMeasure right) => new UnaryTariffMeasure(left.T0 * right.T0),
    (BinaryTariffMeasure left, BinaryTariffMeasure right) => new BinaryTariffMeasure(left.T1 * right.T1, left.T2 * right.T2),
    _ => Null
  };

  public static TariffMeasure operator /(TariffMeasure lhs, TariffMeasure rhs) => (lhs, rhs) switch
  {
    (UnaryTariffMeasure left, UnaryTariffMeasure right) => new UnaryTariffMeasure(left.T0 / right.T0),
    (BinaryTariffMeasure left, BinaryTariffMeasure right) => new BinaryTariffMeasure(left.T1 / right.T1, left.T2 / right.T2),
    _ => Null
  };
}
