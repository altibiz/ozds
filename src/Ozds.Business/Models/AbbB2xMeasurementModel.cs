using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record AbbB2xMeasurementModel(
  string MeterId,
  DateTimeOffset Timestamp,
  float VoltageL1AnyT0_V,
  float VoltageL2AnyT0_V,
  float VoltageL3AnyT0_V,
  float CurrentL1AnyT0_A,
  float CurrentL2AnyT0_A,
  float CurrentL3AnyT0_A,
  float ActivePowerL1NetT0_W,
  float ActivePowerL2NetT0_W,
  float ActivePowerL3NetT0_W,
  float ReactivePowerL1NetT0_VAR,
  float ReactivePowerL2NetT0_VAR,
  float ReactivePowerL3NetT0_VAR,
  float ActiveEnergyL1ImportT0_Wh,
  float ActiveEnergyL2ImportT0_Wh,
  float ActiveEnergyL3ImportT0_Wh,
  float ActiveEnergyL1ExportT0_Wh,
  float ActiveEnergyL2ExportT0_Wh,
  float ActiveEnergyL3ExportT0_Wh,
  float ReactiveEnergyL1ImportT0_VARh,
  float ReactiveEnergyL2ImportT0_VARh,
  float ReactiveEnergyL3ImportT0_VARh,
  float ReactiveEnergyL1ExportT0_VARh,
  float ReactiveEnergyL2ExportT0_VARh,
  float ReactiveEnergyL3ExportT0_VARh,
  float ActiveEnergyTotalImportT0_Wh,
  float ActiveEnergyTotalExportT0_Wh,
  float ReactiveEnergyTotalImportT0_VARh,
  float ReactiveEnergyTotalExportT0_VARh,
  float ActiveEnergyTotalImportT1_Wh,
  float ActiveEnergyTotalImportT2_Wh
) : MeasurementModel(MeterId, Timestamp)
{
  public override TariffMeasure Current_A
  {
    get
    {
      return new UnaryTariffMeasure(
        new AnyDuplexMeasure(
          new TriPhasicMeasure(CurrentL1AnyT0_A, CurrentL2AnyT0_A, CurrentL3AnyT0_A)
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
          new TriPhasicMeasure(VoltageL1AnyT0_V, VoltageL2AnyT0_V, VoltageL3AnyT0_V)
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
          new TriPhasicMeasure(
            ReactivePowerL1NetT0_VAR,
            ReactivePowerL2NetT0_VAR,
            ReactivePowerL3NetT0_VAR
          )
        )
      );
    }
  }

  public override TariffMeasure ApparentPower_VA
  {
    get { return TariffMeasure.Null; }
  }

  public override TariffMeasure ActiveEnergyCumulative_Wh
  {
    get
    {
      return ActiveEnergyL1ImportT0_Wh is not 0
             || ActiveEnergyL2ImportT0_Wh is not 0
             || ActiveEnergyL3ImportT0_Wh is not 0
             || ActiveEnergyL1ExportT0_Wh is not 0
             || ActiveEnergyL2ExportT0_Wh is not 0
             || ActiveEnergyL3ExportT0_Wh is not 0
        ? new CompositeTariffMeasure(new() {
            new UnaryTariffMeasure(
              new ImportExportDuplexMeasure(
                new CompositePhasicMeasure(new() {
                  new TriPhasicMeasure(
                    ActiveEnergyL1ImportT0_Wh,
                    ActiveEnergyL2ImportT0_Wh,
                    ActiveEnergyL3ImportT0_Wh
                  ),
                  new SinglePhasicMeasure(ActiveEnergyTotalImportT0_Wh),
                }),
                new CompositePhasicMeasure(new() {
                  new TriPhasicMeasure(
                    ActiveEnergyL1ExportT0_Wh,
                    ActiveEnergyL2ExportT0_Wh,
                    ActiveEnergyL3ExportT0_Wh
                  ),
                  new SinglePhasicMeasure(ActiveEnergyTotalExportT0_Wh)
                })
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
        : new CompositeTariffMeasure(new() {
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

  public override TariffMeasure ReactiveEnergyCumulative_VARh
  {
    get
    {
      return ReactiveEnergyL1ImportT0_VARh is not 0
             || ReactiveEnergyL2ImportT0_VARh is not 0
             || ReactiveEnergyL3ImportT0_VARh is not 0
             || ReactiveEnergyL1ExportT0_VARh is not 0
             || ReactiveEnergyL2ExportT0_VARh is not 0
             || ReactiveEnergyL3ExportT0_VARh is not 0
        ? new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new CompositePhasicMeasure(new() {
                new TriPhasicMeasure(
                  ReactiveEnergyL1ImportT0_VARh,
                  ReactiveEnergyL2ImportT0_VARh,
                  ReactiveEnergyL3ImportT0_VARh
                ),
                new SinglePhasicMeasure(ReactiveEnergyTotalImportT0_VARh),
              }),
              new CompositePhasicMeasure(new() {
                new TriPhasicMeasure(
                  ReactiveEnergyL1ExportT0_VARh,
                  ReactiveEnergyL2ExportT0_VARh,
                  ReactiveEnergyL3ExportT0_VARh
                ),
                new SinglePhasicMeasure(ReactiveEnergyTotalExportT0_VARh)
              })
            )
          )
        : new UnaryTariffMeasure(
            new ImportExportDuplexMeasure(
              new SinglePhasicMeasure(ReactiveEnergyTotalImportT0_VARh),
              new SinglePhasicMeasure(ReactiveEnergyTotalExportT0_VARh)
            )
          );
    }
  }

  public override TariffMeasure ApparentEnergyCumulative_VAh
  {
    get { return TariffMeasure.Null; }
  }

  public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class AbbB2xMeasurementModelExtensions
{
  public static AbbB2xAggregateModel ToAggregate(
    this AbbB2xMeasurementModel measurement,
    TimeSpan timeSpan) =>
    new(
      measurement.MeterId,
      measurement.Timestamp,
      timeSpan,
      1,
      measurement.VoltageL1AnyT0_V,
      measurement.VoltageL2AnyT0_V,
      measurement.VoltageL3AnyT0_V,
      measurement.CurrentL1AnyT0_A,
      measurement.CurrentL2AnyT0_A,
      measurement.CurrentL3AnyT0_A,
      measurement.ActivePowerL1NetT0_W,
      measurement.ActivePowerL2NetT0_W,
      measurement.ActivePowerL3NetT0_W,
      measurement.ReactivePowerL1NetT0_VAR,
      measurement.ReactivePowerL2NetT0_VAR,
      measurement.ReactivePowerL3NetT0_VAR,
      measurement.ActiveEnergyTotalImportT0_Wh,
      measurement.ActiveEnergyTotalImportT0_Wh,
      measurement.ActiveEnergyTotalExportT0_Wh,
      measurement.ActiveEnergyTotalExportT0_Wh,
      measurement.ReactiveEnergyTotalImportT0_VARh,
      measurement.ReactiveEnergyTotalImportT0_VARh,
      measurement.ReactiveEnergyTotalExportT0_VARh,
      measurement.ReactiveEnergyTotalExportT0_VARh,
      measurement.ActiveEnergyTotalImportT1_Wh,
      measurement.ActiveEnergyTotalImportT1_Wh,
      measurement.ActiveEnergyTotalImportT2_Wh,
      measurement.ActiveEnergyTotalImportT2_Wh
    );

  public static AbbB2xMeasurementModel ToModel(this AbbB2xMeasurementEntity entity) =>
    new(
      entity.Meter.Id,
      entity.Timestamp,
      entity.VoltageL1AnyT0_V,
      entity.VoltageL2AnyT0_V,
      entity.VoltageL3AnyT0_V,
      entity.CurrentL1AnyT0_A,
      entity.CurrentL2AnyT0_A,
      entity.CurrentL3AnyT0_A,
      entity.ActivePowerL1NetT0_W,
      entity.ActivePowerL2NetT0_W,
      entity.ActivePowerL3NetT0_W,
      entity.ReactivePowerL1NetT0_VAR,
      entity.ReactivePowerL2NetT0_VAR,
      entity.ReactivePowerL3NetT0_VAR,
      entity.ActiveEnergyL1ImportT0_Wh,
      entity.ActiveEnergyL2ImportT0_Wh,
      entity.ActiveEnergyL3ImportT0_Wh,
      entity.ActiveEnergyL1ExportT0_Wh,
      entity.ActiveEnergyL2ExportT0_Wh,
      entity.ActiveEnergyL3ExportT0_Wh,
      entity.ReactiveEnergyL1ImportT0_VARh,
      entity.ReactiveEnergyL2ImportT0_VARh,
      entity.ReactiveEnergyL3ImportT0_VARh,
      entity.ReactiveEnergyL1ExportT0_VARh,
      entity.ReactiveEnergyL2ExportT0_VARh,
      entity.ReactiveEnergyL3ExportT0_VARh,
      entity.ActiveEnergyTotalImportT0_Wh,
      entity.ActiveEnergyTotalExportT0_Wh,
      entity.ReactiveEnergyTotalImportT0_VARh,
      entity.ReactiveEnergyTotalExportT0_VARh,
      entity.ActiveEnergyTotalImportT1_Wh,
      entity.ActiveEnergyTotalImportT2_Wh
    );

  public static AbbB2xMeasurementEntity ToEntity(this AbbB2xMeasurementModel model) =>
    new()
    {
      Meter = new() { Id = model.MeterId },
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
      ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = model.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = model.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = model.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = model.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = model.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = model.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh
    };
}
