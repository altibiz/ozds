using Ozds.Business.Conversion.Base;
using Ozds.Business.Iot;
using Ozds.Business.Models;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxPushRequestMeasurementConverter :
  PushRequestMeasurementConverter<SchneideriEM3xxxPushRequest,
    SchneideriEM3xxxMeasurementModel>
{
  protected override string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }

  protected override SchneideriEM3xxxMeasurementModel ToMeasurement(
    SchneideriEM3xxxPushRequest pushRequest,
    string meterId,
    DateTimeOffset timestamp)
  {
    return pushRequest.ToModel(meterId, timestamp);
  }

  protected override SchneideriEM3xxxPushRequest ToPushRequest(
    SchneideriEM3xxxMeasurementModel measurement)
  {
    return measurement.ToPushRequest();
  }
}

public static class SchneideriEM3xxxPushRequestMeasurementConverterExtensions
{
  public static SchneideriEM3xxxMeasurementModel ToModel(
    this SchneideriEM3xxxPushRequest request,
    string meterId,
    DateTimeOffset timestamp
  )
  {
    return new SchneideriEM3xxxMeasurementModel
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
      ReactivePowerTotalNetT0_VAR = request.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = request.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = request.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = request.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = request.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        request.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        request.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = request.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = request.ActiveEnergyTotalImportT2_Wh
    };
  }

  public static SchneideriEM3xxxPushRequest ToPushRequest(
    this SchneideriEM3xxxMeasurementModel model
  )
  {
    return new SchneideriEM3xxxPushRequest(
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
    );
  }
}
