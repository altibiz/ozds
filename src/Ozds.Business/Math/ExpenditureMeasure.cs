using System.Numerics;

namespace Ozds.Business.Math;

// TODO: convert properties to methods and create proper class hierarchy
#pragma warning disable S2365
#pragma warning disable S3060

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

  public TariffMeasure<T> ExpenditureUsage()
  {
    {
      return this switch
      {
        UsageExpenditureMeasure<T> usage => usage.TrueUsage,
        DualExpenditureMeasure<T> dual => dual.TrueUsage,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> ExpenditureSupply()
  {
    {
      return this switch
      {
        SupplyExpenditureMeasure<T> supply => supply.TrueSupply,
        DualExpenditureMeasure<T> dual => dual.TrueSupply,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> ExpenditureSum()
  {
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

  public ExpenditureMeasure<TConverted> ConvertPrimitiveTo<TConverted>()
    where TConverted : struct,
    IComparisonOperators<TConverted, TConverted, bool>,
    IAdditionOperators<TConverted, TConverted, TConverted>,
    ISubtractionOperators<TConverted, TConverted, TConverted>,
    IMultiplyOperators<TConverted, TConverted, TConverted>,
    IDivisionOperators<TConverted, TConverted, TConverted>
  {
    return this switch
    {
      UsageExpenditureMeasure<T> usage => new
        UsageExpenditureMeasure<TConverted>(
          usage.TrueUsage.ConvertPrimitiveTo<TConverted>()),
      SupplyExpenditureMeasure<T> supply => new
        SupplyExpenditureMeasure<TConverted>(
          supply.TrueSupply.ConvertPrimitiveTo<TConverted>()),
      DualExpenditureMeasure<T> dual => new DualExpenditureMeasure<TConverted>(
        dual.TrueUsage.ConvertPrimitiveTo<TConverted>(),
        dual.TrueSupply.ConvertPrimitiveTo<TConverted>()
      ),
      _ => NullExpenditureMeasure<TConverted>.Null
    };
  }
}
