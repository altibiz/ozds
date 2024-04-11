using System.Text.Json;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries.Agnotic;

namespace Ozds.Business.Iot;

public class OzdsIotHandler
{
  private readonly OzdsAuditableQueries _auditableQueries;

  private readonly IHttpContextAccessor _httpContextAccessor;

  private readonly OzdsMeasurementMutations _measurementMutations;

  private readonly AgnosticPushRequestMeasurementConverter _pushRequestMeasurementConverter;


  public OzdsIotHandler(
    OzdsAuditableQueries auditableQueries,
    IHttpContextAccessor httpContextAccessor,
    AgnosticPushRequestMeasurementConverter pushRequestMeasurementConverter,
    OzdsMeasurementMutations measurementMutations
  )
  {
    _auditableQueries = auditableQueries;
    _httpContextAccessor = httpContextAccessor;
    _pushRequestMeasurementConverter = pushRequestMeasurementConverter;
    _measurementMutations = measurementMutations;
  }

  public Task<bool> Authorize(string id, string request)
  {
    return Task.FromResult(true);
  }

  public Task OnPush(string _, string request)
  {
    var messengerRequest =
      JsonSerializer.Deserialize<MessengerPushRequest>(
        request,
        new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true
        });
    if (messengerRequest?.Measurements is null
        || messengerRequest.Measurements.Length == 0)
    {
      return Task.CompletedTask;
    }

    foreach (var item in messengerRequest.Measurements)
    {
      var measurement = _pushRequestMeasurementConverter.ToMeasurement(
        item.Data, item.MeterId, item.Timestamp
      );
      _measurementMutations.Create(measurement);
    }

    return Task.CompletedTask;
  }

  public Task OnPoll(string id, string request)
  {
    return Task.CompletedTask;
  }
}
