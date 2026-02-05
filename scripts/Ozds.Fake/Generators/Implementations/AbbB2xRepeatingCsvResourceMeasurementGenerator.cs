using Ozds.Fake.Generation.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Generation.Implementations;

public class AbbB2xRepeatingCsvResourceMeasurementGenerator(
  IServiceProvider serviceProvider
)
  : RepeatingCsvResourceMeasurementGenerator<AbbB2xMeasurementRecord>(
    serviceProvider
  )
{
  protected override string CsvResourceName
  {
    get { return "abb-B2x-measurements.csv"; }
  }

  protected override string MeterIdPrefix
  {
    get { return "abb-B2x"; }
  }
}
