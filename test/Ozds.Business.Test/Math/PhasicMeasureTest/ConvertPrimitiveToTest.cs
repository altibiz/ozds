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
        new SinglePhasicMeasureSum<decimal>(5.4m),
        new SinglePhasicMeasureSum<float>(5.4f)
      },
      {
        new TriPhasicMeasure<decimal>(3.3m, 2.2m, 1.1m),
        new TriPhasicMeasure<float>(3.3f, 2.2f, 1.1f)
      },
      {
        new CompositePhasicMeasure<decimal>(
        [
          new SinglePhasicMeasureSum<decimal>(4.4m),
          new SinglePhasicMeasureSum<decimal>(5.5m)
        ]),
        new CompositePhasicMeasure<float>(
        [
          new SinglePhasicMeasureSum<float>(4.4f),
          new SinglePhasicMeasureSum<float>(5.5f)
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
        new SinglePhasicMeasureSum<float>(5.4f),
        new SinglePhasicMeasureSum<decimal>(5.4m)
      },
      {
        new TriPhasicMeasure<float>(3.3f, 2.2f, 1.1f),
        new TriPhasicMeasure<decimal>(3.3m, 2.2m, 1.1m)
      },
      {
        new CompositePhasicMeasure<float>(
        [
          new SinglePhasicMeasureSum<float>(4.4f),
          new SinglePhasicMeasureSum<float>(5.5f)
        ]),
        new CompositePhasicMeasure<decimal>(
        [
          new SinglePhasicMeasureSum<decimal>(4.4m),
          new SinglePhasicMeasureSum<decimal>(5.5m)
        ])
      },
      {
        new NullPhasicMeasure<float>(),
        new NullPhasicMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(DecimalToFloatConversion))]
  public void ConvertsDecimalToFloatCorrectly(
    PhasicMeasure<decimal> input,
    PhasicMeasure<float> expected)
  {
    var result = input.ConvertPrimitiveTo<float>();
    AssertPhasicMeasureEqual(expected, result);
  }

  [Theory]
  [MemberData(nameof(FloatToDecimalConversion))]
  public void ConvertsFloatToDecimalCorrectly(
    PhasicMeasure<float> input,
    PhasicMeasure<decimal> expected)
  {
    var result = input.ConvertPrimitiveTo<decimal>();
    AssertPhasicMeasureEqual(expected, result);
  }

  private void AssertPhasicMeasureEqual<T>(
    PhasicMeasure<T> expected,
    PhasicMeasure<T> actual)
    where T : struct,
    IComparisonOperators<T, T, bool>,
    IAdditionOperators<T, T, T>,
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, T, T>,
    IDivisionOperators<T, T, T>
  {
    switch (expected, actual)
    {
      case (SinglePhasicMeasureSum<T> expSingle, SinglePhasicMeasureSum<T> actSingle):
        Assert.Equal(expSingle.Value, actSingle.Value);
        break;
      case (TriPhasicMeasure<T> expTri, TriPhasicMeasure<T> actTri):
        Assert.Equal(expTri.ValueL1, actTri.ValueL1);
        Assert.Equal(expTri.ValueL2, actTri.ValueL2);
        Assert.Equal(expTri.ValueL3, actTri.ValueL3);
        break;
      case (CompositePhasicMeasure<T> expComp, CompositePhasicMeasure<T> actComp
        ):
        Assert.Equal(expComp.Measures.Count, actComp.Measures.Count);
        for (var i = 0; i < expComp.Measures.Count; i++)
        {
          AssertPhasicMeasureEqual(expComp.Measures[i], actComp.Measures[i]);
        }

        break;
      case (NullPhasicMeasure<T>, NullPhasicMeasure<T>):
        break;
      default:
        Assert.Fail(
          "PhasicMeasure types do not match or are not supported in test.");
        break;
    }
  }
}
