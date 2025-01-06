using Ozds.Business.Buffers;
using Ozds.Business.Caching;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class IotPushEventObserver(
  IPushSubscriber subscriber,
  AgnosticModelEntityConverter modelEntityConverter,
  MeasurementLocationCache cache,
  AgnosticPushRequestMeasurementConverter pushRequestConverter
) : IIotPushSubscriber
{
  public void Subscribe(EventHandler<IotPushEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter,
      cache,
      pushRequestConverter
    );
    subscriber.Subscribe(modelEventHandler.Handle);
  }

  public void Unsubscribe(EventHandler<IotPushEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter,
      cache,
      pushRequestConverter
    );
    subscriber.Unsubscribe(modelEventHandler.Handle);
  }

  private record struct Handler(
    EventHandler<IotPushEventArgs> EventHandler,
    AgnosticModelEntityConverter ModelEntityConverter,
    MeasurementLocationCache Cache,
    AgnosticPushRequestMeasurementConverter PushRequestConverter
  )
  {
    public readonly void Handle(
      object? sender,
      PushEventArgs eventArgs)
    {
      var handler = this;
      Task.Run(() => handler.HandleAsync(sender, eventArgs));
    }

    private readonly async Task HandleAsync(
      object? sender,
      PushEventArgs eventArgs
    )
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

      EventHandler(sender, modelEventArgs);
    }
  }
}
