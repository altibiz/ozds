using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffDivideByScalarTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, decimal, TariffMeasure<decimal>>
    TariffMeasuresDivide = new()
    {
      {
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(10),
            new SinglePhasicMeasureSum<decimal>(6))),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(5),
            new SinglePhasicMeasureSum<decimal>(3)))
      },

      {
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(8, 6, 4),
            new TriPhasicMeasure<decimal>(2, 1, 0.5m))),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(4, 3, 2),
            new TriPhasicMeasure<decimal>(1, 0.5m, 0.25m)))
      },

      {
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(1),
            new SinglePhasicMeasureSum<decimal>(2)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(3),
            new SinglePhasicMeasureSum<decimal>(4))),
        2,
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(0.5m),
            new SinglePhasicMeasureSum<decimal>(1)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(1.5m),
            new SinglePhasicMeasureSum<decimal>(2)))
      },

      { new NullTariffMeasure<decimal>(), 2, new NullTariffMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresDivide))]
  public void Divide_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    decimal divisor,
    TariffMeasure<decimal> expected)
  {
    var result = measure.Divide(divisor);

    Assert.Equal(expected, result);
  }
}
