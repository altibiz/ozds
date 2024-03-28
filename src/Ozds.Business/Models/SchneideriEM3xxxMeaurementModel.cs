using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeasurementModel : MeasurementModel<SchneideriEM3xxxMeasurementValidatorModel>
{
  [Required]
  public required float VoltageL1AnyT0_V { get; init; }
  [Required]
  public required float VoltageL2AnyT0_V { get; init; }
  [Required]
  public required float VoltageL3AnyT0_V { get; init; }
  [Required]
  public required float CurrentL1AnyT0_A { get; init; }
  [Required]
  public required float CurrentL2AnyT0_A { get; init; }
  [Required]
  public required float CurrentL3AnyT0_A { get; init; }
  [Required]
  public required float ActivePowerL1NetT0_W { get; init; }
  [Required]
  public required float ActivePowerL2NetT0_W { get; init; }
  [Required]
  public required float ActivePowerL3NetT0_W { get; init; }
  [Required]
  public required float ReactivePowerTotalNetT0_VAR { get; init; }
  [Required]
  public required float ApparentPowerTotalNetT0_VA { get; init; }
  [Required]
  public required float ActiveEnergyL1ImportT0_Wh { get; init; }
  [Required]
  public required float ActiveEnergyL2ImportT0_Wh { get; init; }
  [Required]
  public required float ActiveEnergyL3ImportT0_Wh { get; init; }
  [Required]
  public required float ActiveEnergyTotalImportT0_Wh { get; init; }
  [Required]
  public required float ActiveEnergyTotalExportT0_Wh { get; init; }
  [Required]
  public required float ReactiveEnergyTotalImportT0_VARh { get; init; }
  [Required]
  public required float ReactiveEnergyTotalExportT0_VARh { get; init; }
  [Required]
  public required float ActiveEnergyTotalImportT1_Wh { get; init; }
  [Required]
  public required float ActiveEnergyTotalImportT2_Wh { get; init; }

  public override TariffMeasure Current_A
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            CurrentL1AnyT0_A,
            CurrentL2AnyT0_A,
            CurrentL3AnyT0_A
          )
        )
      );
    }
  }

  public override TariffMeasure Voltage_V
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(
            VoltageL1AnyT0_V,
            VoltageL2AnyT0_V,
            VoltageL3AnyT0_V
          )
        )
      );
    }
  }

  public override TariffMeasure ActivePower_W
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new TriPhasicMeasure(
            ActivePowerL1NetT0_W,
            ActivePowerL2NetT0_W,
            ActivePowerL3NetT0_W
          )
        )
      );
    }
  }

  public override TariffMeasure ReactivePower_VAR
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new SinglePhasicMeasure(
            ReactivePowerTotalNetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure ApparentPower_VA
  {
    get
    {
      return new UnaryTariffMeasure(
        new NetDuplexMeasure(
          new SinglePhasicMeasure(
            ApparentPowerTotalNetT0_VA
          )
        )
      );
    }
  }


  public override TariffMeasure ActiveEnergy_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
             && ActiveEnergyL2ImportT0_Wh is not 0
             && ActiveEnergyL3ImportT0_Wh is not 0
        ? new CompositeTariffMeasure(new List<TariffMeasure>
        {
          new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new TriPhasicMeasure(
                ActiveEnergyL1ImportT0_Wh,
                ActiveEnergyL2ImportT0_Wh,
                ActiveEnergyL3ImportT0_Wh
              ),
              new SinglePhasicMeasure(
                ActiveEnergyTotalExportT0_Wh
              )
            )
          ),
          new BinaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure.Null
            ),
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure.Null
            )
          )
        })
        : new CompositeTariffMeasure(new List<TariffMeasure>
        {
          new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT0_Wh),
              new SinglePhasicMeasure(ActiveEnergyTotalExportT0_Wh)
            )
          ),
          new BinaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT1_Wh),
              PhasicMeasure.Null
            ),
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ActiveEnergyTotalImportT2_Wh),
              PhasicMeasure.Null
            )
          )
        });
    }
  }

  public override TariffMeasure ReactiveEnergy_VARh
  {
    get
    {
      return new UnaryTariffMeasure(
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ReactiveEnergyTotalImportT0_VARh),
          new SinglePhasicMeasure(ReactiveEnergyTotalExportT0_VARh)
        )
      );
    }
  }

  public override TariffMeasure ApparentEnergy_VAh
  {
    get { return TariffMeasure.Null; }
  }
}

public static class SchneideriEM3xxxMeasurementModelExtensions
{
  public static SchneideriEM3xxxAggregateModel ToAggregate(
    this SchneideriEM3xxxMeasurementModel measurement,
    IntervalModel interval)
  {
    return new SchneideriEM3xxxAggregateModel()
    {
      MeterId = measurement.MeterId,
      Timestamp = measurement.Timestamp,
      Interval = interval,
      Count = 1,
      VoltageL1AnyT0Avg_V = measurement.VoltageL1AnyT0_V,
      VoltageL2AnyT0Avg_V = measurement.VoltageL2AnyT0_V,
      VoltageL3AnyT0Avg_V = measurement.VoltageL3AnyT0_V,
      CurrentL1AnyT0Avg_A = measurement.CurrentL1AnyT0_A,
      CurrentL2AnyT0Avg_A = measurement.CurrentL2AnyT0_A,
      CurrentL3AnyT0Avg_A = measurement.CurrentL3AnyT0_A,
      ActivePowerL1NetT0Avg_W = measurement.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0Avg_W = measurement.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0Avg_W = measurement.ActivePowerL3NetT0_W,
      ReactivePowerTotalNetT0Avg_VAR = measurement.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0Avg_VA = measurement.ApparentPowerTotalNetT0_VA,
      ActiveEnergyTotalImportT0Min_Wh = measurement.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalImportT0Max_Wh = measurement.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0Min_Wh = measurement.ActiveEnergyTotalExportT0_Wh,
      ActiveEnergyTotalExportT0Max_Wh = measurement.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0Min_VARh = measurement.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalImportT0Max_VARh = measurement.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0Min_VARh = measurement.ReactiveEnergyTotalExportT0_VARh,
      ReactiveEnergyTotalExportT0Max_VARh = measurement.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1Min_Wh = measurement.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT1Max_Wh = measurement.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2Min_Wh = measurement.ActiveEnergyTotalImportT2_Wh,
      ActiveEnergyTotalImportT2Max_Wh = measurement.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static SchneideriEM3xxxMeasurementModel ToModel(
    this SchneideriEM3xxxMeasurementEntity entity)
  {
    return new SchneideriEM3xxxMeasurementModel()
    {
      MeterId = entity.MeterId,
      Timestamp = entity.Timestamp,
      VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W,
      ReactivePowerTotalNetT0_VAR = entity.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = entity.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = entity.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = entity.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = entity.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = entity.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = entity.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = entity.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static SchneideriEM3xxxMeasurementEntity ToEntity(
    this SchneideriEM3xxxMeasurementModel model)
  {
    return new SchneideriEM3xxxMeasurementEntity
    {
      MeterId = model.MeterId,
      Timestamp = model.Timestamp,
      VoltageL1AnyT0_V = model.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = model.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = model.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = model.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = model.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = model.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W,
      ReactivePowerTotalNetT0_VAR = model.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = model.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh
    };
  }
}
