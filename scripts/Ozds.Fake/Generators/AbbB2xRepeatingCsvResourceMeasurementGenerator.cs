using Ozds.Data.Entities;
using Ozds.Fake.Generators.Base;

namespace Ozds.Fake.Generators;

public class AbbB2xRepeatingCsvResourceMeasurementGenerator :
  RepeatingCsvResourceMeasurementGenerator<AbbB2xMeasurementEntity>
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
