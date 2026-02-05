namespace Ozds.Data.Entities.Abstractions;

public interface IMeasurementLocationEntity
  : ITrackableEntity,
    IIdentifiableEntity
{
  public string MeterId { get; }
}
