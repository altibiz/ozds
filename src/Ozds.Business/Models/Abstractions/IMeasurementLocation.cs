namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementLocation : ITrackableIdentifiable
{
  public string MeterId { get; }
}
