using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffUnaryTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, DuplexMeasure<decimal>>
    TariffMeasuresUnary = new()
    {
      {
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(5),
            new SinglePhasicMeasureSum<decimal>(3)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(2),
            new SinglePhasicMeasureSum<decimal>(1))),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(7),
          new SinglePhasicMeasureSum<decimal>(4))
      },

      {
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6))),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6))
      },

      { new NullTariffMeasure<decimal>(), DuplexMeasure<decimal>.Null }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresUnary))]
  public void TariffUnary_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    DuplexMeasure<decimal> expected)
  {
    var result = measure.TariffUnary();

    Assert.Equal(expected, result);
  }
}
