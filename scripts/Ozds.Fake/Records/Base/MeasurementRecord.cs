using CsvHelper.Configuration.Attributes;
using Ozds.Business.Math;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Records.Base;

public abstract record class MeasurementRecord : IMeasurementRecord
{
  public required string MeterId { get; set; }

  public required DateTimeOffset Timestamp { get; set; }

  [Ignore]
  public abstract TariffMeasure<decimal> ActiveEnergy_Wh { get; }

  [Ignore]
  public abstract TariffMeasure<decimal> ReactiveEnergy_VARh { get; }

  [Ignore]
  public abstract TariffMeasure<decimal> ApparentEnergy_VAh { get; }
}
