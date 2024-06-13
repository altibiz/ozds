using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffAddTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, TariffMeasure<decimal>,
      TariffMeasure<decimal>> TariffMeasuresAdd = new()
    {
      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(2),
          new SinglePhasicMeasure<decimal>(1))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(7),
          new SinglePhasicMeasure<decimal>(4)))
      },

      {
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 3, 4),
          new TriPhasicMeasure<decimal>(1, 1, 1))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(3, 5, 7),
          new TriPhasicMeasure<decimal>(5, 6, 7)))
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
            new SinglePhasicMeasure<decimal>(3),
            new SinglePhasicMeasure<decimal>(5)),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(7),
            new SinglePhasicMeasure<decimal>(9)))
      },

      {
        new NullTariffMeasure<decimal>(), new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresAdd))]
  public void Add_ReturnsExpectedResult(TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs, TariffMeasure<decimal> expected)
  {
    var result = lhs.Add(rhs);

    Assert.Equal(expected, result);
  }
}
