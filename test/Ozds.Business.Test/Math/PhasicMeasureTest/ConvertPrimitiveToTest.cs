using System.Numerics;
using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class ConvertPrimitiveToTest
{
  public static readonly
    TheoryData<PhasicMeasure<decimal>, PhasicMeasure<float>>
    DecimalToFloatConversion = new()
    {
      {
        new SinglePhasicMeasure<decimal>(5.4m),
        new SinglePhasicMeasure<float>(5.4f)
      },
      {
        new TriPhasicMeasure<decimal>(3.3m, 2.2m, 1.1m),
        new TriPhasicMeasure<float>(3.3f, 2.2f, 1.1f)
      },
      {
        new CompositePhasicMeasure<decimal>([
          new SinglePhasicMeasure<decimal>(4.4m),
          new SinglePhasicMeasure<decimal>(5.5m)
        ]),
        new CompositePhasicMeasure<float>([
          new SinglePhasicMeasure<float>(4.4f),
          new SinglePhasicMeasure<float>(5.5f)
        ])
      },
      {
        new NullPhasicMeasure<decimal>(),
        new NullPhasicMeasure<float>()
      }
    };

  public static readonly
    TheoryData<PhasicMeasure<float>, PhasicMeasure<decimal>>
    FloatToDecimalConversion = new()
    {
      {
        new SinglePhasicMeasure<float>(5.4f),
        new SinglePhasicMeasure<decimal>(5.4m)
      },
      {
        new TriPhasicMeasure<float>(3.3f, 2.2f, 1.1f),
        new TriPhasicMeasure<decimal>(3.3m, 2.2m, 1.1m)
      },
      {
        new CompositePhasicMeasure<float>([
          new SinglePhasicMeasure<float>(4.4f),
          new SinglePhasicMeasure<float>(5.5f)
        ]),
        new CompositePhasicMeasure<decimal>([
          new SinglePhasicMeasure<decimal>(4.4m),
          new SinglePhasicMeasure<decimal>(5.5m)
        ])
      },
      {
        new NullPhasicMeasure<float>(),
        new NullPhasicMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(DecimalToFloatConversion))]
  public void ConvertsDecimalToFloatCorrectly(PhasicMeasure<decimal> input,
    PhasicMeasure<float> expected)
  {
    var result = input.ConvertPrimitiveTo<float>();
    AssertPhasicMeasureEqual(expected, result);
  }

  [Theory]
  [MemberData(nameof(FloatToDecimalConversion))]
  public void ConvertsFloatToDecimalCorrectly(PhasicMeasure<float> input,
    PhasicMeasure<decimal> expected)
  {
    var result = input.ConvertPrimitiveTo<decimal>();
    AssertPhasicMeasureEqual(expected, result);
  }

  private void AssertPhasicMeasureEqual<U>(PhasicMeasure<U> expected,
    PhasicMeasure<U> actual)
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
      case (CompositePhasicMeasure<U> expComp, CompositePhasicMeasure<U> actComp
        ):
        Assert.Equal(expComp.Measures.Count, actComp.Measures.Count);
        for (var i = 0; i < expComp.Measures.Count; i++)
        {
          AssertPhasicMeasureEqual(expComp.Measures[i], actComp.Measures[i]);
        }

        break;
      case (NullPhasicMeasure<U>, NullPhasicMeasure<U>):
        break;
      default:
        Assert.Fail(
          "PhasicMeasure types do not match or are not supported in test.");
        break;
    }
  }
}
