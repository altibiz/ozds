using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Records.Base;

public class MeasurementRecord : IMeasurementRecord
{
  public required string MeterId { get; set; }

  public required DateTimeOffset Timestamp { get; set; }
}
