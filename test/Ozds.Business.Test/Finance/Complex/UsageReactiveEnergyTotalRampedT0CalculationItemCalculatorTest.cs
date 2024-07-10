using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
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
          x => x.ReactiveImportMin_VARh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveImportMax_VARh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(m.ReactiveImportMin_VARh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveImportAmount_VARh,
          (_, m) => System.Math.Round(
            m.ReactiveImportMax_VARh - m.ReactiveImportMin_VARh,
            0))
        .RuleFor(
          x => x.ReactiveExportMin_VARh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveExportMax_VARh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(m.ReactiveExportMin_VARh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ReactiveExportAmount_VARh,
          (_, m) => System.Math.Round(
            m.ReactiveExportMax_VARh - m.ReactiveExportMin_VARh,
            0))
        .RuleFor(
          x => x.ActiveImportMin_Wh,
          (f, _) => System.Math.Round(
            f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ActiveImportMax_Wh,
          (f, m) => System.Math.Round(
            f.Random.Decimal(m.ActiveImportMin_Wh, Constants.MaxEnergyValue),
            2))
        .RuleFor(
          x => x.ActiveImportAmount_Wh,
          (_, m) => System.Math.Round(
            m.ActiveImportMax_Wh - m.ActiveImportMin_Wh,
            0))
        .RuleFor(
          x => x.Amount_VARh,
          (f, m) => System.Math.Round(
            System.Math.Max(
              System.Math.Abs(m.ReactiveImportAmount_VARh)
              + System.Math.Abs(m.ReactiveExportAmount_VARh)
              - 0.33M * m.ActiveImportAmount_Wh,
              0),
            0))
        .RuleFor(
          x => x.Price_EUR,
          (f, _) => System.Math.Round(
            f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue),
            6))
        .RuleFor(
          x => x.Total_EUR,
          (_, m) => System.Math.Round(
            m.Amount_VARh * m.Price_EUR,
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
          x => x.ReactiveEnergyTotalImportT0Max_VARh,
          (f, _) => f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Max_VARh,
          (f, _) => f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Max_Wh,
          (f, _) => f.Random.Decimal(Constants.MinEnergyValue, Constants.MaxEnergyValue))
        .Generate(Constants.DefaultFuzzCount);
    var startAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
            x => x.Timestamp,
            (_, _) => start)
        .RuleFor(
          x => x.ReactiveEnergyTotalImportT0Max_VARh,
          (_, _) => expected.ReactiveImportMin_VARh)
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Max_VARh,
          (_, _) => expected.ReactiveExportMin_VARh)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Max_Wh,
          (_, _) => expected.ActiveImportMin_Wh)
        .Generate();
    var endAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
            x => x.Timestamp,
            (_, _) => end)
        .RuleFor(
          x => x.ReactiveEnergyTotalImportT0Max_VARh,
          (_, _) => expected.ReactiveImportMax_VARh)
        .RuleFor(
          x => x.ReactiveEnergyTotalExportT0Max_VARh,
          (_, _) => expected.ReactiveExportMax_VARh)
        .RuleFor(
          x => x.ActiveEnergyTotalImportT0Max_Wh,
          (_, _) => expected.ActiveImportMax_Wh)
        .Generate();

    var aggregates = noiseAggregates
      .Append(startAggregate)
      .Append(endAggregate)
      .OrderBy(_ => Random.Shared.Next())
      .ToList<IAggregate>();

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
