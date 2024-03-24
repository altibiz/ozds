namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementValidator : IAuditable
{
  public string? Validate(IMeasurement measurement, string Property);
}
