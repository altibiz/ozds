using Ozds.Business.Buffers;
using Ozds.Business.Caching;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class IotPushRelay(
  IServiceProvider serviceProvider,
  IPushSubscriber subscriber
) : Relay<PushEventArgs, IotPushEventArgs, IotPushPipe>(
  serviceProvider
), IIotPushSubscriber
{
  protected override void SubscribeIn(
    EventHandler<PushEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<PushEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class IotPushPipe(
  MeasurementLocationCache Cache,
  AgnosticPushRequestMeasurementConverter PushRequestConverter
) : IPipe<PushEventArgs, IotPushEventArgs>
{
  public async Task<IotPushEventArgs> Transform(
    PushEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var bufferBehavior = eventArgs.BufferBehavior switch
    {
      PushEventBufferBehavior.Realtime =>
        MeasurementBufferBehavior.Realtime,
      PushEventBufferBehavior.Buffer =>
        MeasurementBufferBehavior.Buffer,
      _ => throw new ArgumentOutOfRangeException(nameof(eventArgs))
    };

    var measurementLocations = await Cache.GetAsync(
      eventArgs.Request.Measurements
        .Select(x => x.MeterId)
        .Distinct(),
      CancellationToken.None);

    var modelMeasurements = new List<IMeasurement>();
    foreach (var meterPushRequest in eventArgs.Request.Measurements)
    {
      if (measurementLocations.FirstOrDefault(measurementLocation =>
        measurementLocation.MeterId == meterPushRequest.MeterId)
        is { } measurementLocation)
      {
        modelMeasurements.Add(PushRequestConverter
          .ToMeasurement(meterPushRequest, measurementLocation.Id));
      }
    }

    var modelEventArgs = new IotPushEventArgs
    {
      BufferBehavior = bufferBehavior,
      Measurements = modelMeasurements,
      MessengerId = eventArgs.MessengerId
    };

    return modelEventArgs;
  }
}
