namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementLocation : IAuditable
{
  public string MeterId { get; }
}
