using Ozds.Business.Models.Abstractions;

namespace Ozds.Fake.Identification;

// TODO: this/calling code should be using naming conventions
// for the meter model stuff

public record struct MeasurementLocationMeterId(
  string MeasurementLocationId,
  string MeterId
)
{
  public readonly string MeterModel
  {
    get { return string.Join('-', MeterId.Split('-').Take(2)); }
  }

  public static MeasurementLocationMeterId FromString(
    string id)
  {
    var parts = id.Split(":");
    return new MeasurementLocationMeterId(parts[0], parts[1]);
  }
}

public static class IMeasurementExtensions
{
  public static string MeterModel(this IMeasurement measurement)
  {
    return string.Join('-', measurement.MeterId.Split('-').Take(2));
  }
}
