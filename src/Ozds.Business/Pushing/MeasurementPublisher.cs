using Enms.Business.Models.Abstractions;
using Enms.Business.Pushing.Abstractions;

namespace Enms.Business.Pushing;

public class MeasurementPublisher(ILogger<MeasurementPublisher> logger)
  : IMeasurementPublisher, IMeasurementSubscriber
{
  public event EventHandler<MeasurementPublishEventArgs>? OnBeforePublish;

  public event EventHandler<MeasurementPublishEventArgs>? OnAfterPublish;

  public void AfterPublish(
    IReadOnlyList<IMeasurement> measurements,
    IReadOnlyList<IAggregate> aggregates)
  {
    try
    {
      OnAfterPublish?.Invoke(
        this,
        new MeasurementPublishEventArgs(measurements, aggregates)
      );
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "OnAfterPublish");
    }
  }

  public void BeforePublish(
    IReadOnlyList<IMeasurement> measurements,
    IReadOnlyList<IAggregate> aggregates)
  {
    try
    {
      OnBeforePublish?.Invoke(
        this,
        new MeasurementPublishEventArgs(measurements, aggregates)
      );
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "OnBeforePublish");
    }
  }
}
