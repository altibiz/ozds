using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Pushing.Abstractions;

public interface IMeasurementPusher
{
  public Task Push(IEnumerable<IMeasurement> measurements);
}
