namespace Ozds.Data.Entities.Abstractions;

public interface IMeasurementLocationEntity : IAuditableEntity
{
  public string MeterId { get; }
}
