using Ozds.Data.Entities;
using Ozds.Fake.Generators.Base;
using Ozds.Fake.Loaders;

namespace Ozds.Fake.Generators;

public class AbbB2xRepeatingCsvResourceMeasurementGenerator : RepeatingCsvResourceMeasurementGenerator<AbbB2xMeasurementEntity>
{
  public AbbB2xRepeatingCsvResourceMeasurementGenerator(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  protected override string CsvResourceName => "abb-B2x-measurements.csv";

  protected override string MeterIdPrefix => "abb-B2x";
}
