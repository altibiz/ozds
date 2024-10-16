using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class UsageReactiveEnergyTotalRampedT0CalculationItemCalculatorTest
{
  public static readonly
    TheoryData<
      UsageReactiveEnergyTotalRampedT0CalculationItemModel>
    TestData = new(
      new Faker<UsageReactiveEnergyTotalRampedT0CalculationItemModel>()
        .RuleFor(
          x => x.ReactiveImportMin_kVARh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveImportMax_kVARh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(
              m.ReactiveImportMin_kVARh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveImportAmount_kVARh,
          (_, m) => System.Math.Round(
            m.ReactiveImportMax_kVARh - m.ReactiveImportMin_kVARh,
            0))
        .RuleFor(
          x => x.ReactiveExportMin_kVARh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveExportMax_kVARh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(
              m.ReactiveExportMin_kVARh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveExportAmount_kVARh,
          (_, m) => System.Math.Round(
            m.ReactiveExportMax_kVARh - m.ReactiveExportMin_kVARh,
            0))
        .RuleFor(
          x => x.ActiveImportMin_kWh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ActiveImportMax_kWh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(m.ActiveImportMin_kWh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ActiveImportAmount_kWh,
          (_, m) => System.Math.Round(
            m.ActiveImportMax_kWh - m.ActiveImportMin_kWh,
            0))
        .RuleFor(
          x => x.Amount_kVARh,
          (f, m) => System.Math.Round(
            System.Math.Max(
              System.Math.Abs(m.ReactiveImportAmount_kVARh)
              + System.Math.Abs(m.ReactiveExportAmount_kVARh)
              - 0.33M * m.ActiveImportAmount_kWh,
              0),
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
            m.Amount_kVARh * m.Price_EUR,
            2))
        .GenerateLazy(Constants.DefaultFuzzCount)
    );

  [Theory]
  [MemberData(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xAggregates(
    UsageReactiveEnergyTotalRampedT0CalculationItemModel expected)
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
          x => x.ReactiveEnergyTotalImportT0Min_VARh,
          (f, _) => f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Min_VARh,
          (f, _) => f.Random.Decimal(
            Constants.MinEnergyValue, Constants.MaxEnergyValue))
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
          x => x.ReactiveEnergyTotalImportT0Min_VARh,
          (_, _) => expected.ReactiveImportMin_kVARh * 1000M)
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Min_VARh,
          (_, _) => expected.ReactiveExportMin_kVARh * 1000M)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Min_Wh,
          (_, _) => expected.ActiveImportMin_kWh * 1000M)
        .Generate();
    var endAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Timestamp,
          (_, _) => end)
        .RuleFor(
          x => x.ReactiveEnergyTotalImportT0Min_VARh,
          (_, _) => expected.ReactiveImportMax_kVARh * 1000M)
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Min_VARh,
          (_, _) => expected.ReactiveExportMax_kVARh * 1000M)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Min_Wh,
          (_, _) => expected.ActiveImportMax_kWh * 1000M)
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
      new UsageReactiveEnergyTotalRampedT0CalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<UsageReactiveEnergyTotalRampedT0CalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
