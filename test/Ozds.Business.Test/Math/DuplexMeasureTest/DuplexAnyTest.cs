using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAnyTest
{
  public static IEnumerable<(DuplexMeasure<decimal>, PhasicMeasure<decimal>)>
    DuplexMeasuresAny()
  {
    return new List<(DuplexMeasure<decimal>, PhasicMeasure<decimal>)>
    {
      (new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new SinglePhasicMeasureSum<decimal>(4)
      ),
      (new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new SinglePhasicMeasureSum<decimal>(3)
      ),
      (new AnyDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3)),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      ),
      (new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>())
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresAny))]
  public void DuplexAnyReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexAny();

    result.Should().Be(expected);
  }
}
