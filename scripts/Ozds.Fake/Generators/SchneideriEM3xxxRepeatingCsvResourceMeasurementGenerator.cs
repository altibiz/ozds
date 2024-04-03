using Ozds.Data.Entities;
using Ozds.Fake.Generators.Base;

namespace Ozds.Fake.Generators;

public class SchneideriEM3xxxRepeatingCsvResourceMeasurementGenerator :
  RepeatingCsvResourceMeasurementGenerator<SchneideriEM3xxxMeasurementEntity>
{
  public SchneideriEM3xxxRepeatingCsvResourceMeasurementGenerator(
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
