using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffMultiplyByMeasureTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, TariffMeasure<decimal>,
      TariffMeasure<decimal>> TariffMeasuresMultiply = new()
    {
      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(2),
          new SinglePhasicMeasure<decimal>(1))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(10),
          new SinglePhasicMeasure<decimal>(3)))
      },

      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 3, 4),
          new TriPhasicMeasure<decimal>(1, 2, 3))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 6, 12),
          new TriPhasicMeasure<decimal>(4, 10, 18)))
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
            new SinglePhasicMeasure<decimal>(2),
            new SinglePhasicMeasure<decimal>(3)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(4),
            new SinglePhasicMeasure<decimal>(5))),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(2),
            new SinglePhasicMeasure<decimal>(6)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(12),
            new SinglePhasicMeasure<decimal>(20)))
      },

      {
        new NullTariffMeasure<decimal>(), new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresMultiply))]
  public void Multiply_ReturnsExpectedResult(TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs, TariffMeasure<decimal> expected)
  {
    var result = lhs.Multiply(rhs);

    Assert.Equal(expected, result);
  }
}
