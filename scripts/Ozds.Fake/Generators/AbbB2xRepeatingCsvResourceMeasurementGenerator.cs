using Ozds.Fake.Generators.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Generators;

public class AbbB2xRepeatingCsvResourceMeasurementGenerator :
  RepeatingCsvResourceMeasurementGenerator<AbbB2xMeasurementRecord>
{
  public AbbB2xRepeatingCsvResourceMeasurementGenerator(
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  protected override string CsvResourceName
  {
    get { return "abb-B2x-measurements.csv"; }
  }

  protected override string MeterIdPrefix
  {
    get { return "abb-B2x"; }
  }
}
