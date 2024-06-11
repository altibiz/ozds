using System.Numerics;

namespace Ozds.Business.Math;

public record class UsageExpenditureMeasure<T>(
  TariffMeasure<T> TrueUsage)
  : ExpenditureMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class SupplyExpenditureMeasure<T>(
  TariffMeasure<T> TrueSupply)
  : ExpenditureMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class DualExpenditureMeasure<T>(
  TariffMeasure<T> TrueUsage,
  TariffMeasure<T> TrueSupply)
  : ExpenditureMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NullExpenditureMeasure<T> : ExpenditureMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public abstract record class ExpenditureMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public static readonly ExpenditureMeasure<T> Null =
    new NullExpenditureMeasure<T>();

  public TariffMeasure<T> ExpenditureUsage
  {
    get
    {
      return this switch
      {
        UsageExpenditureMeasure<T> usage => usage.TrueUsage,
        DualExpenditureMeasure<T> dual => dual.TrueUsage,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> ExpenditureSupply
  {
    get
    {
      return this switch
      {
        SupplyExpenditureMeasure<T> supply => supply.TrueSupply,
        DualExpenditureMeasure<T> dual => dual.TrueSupply,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> ExpenditureSum
  {
    get
    {
      return this switch
      {
        UsageExpenditureMeasure<T> usage => usage.TrueUsage,
        SupplyExpenditureMeasure<T> supply => supply.TrueSupply,
        DualExpenditureMeasure<T> dual => dual.TrueUsage.Add(dual.TrueSupply),
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public ExpenditureMeasure<U> ConvertPrimitiveTo<U>()
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    return this switch
    {
      UsageExpenditureMeasure<T> usage => new UsageExpenditureMeasure<U>(
        usage.TrueUsage.ConvertPrimitiveTo<U>()),
      SupplyExpenditureMeasure<T> supply => new SupplyExpenditureMeasure<U>(
        supply.TrueSupply.ConvertPrimitiveTo<U>()),
      DualExpenditureMeasure<T> dual => new DualExpenditureMeasure<U>(
        dual.TrueUsage.ConvertPrimitiveTo<U>(),
        dual.TrueSupply.ConvertPrimitiveTo<U>()
      ),
      _ => NullExpenditureMeasure<U>.Null
    };
  }
}
