using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Enums;

public enum ObisModel
{
  ActiveEnergyTotalImportT0_kWh,
  ActiveEnergyTotalImportT1_kWh,
  ActiveEnergyTotalImportT2_kWh,
  ReactiveEnergyTotalImportT0_kVARh,
  ReactiveEnergyTotalExportT0_kVARh,
  ActivePowerTotalImportT1_kW
}

public static class ObisModelExtensions
{
  public static ObisModel ToObis(this string code)
  {
    return code switch
    {
      "A+_T0" => ObisModel.ActiveEnergyTotalImportT0_kWh,
      "A+_T1" => ObisModel.ActiveEnergyTotalImportT1_kWh,
      "A+_T2" => ObisModel.ActiveEnergyTotalImportT2_kWh,
      "R1_T0" => ObisModel.ReactiveEnergyTotalImportT0_kVARh,
      "R4_T0" => ObisModel.ReactiveEnergyTotalExportT0_kVARh,
      "P+_T1" => ObisModel.ActivePowerTotalImportT1_kW,
      _ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
    };
  }

  public static string ToCode(this ObisModel model)
  {
    return model switch
    {
      ObisModel.ActiveEnergyTotalImportT0_kWh => "A+_T0",
      ObisModel.ActiveEnergyTotalImportT1_kWh => "A+_T1",
      ObisModel.ActiveEnergyTotalImportT2_kWh => "A+_T2",
      ObisModel.ReactiveEnergyTotalImportT0_kVARh => "R1_T0",
      ObisModel.ReactiveEnergyTotalExportT0_kVARh => "R4_T0",
      ObisModel.ActivePowerTotalImportT1_kW => "P+_T1",
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static string ToUnit(this ObisModel model)
  {
    return model switch
    {
      ObisModel.ActiveEnergyTotalImportT0_kWh => "kWh",
      ObisModel.ActiveEnergyTotalImportT1_kWh => "kWh",
      ObisModel.ActiveEnergyTotalImportT2_kWh => "kWh",
      ObisModel.ReactiveEnergyTotalImportT0_kVARh => "kVARh",
      ObisModel.ReactiveEnergyTotalExportT0_kVARh => "kVARh",
      ObisModel.ActivePowerTotalImportT1_kW => "kW",
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static decimal GetValue(
    this ObisModel model,
    IAggregate min,
    IAggregate max)
  {
    if (model is ObisModel.ActiveEnergyTotalImportT0_kWh)
    {
      var maxEnergy = max.ActiveEnergy_Wh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      var minEnergy = min.ActiveEnergy_Wh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActiveEnergyTotalImportT1_kWh)
    {
      var maxEnergy = max.ActiveEnergy_Wh
        .TariffBinary().T1
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      var minEnergy = min.ActiveEnergy_Wh
        .TariffBinary().T1
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActiveEnergyTotalImportT2_kWh)
    {
      var maxEnergy = max.ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      var minEnergy = min.ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ReactiveEnergyTotalImportT0_kVARh)
    {
      var maxEnergy = max.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      var minEnergy = min.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();
      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ReactiveEnergyTotalExportT0_kVARh)
    {
      var maxEnergy = max.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexExport()
        .AggregateMax()
        .PhaseSum();
      var minEnergy = min.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexExport()
        .AggregateMax()
        .PhaseSum();
      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActivePowerTotalImportT1_kW)
    {
      return min.DerivedActivePower_W
          .TariffBinary().T1
          .DuplexExport()
          .AggregateMax()
          .PhaseSum()
        / 1000;
    }

    throw new ArgumentOutOfRangeException(nameof(model), model, null);
  }

  public static decimal GetDerivedValue(
    this ObisModel model,
    IAggregate aggregate
  )
  {
    return model switch
    {
      ObisModel.ActiveEnergyTotalImportT0_kWh =>
        aggregate.DerivedActivePower_W
          .TariffUnary()
          .DuplexImport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      ObisModel.ActiveEnergyTotalImportT1_kWh =>
        aggregate.DerivedActivePower_W
          .TariffBinary().T1
          .DuplexImport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      ObisModel.ActiveEnergyTotalImportT2_kWh =>
        aggregate.DerivedActivePower_W
          .TariffBinary().T2
          .DuplexImport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      ObisModel.ReactiveEnergyTotalImportT0_kVARh =>
        aggregate.DerivedReactivePower_VAR
          .TariffUnary()
          .DuplexImport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      ObisModel.ReactiveEnergyTotalExportT0_kVARh =>
        aggregate.DerivedReactivePower_VAR
          .TariffUnary()
          .DuplexExport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      ObisModel.ActivePowerTotalImportT1_kW =>
        aggregate.DerivedActivePower_W
          .TariffBinary().T1
          .DuplexExport()
          .AggregateMax()
          .PhaseSum()
        / 1000,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public static decimal GetValue(this ObisModel model, IAggregate aggregate)
  {
    if (model is ObisModel.ActiveEnergyTotalImportT0_kWh)
    {
      var maxEnergy = aggregate.ActiveEnergy_Wh
        .TariffUnary()
        .DuplexImport()
        .AggregateMax()
        .PhaseSum();

      var minEnergy = aggregate.ActiveEnergy_Wh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();

      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActiveEnergyTotalImportT1_kWh)
    {
      var maxEnergy = aggregate.ActiveEnergy_Wh
        .TariffBinary().T1
        .DuplexImport()
        .AggregateMax()
        .PhaseSum();

      var minEnergy = aggregate.ActiveEnergy_Wh
        .TariffBinary().T1
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();

      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActiveEnergyTotalImportT2_kWh)
    {
      var maxEnergy = aggregate.ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .AggregateMax()
        .PhaseSum();

      var minEnergy = aggregate.ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();

      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ReactiveEnergyTotalImportT0_kVARh)
    {
      var maxEnergy = aggregate.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexImport()
        .AggregateMax()
        .PhaseSum();

      var minEnergy = aggregate.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexImport()
        .AggregateMin()
        .PhaseSum();

      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ReactiveEnergyTotalExportT0_kVARh)
    {
      var maxEnergy = aggregate.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexExport()
        .AggregateMax()
        .PhaseSum();

      var minEnergy = aggregate.ReactiveEnergy_VARh
        .TariffUnary()
        .DuplexExport()
        .AggregateMin()
        .PhaseSum();

      return (maxEnergy - minEnergy) / 1000;
    }

    if (model is ObisModel.ActivePowerTotalImportT1_kW)
    {
      var power = aggregate.DerivedActivePower_W
        .TariffBinary().T1
        .DuplexImport()
        .AggregateMax()
        .PhaseSum();

      return power / 1000;
    }

    throw new ArgumentOutOfRangeException(nameof(model), model, null);
  }
}
