using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Iot.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class PidgeonAbbB2xPushRequestMeasurementConverter
  : ConcretePushRequestMeasurementConverter<
      PidgeonAbbB2xMeterPushRequestEntity,
      AbbB2xMeasurementModel>
{
  public override string MeterIdPrefix
  {
    get { return "abb-B2x"; }
  }

  public override void InitializePushRequest(
    AbbB2xMeasurementModel measurement,
    PidgeonAbbB2xMeterPushRequestEntity pushRequest
  )
  {
#pragma warning disable IDE0017 // Simplify object initialization
    var data = new PidgeonAbbB2xMeterPushRequestData();
    data.VoltageL1AnyT0_V = measurement.VoltageL1AnyT0_V;
    data.VoltageL2AnyT0_V = measurement.VoltageL2AnyT0_V;
    data.VoltageL3AnyT0_V = measurement.VoltageL3AnyT0_V;
    data.CurrentL1AnyT0_A = measurement.CurrentL1AnyT0_A;
    data.CurrentL2AnyT0_A = measurement.CurrentL2AnyT0_A;
    data.CurrentL3AnyT0_A = measurement.CurrentL3AnyT0_A;
    data.ActivePowerL1NetT0_W = measurement.ActivePowerL1NetT0_W;
    data.ActivePowerL2NetT0_W = measurement.ActivePowerL2NetT0_W;
    data.ActivePowerL3NetT0_W = measurement.ActivePowerL3NetT0_W;
    data.ReactivePowerL1NetT0_VAR = measurement.ReactivePowerL1NetT0_VAR;
    data.ReactivePowerL2NetT0_VAR = measurement.ReactivePowerL2NetT0_VAR;
    data.ReactivePowerL3NetT0_VAR = measurement.ReactivePowerL3NetT0_VAR;
    data.ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh;
    data.ActiveEnergyL2ImportT0_Wh = measurement.ActiveEnergyL2ImportT0_Wh;
    data.ActiveEnergyL3ImportT0_Wh = measurement.ActiveEnergyL3ImportT0_Wh;
    data.ActiveEnergyL1ExportT0_Wh = measurement.ActiveEnergyL1ExportT0_Wh;
    data.ActiveEnergyL2ExportT0_Wh = measurement.ActiveEnergyL2ExportT0_Wh;
    data.ActiveEnergyL3ExportT0_Wh = measurement.ActiveEnergyL3ExportT0_Wh;
    data.ReactiveEnergyL1ImportT0_VARh = measurement.ReactiveEnergyL1ImportT0_VARh;
    data.ReactiveEnergyL2ImportT0_VARh = measurement.ReactiveEnergyL2ImportT0_VARh;
    data.ReactiveEnergyL3ImportT0_VARh = measurement.ReactiveEnergyL3ImportT0_VARh;
    data.ReactiveEnergyL1ExportT0_VARh = measurement.ReactiveEnergyL1ExportT0_VARh;
    data.ReactiveEnergyL2ExportT0_VARh = measurement.ReactiveEnergyL2ExportT0_VARh;
    data.ReactiveEnergyL3ExportT0_VARh = measurement.ReactiveEnergyL3ExportT0_VARh;
    data.ActiveEnergyTotalImportT0_Wh = measurement.ActiveEnergyTotalImportT0_Wh;
    data.ActiveEnergyTotalExportT0_Wh = measurement.ActiveEnergyTotalExportT0_Wh;
    data.ReactiveEnergyTotalImportT0_VARh = measurement.ReactiveEnergyTotalImportT0_VARh;
    data.ReactiveEnergyTotalExportT0_VARh = measurement.ReactiveEnergyTotalExportT0_VARh;
    data.ActiveEnergyTotalImportT1_Wh = measurement.ActiveEnergyTotalImportT1_Wh;
    data.ActiveEnergyTotalImportT2_Wh = measurement.ActiveEnergyTotalImportT2_Wh;
#pragma warning restore IDE0017 // Simplify object initialization
    pushRequest.Data = data;
    pushRequest.Timestamp = measurement.Timestamp;
    pushRequest.MeterId = measurement.MeterId;
  }

  public override void InitializeMeasurement(
    PidgeonAbbB2xMeterPushRequestEntity pushRequest,
    string measurementLocationId,
    AbbB2xMeasurementModel measurement
  )
  {
    measurement.MeterId = pushRequest.MeterId;
    measurement.MeasurementLocationId = measurementLocationId;
    measurement.Timestamp = pushRequest.Timestamp;
    measurement.VoltageL1AnyT0_V = pushRequest.Data.VoltageL1AnyT0_V;
    measurement.VoltageL2AnyT0_V = pushRequest.Data.VoltageL2AnyT0_V;
    measurement.VoltageL3AnyT0_V = pushRequest.Data.VoltageL3AnyT0_V;
    measurement.CurrentL1AnyT0_A = pushRequest.Data.CurrentL1AnyT0_A;
    measurement.CurrentL2AnyT0_A = pushRequest.Data.CurrentL2AnyT0_A;
    measurement.CurrentL3AnyT0_A = pushRequest.Data.CurrentL3AnyT0_A;
    measurement.ActivePowerL1NetT0_W = pushRequest.Data.ActivePowerL1NetT0_W;
    measurement.ActivePowerL2NetT0_W = pushRequest.Data.ActivePowerL2NetT0_W;
    measurement.ActivePowerL3NetT0_W = pushRequest.Data.ActivePowerL3NetT0_W;
    measurement.ReactivePowerL1NetT0_VAR =
      pushRequest.Data.ReactivePowerL1NetT0_VAR;
    measurement.ReactivePowerL2NetT0_VAR =
      pushRequest.Data.ReactivePowerL2NetT0_VAR;
    measurement.ReactivePowerL3NetT0_VAR =
      pushRequest.Data.ReactivePowerL3NetT0_VAR;
    measurement.ActiveEnergyL1ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL1ImportT0_Wh;
    measurement.ActiveEnergyL2ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL2ImportT0_Wh;
    measurement.ActiveEnergyL3ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL3ImportT0_Wh;
    measurement.ActiveEnergyL1ExportT0_Wh =
      pushRequest.Data.ActiveEnergyL1ExportT0_Wh;
    measurement.ActiveEnergyL2ExportT0_Wh =
      pushRequest.Data.ActiveEnergyL2ExportT0_Wh;
    measurement.ActiveEnergyL3ExportT0_Wh =
      pushRequest.Data.ActiveEnergyL3ExportT0_Wh;
    measurement.ReactiveEnergyL1ImportT0_VARh =
      pushRequest.Data.ReactiveEnergyL1ImportT0_VARh;
    measurement.ReactiveEnergyL2ImportT0_VARh =
      pushRequest.Data.ReactiveEnergyL2ImportT0_VARh;
    measurement.ReactiveEnergyL3ImportT0_VARh =
      pushRequest.Data.ReactiveEnergyL3ImportT0_VARh;
    measurement.ReactiveEnergyL1ExportT0_VARh =
      pushRequest.Data.ReactiveEnergyL1ExportT0_VARh;
    measurement.ReactiveEnergyL2ExportT0_VARh =
      pushRequest.Data.ReactiveEnergyL2ExportT0_VARh;
    measurement.ReactiveEnergyL3ExportT0_VARh =
      pushRequest.Data.ReactiveEnergyL3ExportT0_VARh;
    measurement.ActiveEnergyTotalImportT0_Wh =
      pushRequest.Data.ActiveEnergyTotalImportT0_Wh;
    measurement.ActiveEnergyTotalExportT0_Wh =
      pushRequest.Data.ActiveEnergyTotalExportT0_Wh;
    measurement.ReactiveEnergyTotalImportT0_VARh =
      pushRequest.Data.ReactiveEnergyTotalImportT0_VARh;
    measurement.ReactiveEnergyTotalExportT0_VARh =
      pushRequest.Data.ReactiveEnergyTotalExportT0_VARh;
    measurement.ActiveEnergyTotalImportT1_Wh =
      pushRequest.Data.ActiveEnergyTotalImportT1_Wh;
    measurement.ActiveEnergyTotalImportT2_Wh =
      pushRequest.Data.ActiveEnergyTotalImportT2_Wh;
  }
}
