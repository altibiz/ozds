namespace Ozds.Business.Models.Abstractions;

public interface IMeter
{
  public string Id { get; }

  public string? NetworkUserMeasurementLocationId { get; }

  public string? LocationMeasurementLocationId { get; }

  public IMeterCapabilities Capabilities { get; }

  public IMeterValidator Validator { get; }
}
