using Ozds.Business.Math;

namespace Ozds.Fake.Records.Abstractions;

public interface IMeasurementRecord
{
  public string MeterId { get; }

  public string MeasurementLocationId { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  public TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  public TariffMeasure<decimal> ApparentEnergy_VAh { get; }
}
