using Ozds.Business.Math;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record AbbB2xMeasurementModel(
  string Source,
  DateTimeOffset Timestamp,
  float VoltageL1_V,
  float VoltageL2_V,
  float VoltageL3_V,
  float CurrentL1_A,
  float CurrentL2_A,
  float CurrentL3_A,
  float ActivePowerL1_W,
  float ActivePowerL2_W,
  float ActivePowerL3_W,
  float ReactivePowerL1_VAR,
  float ReactivePowerL2_VAR,
  float ReactivePowerL3_VAR,
  float ActiveEnergyImportL1_Wh,
  float ActiveEnergyImportL2_Wh,
  float ActiveEnergyImportL3_Wh,
  float ActiveEnergyExportL1_Wh,
  float ActiveEnergyExportL2_Wh,
  float ActiveEnergyExportL3_Wh,
  float ReactiveEnergyImportL1_VARh,
  float ReactiveEnergyImportL2_VARh,
  float ReactiveEnergyImportL3_VARh,
  float ReactiveEnergyExportL1_VARh,
  float ReactiveEnergyExportL2_VARh,
  float ReactiveEnergyExportL3_VARh,
  float ActiveEnergyImportTotal_Wh,
  float ActiveEnergyExportTotal_Wh,
  float ReactiveEnergyImportTotal_VARh,
  float ReactiveEnergyExportTotal_VARh,
  float ActiveEnergyImportTotalT1_Wh,
  float ActiveEnergyImportTotalT2_Wh
) : IMeasurement
{
  string IMeasurement.Source
  {
    get { return Source; }
  }

  DateTimeOffset IMeasurement.Timestamp
  {
    get { return Timestamp; }
  }

  DuplexMeasure IMeasurement.Current_A
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(CurrentL1_A, CurrentL2_A, CurrentL3_A)
      );
    }
  }

  DuplexMeasure IMeasurement.Voltage_V
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(VoltageL1_V, VoltageL2_V, VoltageL3_V)
      );
    }
  }

  DuplexMeasure IMeasurement.ActivePower_W
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(
          ActivePowerL1_W,
          ActivePowerL2_W,
          ActivePowerL3_W
        )
      );
    }
  }

  DuplexMeasure IMeasurement.ReactivePower_VAR
  {
    get
    {
      return new NetDuplexMeasure(
        new TriPhasicMeasure(
          ReactivePowerL1_VAR,
          ReactivePowerL2_VAR,
          ReactivePowerL3_VAR
        )
      );
    }
  }

  DuplexMeasure IMeasurement.ApparentPower_VA
  {
    get { return DuplexMeasure.Null; }
  }

  DuplexMeasure IMeasurement.ActiveEnergyCumulative_Wh
  {
    get
    {
      return ActiveEnergyImportL1_Wh is not 0
             && ActiveEnergyImportL2_Wh is not 0
             && ActiveEnergyImportL3_Wh is not 0
             && ActiveEnergyExportL1_Wh is not 0
             && ActiveEnergyExportL2_Wh is not 0
             && ActiveEnergyExportL3_Wh is not 0
        ? new ImportExportDuplexMeasure(
          new TriPhasicMeasure(
            ActiveEnergyImportL1_Wh,
            ActiveEnergyImportL2_Wh,
            ActiveEnergyImportL3_Wh
          ),
          new TriPhasicMeasure(
            ActiveEnergyExportL1_Wh,
            ActiveEnergyExportL2_Wh,
            ActiveEnergyExportL3_Wh
          )
        )
        : new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ActiveEnergyImportTotal_Wh),
          new SinglePhasicMeasure(ActiveEnergyExportTotal_Wh)
        );
    }
  }

  DuplexMeasure IMeasurement.ReactiveEnergyCumulative_VARh
  {
    get
    {
      return ReactiveEnergyImportL1_VARh is not 0
             && ReactiveEnergyImportL2_VARh is not 0
             && ReactiveEnergyImportL3_VARh is not 0
             && ReactiveEnergyExportL1_VARh is not 0
             && ReactiveEnergyExportL2_VARh is not 0
             && ReactiveEnergyExportL3_VARh is not 0
        ? new ImportExportDuplexMeasure(
          new TriPhasicMeasure(
            ReactiveEnergyImportL1_VARh,
            ReactiveEnergyImportL2_VARh,
            ReactiveEnergyImportL3_VARh
          ),
          new TriPhasicMeasure(
            ReactiveEnergyExportL1_VARh,
            ReactiveEnergyExportL2_VARh,
            ReactiveEnergyExportL3_VARh
          )
        )
        : new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ReactiveEnergyImportTotal_VARh),
          new SinglePhasicMeasure(ReactiveEnergyExportTotal_VARh)
        );
    }
  }

  DuplexMeasure IMeasurement.ApparentEnergyCumulative_VAh
  {
    get { return DuplexMeasure.Null; }
  }

  TariffMeasure IMeasurement.TariffEnergyCumulative_Wh
  {
    get
    {
      return new BinaryTariffMeasure(
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ActiveEnergyImportTotalT1_Wh),
          PhasicMeasure.Null
        ),
        new ImportExportDuplexMeasure(
          new SinglePhasicMeasure(ActiveEnergyImportTotalT2_Wh),
          PhasicMeasure.Null
        )
      );
    }
  }
}

