using Ozds.Business.Conversion.Base;
using Ozds.Business.Iot;
using Ozds.Business.Models;

namespace Ozds.Business.Conversion;

public class AbbB2xPushRequestMeasurementConverter : PushRequestMeasurementConverter<AbbB2xPushRequest, AbbB2xMeasurementModel>
{
  protected override string MeterIdPrefix => "abb-B2x";

  protected override AbbB2xMeasurementModel ToMeasurement(AbbB2xPushRequest pushRequest, string meterId, DateTimeOffset timestamp) =>
    pushRequest.ToModel(meterId, timestamp);

  protected override AbbB2xPushRequest ToPushRequest(AbbB2xMeasurementModel measurement) =>
    measurement.ToPushRequest();
}


public static class AbbB2xPushRequestMeasurementConverterExtensions
{
  public static AbbB2xMeasurementModel ToModel(
    this AbbB2xPushRequest request,
    string meterId,
    DateTimeOffset timestamp
  ) =>
    new()
    {
      MeterId = meterId,
      Timestamp = timestamp,
      VoltageL1AnyT0_V = request.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = request.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = request.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = request.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = request.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = request.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = request.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = request.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = request.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = request.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = request.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = request.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = request.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = request.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = request.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = request.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = request.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = request.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = request.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = request.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = request.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = request.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = request.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = request.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = request.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = request.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = request.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = request.ActiveEnergyTotalImportT2_Wh
    };

  public static AbbB2xPushRequest ToPushRequest(
    this AbbB2xMeasurementModel model
  ) =>
    new(
      VoltageL1AnyT0_V: model.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V: model.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V: model.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A: model.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A: model.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A: model.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W: model.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W: model.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W: model.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR: model.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR: model.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR: model.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh: model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh: model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh: model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh: model.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh: model.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh: model.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh: model.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh: model.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh: model.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh: model.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh: model.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh: model.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh: model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh: model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh: model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh: model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh: model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh: model.ActiveEnergyTotalImportT2_Wh
    );
}
