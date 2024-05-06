using System.Text.Json;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Mutations.Agnostic;

namespace Ozds.Business.Iot;

public class OzdsIotHandler(
  AgnosticPushRequestMeasurementConverter pushRequestMeasurementConverter,
  OzdsMeasurementMutations measurementMutations
)
{
  private static readonly JsonSerializerOptions deserializationOptions =
    new()
    {
      PropertyNameCaseInsensitive = true
    };

  private readonly OzdsMeasurementMutations _measurementMutations =
    measurementMutations;

  private readonly AgnosticPushRequestMeasurementConverter
    _pushRequestMeasurementConverter = pushRequestMeasurementConverter;

  public Task<bool> Authorize(string id, string request)
  {
    return Task.FromResult(true);
  }

  public async Task OnPush(string _, string request)
  {
    var messengerRequest =
      JsonSerializer.Deserialize<MessengerPushRequest>(
        request, deserializationOptions);
    if (messengerRequest?.Measurements is null
        || messengerRequest.Measurements.Length == 0)
    {
      return;
    }

    foreach (var item in messengerRequest.Measurements)
    {
      var measurement = _pushRequestMeasurementConverter.ToMeasurement(
        item.Data, item.MeterId, item.Timestamp
      );
      _measurementMutations.Create(measurement);
    }

    await _measurementMutations.SaveChangesAsync();
  }

  public Task OnPoll(string id, string request)
  {
    return Task.CompletedTask;
  }
}
