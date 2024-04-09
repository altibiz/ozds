using System.Text.Json;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries.Agnotic;

namespace Ozds.Business.Iot;

public class OzdsIotHandler
{
  private readonly OzdsAuditableQueries _auditableQueries;

  private readonly IHttpContextAccessor _httpContextAccessor;

  private readonly OzdsMeasurementMutations _measurementMutations;

  private readonly IServiceProvider _serviceProvider;

  public OzdsIotHandler(
    OzdsAuditableQueries auditableQueries,
    IHttpContextAccessor httpContextAccessor,
    IServiceProvider serviceProvider
  )
  {
    _auditableQueries = auditableQueries;
    _httpContextAccessor = httpContextAccessor;
    _serviceProvider = serviceProvider;
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

    var pushRequestMeasurementConverters = _serviceProvider
      .GetServices<IPushRequestMeasurementConverter>();

    foreach (var item in messengerRequest.Measurements)
    {
      var pushRequestMeasurementConverter = pushRequestMeasurementConverters
        .FirstOrDefault(x => x.CanConvert(item.MeterId));

      if (pushRequestMeasurementConverter == null)
      {
        continue;
      }

      var measurement = pushRequestMeasurementConverter.ToMeasurement(
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
