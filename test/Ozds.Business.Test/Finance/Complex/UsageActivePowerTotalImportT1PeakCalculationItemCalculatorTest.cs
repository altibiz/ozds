using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class UsageActivePowerTotalImportT1PeakCalculationItemCalculatorTest
{
  public static readonly
    TheoryData<
      UsageActivePowerTotalImportT1PeakCalculationItemModel>
    TestData = new(
      new Faker<UsageActivePowerTotalImportT1PeakCalculationItemModel>()
        .RuleFor(
          x => x.Peak_kW,
          (f, m) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.Amount_kW,
          (_, m) => System.Math.Round(
            m.Peak_kW,
            0))
        .RuleFor(
          x => x.Price_EUR,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            3))
        .RuleFor(
          x => x.Total_EUR,
          (_, m) => System.Math.Round(
            m.Amount_kW * m.Price_EUR,
            2))
        .GenerateLazy(Constants.DefaultFuzzCount)
    );

  [Theory]
  [MemberData(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xAggregates(
    UsageActivePowerTotalImportT1PeakCalculationItemModel expected)
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
          x => x.ActiveEnergyTotalImportT1Min_Wh,
          (f, _) => f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .RuleFor(
          x => x.ActiveEnergyTotalImportT1Max_Wh,
          (f, m) => f.Random.Decimal(
            m.ActiveEnergyTotalImportT1Min_Wh,
            m.ActiveEnergyTotalImportT1Min_Wh
            + expected.Peak_kW * 1000M
            * (decimal)IntervalModel.QuarterHour.ToTimeSpan(start).TotalHours
            - 0.01M))
        .Generate(Constants.DefaultFuzzCount);
    var peakAggregate =
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
          x => x.ActiveEnergyTotalImportT1Min_Wh,
          (f, _) => f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .RuleFor(
          x => x.ActiveEnergyTotalImportT1Max_Wh,
          (_, m) => m.ActiveEnergyTotalImportT1Min_Wh
            + expected.Peak_kW * 1000M
            * (decimal)IntervalModel.QuarterHour.ToTimeSpan(start).TotalHours)
        .Generate();

    var aggregates = noiseAggregates
      .SkipLast(1)
      .Append(peakAggregate)
      .OrderBy(_ => Random.Shared.Next())
      .Append(noiseAggregates.Last())
      .ToList<IAggregate>();

    var input = new CalculationItemBasisModel(
      aggregates,
      expected.Price_EUR
    );

    var calculator =
      new UsageActivePowerTotalImportT1PeakCalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<UsageActivePowerTotalImportT1PeakCalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
