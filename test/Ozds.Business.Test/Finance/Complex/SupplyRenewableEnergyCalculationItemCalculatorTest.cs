using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class SupplyRenewableEnergyCalculationItemCalculatorTest
{
  public static readonly
    TheoryData<
      SupplyRenewableEnergyCalculationItemModel>
    TestData = new(
      new Faker<SupplyRenewableEnergyCalculationItemModel>()
        .RuleFor(
          x => x.Min_kWh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.Max_kWh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(m.Min_kWh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.Amount_kWh,
          (_, m) => System.Math.Round(
            m.Max_kWh - m.Min_kWh,
            0))
        .RuleFor(
          x => x.Price_EUR,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            6))
        .RuleFor(
          x => x.Total_EUR,
          (_, m) => System.Math.Round(
            m.Amount_kWh * m.Price_EUR,
            2))
        .GenerateLazy(Constants.DefaultFuzzCount)
    );

  [Theory]
  [MemberData(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xAggregates(
    SupplyRenewableEnergyCalculationItemModel expected)
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
            start.Add(IntervalModel.QuarterHour.ToTimeSpan(start)),
            end.Subtract(IntervalModel.QuarterHour.ToTimeSpan(start))))
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Min_Wh,
          (f, _) => f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .Generate(Constants.DefaultFuzzCount);
    var startAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Timestamp,
          (_, _) => start)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Min_Wh,
          (_, _) => expected.Min_kWh * 1000M)
        .Generate();
    var endAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Timestamp,
          (_, _) => end)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Min_Wh,
          (_, _) => expected.Max_kWh * 1000M)
        .Generate();

    var aggregates = noiseAggregates
      .Append(startAggregate)
      .Append(endAggregate)
      .OrderBy(_ => Random.Shared.Next())
      .ToList<AggregateModel>();

    var input = new CalculationItemBasisModel(
      aggregates,
      expected.Price_EUR
    );

    var calculator =
      new SupplyRenewableEnergyCalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<SupplyRenewableEnergyCalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
