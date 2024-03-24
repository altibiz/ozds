namespace Ozds.Business.Models.Abstractions;

public interface IMeasurementValidator
{
  public string MeterId { get; }
}

public interface IMeasurementValidator<T> : IMeasurementValidator
  where T : IMeasurement
{
  public string? Validate(T measurement, string property);
}
