using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffMultiplyByScalarTest
{
  public static
    IEnumerable<(TariffMeasure<decimal>, decimal, TariffMeasure<decimal>)>
    TariffMeasuresMultiply()
  {
    return new List<(TariffMeasure<decimal>, decimal, TariffMeasure<decimal>)>
    {
      (new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3))),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(10),
            new SinglePhasicSumMeasure<decimal>(6)))
      ),

      (new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6))),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(2, 4, 6),
            new TriPhasicMeasure<decimal>(8, 10, 12)))
      ),

      (new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1),
            new SinglePhasicSumMeasure<decimal>(2)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(4))),
        2,
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(4)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(6),
            new SinglePhasicSumMeasure<decimal>(8)))
      ),

      (new NullTariffMeasure<decimal>(), 2, new NullTariffMeasure<decimal>())
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresMultiply))]
  public void Multiply_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    decimal multiplier,
    TariffMeasure<decimal> expected)
  {
    var result = measure.Multiply(multiplier);

    result.Should().Be(expected);
  }
}
