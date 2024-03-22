namespace Ozds.Business.Models.Abstractions;

public abstract record MeterModel(
  string Id,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId
) : IMeter
{
  public abstract IMeterCapabilities Capabilities { get; }

  public abstract IMeterValidator Validator { get; }
}
