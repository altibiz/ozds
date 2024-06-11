using Xunit;
using Ozds.Business.Math;
using System.Numerics;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class ConvertPrimitiveToTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>, PhasicMeasure<int>> PhasicMeasuresConversion = new()
  {
    {
      new SinglePhasicMeasure<decimal>(5.4m),
      new SinglePhasicMeasure<int>(5)
    },
    {
      new TriPhasicMeasure<decimal>(3.3m, 2.2m, 1.1m),
      new TriPhasicMeasure<int>(3, 2, 1)
    },
    {
      new CompositePhasicMeasure<decimal>(new List<PhasicMeasure<decimal>> {
        new SinglePhasicMeasure<decimal>(4.4m),
        new SinglePhasicMeasure<decimal>(5.5m)
      }),
      new CompositePhasicMeasure<int>(new List<PhasicMeasure<int>> {
        new SinglePhasicMeasure<int>(4),
        new SinglePhasicMeasure<int>(5)
      })
    },
    {
      new NullPhasicMeasure<decimal>(),
      new NullPhasicMeasure<int>()
    }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresConversion))]
  public void ConvertsPhasicMeasureCorrectly(PhasicMeasure<decimal> input, PhasicMeasure<int> expected)
  {
    var result = input.ConvertPrimitiveTo<int>();
    AssertPhasicMeasureEqual(expected, result);
  }

  private void AssertPhasicMeasureEqual<U>(PhasicMeasure<U> expected, PhasicMeasure<U> actual)
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    switch (expected, actual)
    {
      case (SinglePhasicMeasure<U> expSingle, SinglePhasicMeasure<U> actSingle):
        Assert.Equal(expSingle.Value, actSingle.Value);
        break;
      case (TriPhasicMeasure<U> expTri, TriPhasicMeasure<U> actTri):
        Assert.Equal(expTri.ValueL1, actTri.ValueL1);
        Assert.Equal(expTri.ValueL2, actTri.ValueL2);
        Assert.Equal(expTri.ValueL3, actTri.ValueL3);
        break;
      case (CompositePhasicMeasure<U> expComp, CompositePhasicMeasure<U> actComp):
        Assert.Equal(expComp.Measures.Count, actComp.Measures.Count);
        for (int i = 0; i < expComp.Measures.Count; i++)
        {
          AssertPhasicMeasureEqual(expComp.Measures[i], actComp.Measures[i]);
        }
        break;
      case (NullPhasicMeasure<U>, NullPhasicMeasure<U>):
        break;
      default:
        Assert.Fail("PhasicMeasure types do not match or are not supported in test.");
        break;
    }
  }
}
