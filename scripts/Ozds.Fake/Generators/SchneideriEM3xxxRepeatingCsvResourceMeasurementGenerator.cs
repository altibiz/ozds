using Ozds.Fake.Generators.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Generators;

public class SchneideriEM3xxxRepeatingCsvResourceMeasurementGenerator(
  IServiceProvider serviceProvider) :
  RepeatingCsvResourceMeasurementGenerator<SchneideriEM3xxxMeasurementRecord>(
    serviceProvider)
{
  protected override string CsvResourceName
  {
    get { return "schneider-iEM3xxx-measurements.csv"; }
  }

  protected override string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }
}
