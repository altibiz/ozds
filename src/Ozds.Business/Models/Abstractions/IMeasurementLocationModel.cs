namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementLocation : IAuditable
{
  string MeterId { get; }
}
