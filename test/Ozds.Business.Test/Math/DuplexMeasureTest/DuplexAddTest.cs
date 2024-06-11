using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAddTest
{
  public static readonly TheoryData<DuplexMeasure<decimal>, DuplexMeasure<decimal>, DuplexMeasure<decimal>> DuplexMeasuresAdd = new()
  {
    { new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(5), new SinglePhasicMeasure<decimal>(3)),
      new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(2), new SinglePhasicMeasure<decimal>(1)),
      new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(7), new SinglePhasicMeasure<decimal>(4)) },

    { new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3), new TriPhasicMeasure<decimal>(4, 5, 6)),
      new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(2, 3, 4), new TriPhasicMeasure<decimal>(1, 1, 1)),
      new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(3, 5, 7), new TriPhasicMeasure<decimal>(5, 6, 7)) },

    { new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(4)),
      new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(6)),
      new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(10)) },

    { new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(3)),
      new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(4)),
      new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(7)) },

    { new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(DuplexMeasuresAdd))]
  public void AddReturnsExpectedResult(DuplexMeasure<decimal> lhs, DuplexMeasure<decimal> rhs, DuplexMeasure<decimal> expected)
  {
    var result = lhs.Add(rhs);

    Assert.Equal(expected, result);
  }
}
