using Ozds.Business.Math;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Records.Base;

public abstract class MeasurementRecord : IMeasurementRecord
{
  public required string MeterId { get; set; }

  public required DateTimeOffset Timestamp { get; set; }

  public abstract TariffMeasure<float> ActiveEnergy_Wh { get; }

  public abstract TariffMeasure<float> ReactiveEnergy_VARh { get; }

  public abstract TariffMeasure<float> ApparentEnergy_VAh { get; }
}
