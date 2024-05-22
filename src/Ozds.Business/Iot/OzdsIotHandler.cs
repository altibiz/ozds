using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Ozds.Business.Aggregation;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Iot;

public class OzdsIotHandler(
  AgnosticPushRequestMeasurementConverter pushRequestMeasurementConverter,
  BatchAggregatedMeasurementUpserter batchAggregatedMeasurementUpserter,
  ILogger<OzdsIotHandler> logger
)
{
  private static readonly JsonSerializerOptions deserializationOptions =
    new()
    {
      PropertyNameCaseInsensitive = true
    };

  public Task<bool> Authorize(string id, string request)
  {
    return Task.FromResult(true);
  }

  public async Task OnPush(string _, string request)
  {
    MessengerPushRequest? messengerRequest = null;
    try
    {
      messengerRequest =
        JsonSerializer.Deserialize<MessengerPushRequest>(
          request, deserializationOptions);
    }
    catch (JsonException ex)
    {
      // TODO: alerts
      logger.LogError(ex, "Failed to deserialize request");
      throw;
    }

    if (messengerRequest?.Measurements is null
        || messengerRequest.Measurements.Length == 0)
    {
      logger.LogWarning("No measurements in request");
      return;
    }

    var measurements = new List<IMeasurement>();

    foreach (var item in messengerRequest.Measurements)
    {
      var measurement = pushRequestMeasurementConverter.ToMeasurement(
        item.Data, item.MeterId, item.Timestamp
      );

      var validationResults = measurement
        .Validate(new ValidationContext(this))
        .ToList();

      if (validationResults.Count is 0)
      {
        measurements.Add(measurement);
      }
      else
      {
        // TODO: alerts
        logger.LogWarning(
          "Measurement validation failed: {errors}",
          string.Join(", ", validationResults.Select(x => x.ErrorMessage))
        );
      }
    }

    await batchAggregatedMeasurementUpserter
      .BatchAggregatedUpsert(measurements);
  }

  public Task OnPoll(string id, string request)
  {
    return Task.CompletedTask;
  }

  public Task OnUpdate(string id, string request)
  {
    return Task.CompletedTask;
  }
}
