using Ozds.Business.Math;

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
