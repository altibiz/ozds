using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Iot.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class PidgeonSchneideriEM3xxxPushRequestMeasurementConverter
  : ConcretePushRequestMeasurementConverter<
      PidgeonSchneideriEM3xxxMeterPushRequestEntity,
      SchneideriEM3xxxMeasurementModel>
{
  public override string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }

  public override void InitializePushRequest(
    SchneideriEM3xxxMeasurementModel measurement,
    PidgeonSchneideriEM3xxxMeterPushRequestEntity pushRequest
  )
  {
#pragma warning disable IDE0017 // Simplify object initialization
    var data = new PidgeonSchneideriEM3xxxMeterPushRequestData();
    data.VoltageL1AnyT0_V = measurement.VoltageL1AnyT0_V;
    data.VoltageL2AnyT0_V = measurement.VoltageL2AnyT0_V;
    data.VoltageL3AnyT0_V = measurement.VoltageL3AnyT0_V;
    data.CurrentL1AnyT0_A = measurement.CurrentL1AnyT0_A;
    data.CurrentL2AnyT0_A = measurement.CurrentL2AnyT0_A;
    data.CurrentL3AnyT0_A = measurement.CurrentL3AnyT0_A;
    data.ActivePowerL1NetT0_W = measurement.ActivePowerL1NetT0_W;
    data.ActivePowerL2NetT0_W = measurement.ActivePowerL2NetT0_W;
    data.ActivePowerL3NetT0_W = measurement.ActivePowerL3NetT0_W;
    data.ReactivePowerTotalNetT0_VAR = measurement.ReactivePowerTotalNetT0_VAR;
    data.ApparentPowerTotalNetT0_VA = measurement.ApparentPowerTotalNetT0_VA;
    data.ActiveEnergyL1ImportT0_Wh = measurement.ActiveEnergyL1ImportT0_Wh;
    data.ActiveEnergyL2ImportT0_Wh = measurement.ActiveEnergyL2ImportT0_Wh;
    data.ActiveEnergyL3ImportT0_Wh = measurement.ActiveEnergyL3ImportT0_Wh;
    data.ActiveEnergyTotalImportT0_Wh =
      measurement.ActiveEnergyTotalImportT0_Wh;
    data.ActiveEnergyTotalExportT0_Wh =
      measurement.ActiveEnergyTotalExportT0_Wh;
    data.ReactiveEnergyTotalImportT0_VARh =
      measurement.ReactiveEnergyTotalImportT0_VARh;
    data.ReactiveEnergyTotalExportT0_VARh =
      measurement.ReactiveEnergyTotalExportT0_VARh;
    data.ActiveEnergyTotalImportT1_Wh =
      measurement.ActiveEnergyTotalImportT1_Wh;
    data.ActiveEnergyTotalImportT2_Wh =
      measurement.ActiveEnergyTotalImportT2_Wh;
#pragma warning restore IDE0017 // Simplify object initialization
    pushRequest.Data = data;
    pushRequest.Timestamp = measurement.Timestamp;
    pushRequest.MeterId = measurement.MeterId;
  }

  public override void InitializeMeasurement(
    PidgeonSchneideriEM3xxxMeterPushRequestEntity pushRequest,
    string measurementLocationId,
    SchneideriEM3xxxMeasurementModel measurement
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
    measurement.ReactivePowerTotalNetT0_VAR =
      pushRequest.Data.ReactivePowerTotalNetT0_VAR;
    measurement.ApparentPowerTotalNetT0_VA =
      pushRequest.Data.ApparentPowerTotalNetT0_VA;
    measurement.ActiveEnergyL1ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL1ImportT0_Wh;
    measurement.ActiveEnergyL2ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL2ImportT0_Wh;
    measurement.ActiveEnergyL3ImportT0_Wh =
      pushRequest.Data.ActiveEnergyL3ImportT0_Wh;
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