public static class AbbB2xMeasurementModelExtensions
{
  public static AbbB2xMeasurementModel ToModel(this AbbB2xMeasurementEntity entity) =>
    new(
      entity.Source,
      entity.Timestamp,
      entity.VoltageL1_V,
      entity.VoltageL2_V,
      entity.VoltageL3_V,
      entity.CurrentL1_A,
      entity.CurrentL2_A,
      entity.CurrentL3_A,
      entity.ActivePowerL1_W,
      entity.ActivePowerL2_W,
      entity.ActivePowerL3_W,
      entity.ReactivePowerL1_VAR,
      entity.ReactivePowerL2_VAR,
      entity.ReactivePowerL3_VAR,
      entity.ActiveEnergyImportL1_Wh,
      entity.ActiveEnergyImportL2_Wh,
      entity.ActiveEnergyImportL3_Wh,
      entity.ActiveEnergyExportL1_Wh,
      entity.ActiveEnergyExportL2_Wh,
      entity.ActiveEnergyExportL3_Wh,
      entity.ReactiveEnergyImportL1_VARh,
      entity.ReactiveEnergyImportL2_VARh,
      entity.ReactiveEnergyImportL3_VARh,
      entity.ReactiveEnergyExportL1_VARh,
      entity.ReactiveEnergyExportL2_VARh,
      entity.ReactiveEnergyExportL3_VARh,
      entity.ActiveEnergyImportTotal_Wh,
      entity.ActiveEnergyExportTotal_Wh,
      entity.ReactiveEnergyImportTotal_VARh,
      entity.ReactiveEnergyExportTotal_VARh,
      entity.ActiveEnergyImportTotalT1_Wh,
      entity.ActiveEnergyImportTotalT2_Wh
    );

  public static AbbB2xMeasurementEntity ToEntity(this AbbB2xMeasurementModel model) =>
    new()
    {
      Source = model.Source,
      Timestamp = model.Timestamp,
      VoltageL1_V = model.VoltageL1_V,
      VoltageL2_V = model.VoltageL2_V,
      VoltageL3_V = model.VoltageL3_V,
      CurrentL1_A = model.CurrentL1_A,
      CurrentL2_A = model.CurrentL2_A,
      CurrentL3_A = model.CurrentL3_A,
      ActivePowerL1_W = model.ActivePowerL1_W,
      ActivePowerL2_W = model.ActivePowerL2_W,
      ActivePowerL3_W = model.ActivePowerL3_W,
      ReactivePowerL1_VAR = model.ReactivePowerL1_VAR,
      ReactivePowerL2_VAR = model.ReactivePowerL2_VAR,
      ReactivePowerL3_VAR = model.ReactivePowerL3_VAR,
      ActiveEnergyImportL1_Wh = model.ActiveEnergyImportL1_Wh,
      ActiveEnergyImportL2_Wh = model.ActiveEnergyImportL2_Wh,
      ActiveEnergyImportL3_Wh = model.ActiveEnergyImportL3_Wh,
      ActiveEnergyExportL1_Wh = model.ActiveEnergyExportL1_Wh,
      ActiveEnergyExportL2_Wh = model.ActiveEnergyExportL2_Wh,
      ActiveEnergyExportL3_Wh = model.ActiveEnergyExportL3_Wh,
      ReactiveEnergyImportL1_VARh = model.ReactiveEnergyImportL1_VARh,
      ReactiveEnergyImportL2_VARh = model.ReactiveEnergyImportL2_VARh,
      ReactiveEnergyImportL3_VARh = model.ReactiveEnergyImportL3_VARh,
      ReactiveEnergyExportL1_VARh = model.ReactiveEnergyExportL1_VARh,
      ReactiveEnergyExportL2_VARh = model.ReactiveEnergyExportL2_VARh,
      ReactiveEnergyExportL3_VARh = model.ReactiveEnergyExportL3_VARh,
      ActiveEnergyImportTotal_Wh = model.ActiveEnergyImportTotal_Wh,
      ActiveEnergyExportTotal_Wh = model.ActiveEnergyExportTotal_Wh,
      ReactiveEnergyImportTotal_VARh = model.ReactiveEnergyImportTotal_VARh,
      ReactiveEnergyExportTotal_VARh = model.ReactiveEnergyExportTotal_VARh,
      ActiveEnergyImportTotalT1_Wh = model.ActiveEnergyImportTotalT1_Wh,
      ActiveEnergyImportTotalT2_Wh = model.ActiveEnergyImportTotalT2_Wh
    };
}
