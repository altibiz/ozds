namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementLocation : ITrackableIdentifiable,
  ICachedIdentifiable
{
  public string MeterId { get; }
}
