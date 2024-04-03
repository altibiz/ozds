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
        _measurementMutations.Create(deviceRequest.ToModel(item.MeterId, item.Timestamp));
      }
      else if (item.MeterId.StartsWith("schneider-iEM3xxx"))
      {
        var deviceRequest = JsonSerializer.Deserialize<SchneideriEM3xxxPushRequest>(item.Data);
        if (deviceRequest == null)
        {
          continue;
        }
        _measurementMutations.Create(deviceRequest.ToModel(item.MeterId, item.Timestamp));
      }
    }
  }

  public async Task OnPoll(string id, string request) { }
}
