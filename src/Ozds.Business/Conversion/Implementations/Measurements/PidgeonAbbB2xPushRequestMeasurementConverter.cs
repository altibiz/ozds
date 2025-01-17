using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Iot.Entities;

namespace Ozds.Business.Conversion;

public class PidgeonAbbB2xPushRequestMeasurementConverter :
  ConcretePushRequestMeasurementConverter<PidgeonAbbB2xMeterPushRequestEntity,
    AbbB2xMeasurementModel>
{
  protected override string MeterIdPrefix
  {
    get { return "abb-B2x"; }
  }

  protected override AbbB2xMeasurementModel ToMeasurement(
    PidgeonAbbB2xMeterPushRequestEntity pushRequest,
    string measurementLocationId
  )
  {
    return pushRequest.ToModel(measurementLocationId);
  }

  protected override PidgeonAbbB2xMeterPushRequestEntity ToPushRequest(
    AbbB2xMeasurementModel measurement)
  {
    return measurement.ToPushRequest();
  }
}

public static class AbbB2xPushRequestEntityMeasurementConverterExtensions
{
  public static AbbB2xMeasurementModel ToModel(
    this PidgeonAbbB2xMeterPushRequestEntity request,
    string measurementLocationId
  )
  {
    return new AbbB2xMeasurementModel
    {
      MeterId = request.MeterId,
      MeasurementLocationId = measurementLocationId,
      Timestamp = request.Timestamp,
      VoltageL1AnyT0_V = request.Data.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = request.Data.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = request.Data.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = request.Data.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = request.Data.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = request.Data.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = request.Data.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = request.Data.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = request.Data.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = request.Data.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = request.Data.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = request.Data.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = request.Data.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.Data.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.Data.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = request.Data.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = request.Data.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = request.Data.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh =
        request.Data.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh =
        request.Data.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh =
        request.Data.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh =
        request.Data.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh =
        request.Data.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh =
        request.Data.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = request.Data.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = request.Data.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        request.Data.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        request.Data.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = request.Data.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = request.Data.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static PidgeonAbbB2xMeterPushRequestEntity ToPushRequest(
    this AbbB2xMeasurementModel model
  )
  {
    return new PidgeonAbbB2xMeterPushRequestEntity(
      model.MeterId,
      model.Timestamp,
      new PidgeonAbbB2xMeterPushRequestData(
        model.VoltageL1AnyT0_V,
        model.VoltageL2AnyT0_V,
        model.VoltageL3AnyT0_V,
        model.CurrentL1AnyT0_A,
        model.CurrentL2AnyT0_A,
        model.CurrentL3AnyT0_A,
        model.ActivePowerL1NetT0_W,
        model.ActivePowerL2NetT0_W,
        model.ActivePowerL3NetT0_W,
        model.ReactivePowerL1NetT0_VAR,
        model.ReactivePowerL2NetT0_VAR,
        model.ReactivePowerL3NetT0_VAR,
        model.ActiveEnergyL1ImportT0_Wh,
        model.ActiveEnergyL2ImportT0_Wh,
        model.ActiveEnergyL3ImportT0_Wh,
        model.ActiveEnergyL1ExportT0_Wh,
        model.ActiveEnergyL2ExportT0_Wh,
        model.ActiveEnergyL3ExportT0_Wh,
        model.ReactiveEnergyL1ImportT0_VARh,
        model.ReactiveEnergyL2ImportT0_VARh,
        model.ReactiveEnergyL3ImportT0_VARh,
        model.ReactiveEnergyL1ExportT0_VARh,
        model.ReactiveEnergyL2ExportT0_VARh,
        model.ReactiveEnergyL3ExportT0_VARh,
        model.ActiveEnergyTotalImportT0_Wh,
        model.ActiveEnergyTotalExportT0_Wh,
        model.ReactiveEnergyTotalImportT0_VARh,
        model.ReactiveEnergyTotalExportT0_VARh,
        model.ActiveEnergyTotalImportT1_Wh,
        model.ActiveEnergyTotalImportT2_Wh
      )
    );
  }
}
