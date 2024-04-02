using System.Text.Json;
using Ozds.Business.Models;
using Ozds.Business.Mutations;
using Ozds.Business.Queries.Base;

namespace Ozds.Business.Iot;

public class OzdsIotHandler
{
  private readonly OzdsAuditableQueries _auditableQueries;

  private readonly OzdsMeasurementMutations _measurementMutations;

  private readonly IHttpContextAccessor _httpContextAccessor;

  public OzdsIotHandler(
    OzdsAuditableQueries auditableQueries,
    IHttpContextAccessor httpContextAccessor
  )
  {
    _auditableQueries = auditableQueries;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<bool> Authorize(string id, string request)
  {
    return true;
  }

  public async Task OnPush(string _, string request)
  {
    var messengerRequest = JsonSerializer.Deserialize<MessengerPushRequest>(request);
    if (messengerRequest == null)
    {
      return;
    }

    foreach (var item in messengerRequest.Measurements)
    {
      if (item.MeterId.StartsWith("abb-b2x"))
      {
        var deviceRequest = JsonSerializer.Deserialize<AbbB2xPushRequest>(item.Data);
        if (deviceRequest == null)
        {
          continue;
        }

        var model = new AbbB2xMeasurementModel()
        {
          MeterId = item.MeterId,
          Timestamp = item.Timestamp,
          VoltageL1AnyT0_V = deviceRequest.VoltageL1AnyT0_V,
          VoltageL2AnyT0_V = deviceRequest.VoltageL2AnyT0_V,
          VoltageL3AnyT0_V = deviceRequest.VoltageL3AnyT0_V,
          CurrentL1AnyT0_A = deviceRequest.CurrentL1AnyT0_A,
          CurrentL2AnyT0_A = deviceRequest.CurrentL2AnyT0_A,
          CurrentL3AnyT0_A = deviceRequest.CurrentL3AnyT0_A,
          ActivePowerL1NetT0_W = deviceRequest.ActivePowerL1NetT0_W,
          ActivePowerL2NetT0_W = deviceRequest.ActivePowerL2NetT0_W,
          ActivePowerL3NetT0_W = deviceRequest.ActivePowerL3NetT0_W,
          ReactivePowerL1NetT0_VAR = deviceRequest.ReactivePowerL1NetT0_VAR,
          ReactivePowerL2NetT0_VAR = deviceRequest.ReactivePowerL2NetT0_VAR,
          ReactivePowerL3NetT0_VAR = deviceRequest.ReactivePowerL3NetT0_VAR,
          ActiveEnergyL1ImportT0_Wh = deviceRequest.ActiveEnergyL1ImportT0_Wh,
          ActiveEnergyL2ImportT0_Wh = deviceRequest.ActiveEnergyL2ImportT0_Wh,
          ActiveEnergyL3ImportT0_Wh = deviceRequest.ActiveEnergyL3ImportT0_Wh,
          ActiveEnergyL1ExportT0_Wh = deviceRequest.ActiveEnergyL1ExportT0_Wh,
          ActiveEnergyL2ExportT0_Wh = deviceRequest.ActiveEnergyL2ExportT0_Wh,
          ActiveEnergyL3ExportT0_Wh = deviceRequest.ActiveEnergyL3ExportT0_Wh,
          ReactiveEnergyL1ImportT0_VARh = deviceRequest.ReactiveEnergyL1ImportT0_VARh,
          ReactiveEnergyL2ImportT0_VARh = deviceRequest.ReactiveEnergyL2ImportT0_VARh,
          ReactiveEnergyL3ImportT0_VARh = deviceRequest.ReactiveEnergyL3ImportT0_VARh,
          ReactiveEnergyL1ExportT0_VARh = deviceRequest.ReactiveEnergyL1ExportT0_VARh,
          ReactiveEnergyL2ExportT0_VARh = deviceRequest.ReactiveEnergyL2ExportT0_VARh,
          ReactiveEnergyL3ExportT0_VARh = deviceRequest.ReactiveEnergyL3ExportT0_VARh,
          ActiveEnergyTotalImportT0_Wh = deviceRequest.ActiveEnergyTotalImportT0_Wh,
          ActiveEnergyTotalExportT0_Wh = deviceRequest.ActiveEnergyTotalExportT0_Wh,
          ReactiveEnergyTotalImportT0_VARh = deviceRequest.ReactiveEnergyTotalImportT0_VARh,
          ReactiveEnergyTotalExportT0_VARh = deviceRequest.ReactiveEnergyTotalExportT0_VARh,
          ActiveEnergyTotalImportT1_Wh = deviceRequest.ActiveEnergyTotalImportT1_Wh,
          ActiveEnergyTotalImportT2_Wh = deviceRequest.ActiveEnergyTotalImportT2_Wh
        };

        _measurementMutations.Create(model);
      }
      else if (item.MeterId.StartsWith("schneider-iEM3xxx"))
      {
        var deviceRequest = JsonSerializer.Deserialize<SchneideriEM3xxxPushRequest>(item.Data);
        if (deviceRequest == null)
        {
          continue;
        }

        var model = new SchneideriEM3xxxMeasurementModel()
        {
          MeterId = item.MeterId,
          Timestamp = item.Timestamp,
          VoltageL1AnyT0_V = deviceRequest.VoltageL1AnyT0_V,
          VoltageL2AnyT0_V = deviceRequest.VoltageL2AnyT0_V,
          VoltageL3AnyT0_V = deviceRequest.VoltageL3AnyT0_V,
          CurrentL1AnyT0_A = deviceRequest.CurrentL1AnyT0_A,
          CurrentL2AnyT0_A = deviceRequest.CurrentL2AnyT0_A,
          CurrentL3AnyT0_A = deviceRequest.CurrentL3AnyT0_A,
          ActivePowerL1NetT0_W = deviceRequest.ActivePowerL1NetT0_W,
          ActivePowerL2NetT0_W = deviceRequest.ActivePowerL2NetT0_W,
          ActivePowerL3NetT0_W = deviceRequest.ActivePowerL3NetT0_W,
          ReactivePowerTotalNetT0_VAR = deviceRequest.ReactivePowerTotalNetT0_VAR,
          ApparentPowerTotalNetT0_VA = deviceRequest.ApparentPowerTotalNetT0_VA,
          ActiveEnergyL1ImportT0_Wh = deviceRequest.ActiveEnergyL1ImportT0_Wh,
          ActiveEnergyL2ImportT0_Wh = deviceRequest.ActiveEnergyL2ImportT0_Wh,
          ActiveEnergyL3ImportT0_Wh = deviceRequest.ActiveEnergyL3ImportT0_Wh,
          ActiveEnergyTotalImportT0_Wh = deviceRequest.ActiveEnergyTotalImportT0_Wh,
          ActiveEnergyTotalExportT0_Wh = deviceRequest.ActiveEnergyTotalExportT0_Wh,
          ReactiveEnergyTotalImportT0_VARh = deviceRequest.ReactiveEnergyTotalImportT0_VARh,
          ReactiveEnergyTotalExportT0_VARh = deviceRequest.ReactiveEnergyTotalExportT0_VARh,
          ActiveEnergyTotalImportT1_Wh = deviceRequest.ActiveEnergyTotalImportT1_Wh,
          ActiveEnergyTotalImportT2_Wh = deviceRequest.ActiveEnergyTotalImportT2_Wh
        };

        _measurementMutations.Create(model);
      }
    }

  }

  public async Task OnPoll(string id, string request) { }
}
