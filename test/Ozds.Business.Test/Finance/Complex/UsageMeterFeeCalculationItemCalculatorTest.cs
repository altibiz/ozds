using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class UsageMeterFeeCalculationItemCalculatorTest
{
  public static IEnumerable<
    UsageMeterFeeCalculationItemModel> TestData()
  {
    return new Faker<UsageMeterFeeCalculationItemModel>()
      .RuleFor(
        x => x.Amount_N,
        (_, m) => 1)
      .RuleFor(
        x => x.Price_EUR,
        (f, _) => System.Math.Round(
          f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue),
          3))
      .RuleFor(
        x => x.Total_EUR,
        (_, m) => System.Math.Round(
          m.Amount_N * m.Price_EUR,
          2))
      .GenerateLazy(Constants.DefaultFuzzCount);
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xAggregates(
    UsageMeterFeeCalculationItemModel expected)
  {
    var start = Constants.DefaultDateTimeOffset;
    var end = start.AddMonths(1);

    var noiseAggregates =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Interval,
          (f, _) => IntervalModel.QuarterHour)
        .RuleFor(
          x => x.Timestamp,
          (f, _) => f.Date.BetweenOffset(
            start.Add(TimeSpan.FromMinutes(15)),
            end.Subtract(TimeSpan.FromMinutes(15))))
        .Generate(Constants.DefaultFuzzCount);

    var aggregates = noiseAggregates
      .OrderBy(_ => Random.Shared.Next())
      .ToList<AggregateModel>();

    var input = new CalculationItemBasisModel
    {
      Aggregates = aggregates,
      Price_EUR = expected.Price_EUR
    };

    var calculator =
      new UsageMeterFeeCalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<UsageMeterFeeCalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
