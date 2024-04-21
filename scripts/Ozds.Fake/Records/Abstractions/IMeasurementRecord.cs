using Ozds.Business.Math;

namespace Ozds.Fake.Records.Abstractions;

public interface IMeasurementRecord
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure<float> ActiveEnergy_Wh { get; }

  public TariffMeasure<float> ReactiveEnergy_VARh { get; }

  public TariffMeasure<float> ApparentEnergy_VAh { get; }
}
