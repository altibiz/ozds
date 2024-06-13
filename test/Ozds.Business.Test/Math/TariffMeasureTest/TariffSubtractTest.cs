using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffSubtractTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, TariffMeasure<decimal>,
      TariffMeasure<decimal>> TariffMeasuresSubtract = new()
    {
      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(2),
          new SinglePhasicMeasure<decimal>(1))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(3),
          new SinglePhasicMeasure<decimal>(2)))
      },

      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(10, 9, 8),
          new TriPhasicMeasure<decimal>(5, 4, 3))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 1, 1),
          new TriPhasicMeasure<decimal>(1, 1, 1))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(9, 8, 7),
          new TriPhasicMeasure<decimal>(4, 3, 2)))
      },

      {
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(1),
            new SinglePhasicMeasure<decimal>(2)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(3),
            new SinglePhasicMeasure<decimal>(4))),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(0.5m),
            new SinglePhasicMeasure<decimal>(1)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(1.5m),
            new SinglePhasicMeasure<decimal>(2))),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(0.5m),
            new SinglePhasicMeasure<decimal>(1)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(1.5m),
            new SinglePhasicMeasure<decimal>(2)))
      },

      {
        new NullTariffMeasure<decimal>(), new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresSubtract))]
  public void Subtract_ReturnsExpectedResult(TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs, TariffMeasure<decimal> expected)
  {
    var result = lhs.Subtract(rhs);

    Assert.Equal(expected, result);
  }
}
