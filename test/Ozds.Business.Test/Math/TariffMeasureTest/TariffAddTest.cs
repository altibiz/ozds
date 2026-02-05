using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffAddTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    TariffMeasure<decimal>,
    TariffMeasure<decimal>
  )> TariffMeasuresAdd()
  {
    return new List<(
      TariffMeasure<decimal>,
      TariffMeasure<decimal>,
      TariffMeasure<decimal>
    )>
    {
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(1)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(7),
            new SinglePhasicSumMeasure<decimal>(4)
          )
        )
      ),
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(2, 3, 4),
            new TriPhasicMeasure<decimal>(1, 1, 1)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(3, 5, 7),
            new TriPhasicMeasure<decimal>(5, 6, 7)
          )
        )
      ),
      (
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1),
            new SinglePhasicSumMeasure<decimal>(2)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(4)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(3)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(4),
            new SinglePhasicSumMeasure<decimal>(5)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(5)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(7),
            new SinglePhasicSumMeasure<decimal>(9)
          )
        )
      ),
      (
        new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>()
      ),
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresAdd))]
  public void Add_ReturnsExpectedResult(
    TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs,
    TariffMeasure<decimal> expected
  )
  {
    var result = lhs.Add(rhs);

    result.Should().Be(expected);
  }
}
