using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.ExpenditureMeasureTest;

public class ExpenditureConvertPrimitiveToFloatTest
{
  public static readonly
    TheoryData<ExpenditureMeasure<decimal>, ExpenditureMeasure<float>>
    ExpenditureMeasuresConvertToFloat = new()
    {
      {
        new UsageExpenditureMeasure<decimal>(new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(5.1m),
            new SinglePhasicMeasure<decimal>(3.1m)))),
        new UsageExpenditureMeasure<float>(new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasure<float>(5.1f),
            new SinglePhasicMeasure<float>(3.1f))))
      },

      {
        new SupplyExpenditureMeasure<decimal>(new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1.1m, 2.1m, 3.1m),
            new TriPhasicMeasure<decimal>(4.1m, 5.1m, 6.1m)))),
        new SupplyExpenditureMeasure<float>(new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new TriPhasicMeasure<float>(1.1f, 2.1f, 3.1f),
            new TriPhasicMeasure<float>(4.1f, 5.1f, 6.1f))))
      },

      {
        new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(1.1m),
              new SinglePhasicMeasure<decimal>(2.1m))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(3.1m),
              new SinglePhasicMeasure<decimal>(4.1m)))),
        new DualExpenditureMeasure<float>(
          new UnaryTariffMeasure<float>(new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasure<float>(1.1f),
            new SinglePhasicMeasure<float>(2.1f))),
          new UnaryTariffMeasure<float>(new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasure<float>(3.1f),
            new SinglePhasicMeasure<float>(4.1f))))
      },

      {
        new NullExpenditureMeasure<decimal>(),
        new NullExpenditureMeasure<float>()
      }
    };

  [Theory]
  [MemberData(nameof(ExpenditureMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloat_ReturnsExpectedResult(
    ExpenditureMeasure<decimal> measure, ExpenditureMeasure<float> expected)
  {
    var result = measure.ConvertPrimitiveTo<float>();

    Assert.Equal(expected, result);
  }
}
