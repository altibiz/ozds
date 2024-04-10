using Ozds.Fake.Generators.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Generators;

public class SchneideriEM3xxxRepeatingCsvResourceMeasurementGenerator :
  RepeatingCsvResourceMeasurementGenerator<SchneideriEM3xxxMeasurementRecord>
{
  public SchneideriEM3xxxRepeatingCsvResourceMeasurementGenerator(
    IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  protected override string CsvResourceName
  {
    get { return "schneider-iEM3xxx-measurements.csv"; }
  }

  protected override string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }
}
