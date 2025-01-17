using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Iot.Entities;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class PidgeonSchneideriEM3xxxPushRequestMeasurementConverter :
  ConcretePushRequestMeasurementConverter<PidgeonSchneideriEM3xxxMeterPushRequestEntity,
    SchneideriEM3xxxMeasurementModel>
{
  protected override string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }

  protected override SchneideriEM3xxxMeasurementModel ToMeasurement(
    PidgeonSchneideriEM3xxxMeterPushRequestEntity pushRequest,
    string measurementLocationId
  )
  {
    return pushRequest.ToModel(measurementLocationId);
  }

  protected override PidgeonSchneideriEM3xxxMeterPushRequestEntity
    ToPushRequest(
      SchneideriEM3xxxMeasurementModel measurement)
  {
    return measurement.ToPushRequest();
  }
}

public static class
  SchneideriEM3xxxPushRequestEntityMeasurementConverterExtensions
{
  public static SchneideriEM3xxxMeasurementModel ToModel(
    this PidgeonSchneideriEM3xxxMeterPushRequestEntity request,
    string measurementLocationId
  )
  {
    return new SchneideriEM3xxxMeasurementModel
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
      ReactivePowerTotalNetT0_VAR = request.Data.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = request.Data.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = request.Data.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.Data.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.Data.ActiveEnergyL3ImportT0_Wh,
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

  public static PidgeonSchneideriEM3xxxMeterPushRequestEntity ToPushRequest(
    this SchneideriEM3xxxMeasurementModel model
  )
  {
    return new PidgeonSchneideriEM3xxxMeterPushRequestEntity(
      model.MeterId,
      model.Timestamp,
      new PidgeonSchneideriEM3xxxMeterPushRequestData(
        model.VoltageL1AnyT0_V,
        model.VoltageL2AnyT0_V,
        model.VoltageL3AnyT0_V,
        model.CurrentL1AnyT0_A,
        model.CurrentL2AnyT0_A,
        model.CurrentL3AnyT0_A,
        model.ActivePowerL1NetT0_W,
        model.ActivePowerL2NetT0_W,
        model.ActivePowerL3NetT0_W,
        model.ReactivePowerTotalNetT0_VAR,
        model.ApparentPowerTotalNetT0_VA,
        model.ActiveEnergyL1ImportT0_Wh,
        model.ActiveEnergyL2ImportT0_Wh,
        model.ActiveEnergyL3ImportT0_Wh,
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
