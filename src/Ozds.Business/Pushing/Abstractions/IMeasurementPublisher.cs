using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Pushing.Abstractions;

public interface IMeasurementPublisher
{
  public void BeforePublish(
    IReadOnlyList<IMeasurement> measurements,
    IReadOnlyList<IAggregate> aggregates);

  public void AfterPublish(
    IReadOnlyList<IMeasurement> measurements,
    IReadOnlyList<IAggregate> aggregates);
}
