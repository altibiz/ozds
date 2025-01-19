using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class SupplyBusinessUsageCalculationItemCalculatorTest
{
  public static readonly
    TheoryData<SupplyBusinessUsageCalculationItemModel>
    TestData = new(
      new Faker<SupplyBusinessUsageCalculationItemModel>()
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
    SupplyBusinessUsageCalculationItemModel expected)
  {
    var start = Constants.DefaultDateTimeOffset;
    var end = start.AddMonths(1);

    var measureFakerNoise = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, f => f.Random.Decimal(
        Constants.MinEnergyValue, Constants.MaxEnergyValue));

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
        .RuleFor(x => x.ActiveEnergyL1ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL1ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT1_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT2_Wh, f => measureFakerNoise.Generate())
        .Generate(Constants.DefaultFuzzCount);

    var measureFakerStart = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.Min_kWh * 1000M);

    var startAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(x => x.Timestamp, (_, _) => start)
        .RuleFor(x => x.ActiveEnergyL1ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT0_Wh, f => measureFakerStart.Generate())
        .RuleFor(x => x.ActiveEnergyL1ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT1_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT2_Wh, f => measureFakerNoise.Generate())
        .Generate();

    var measureFakerEnd = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.Max_kWh * 1000M);

    var endAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(x => x.Timestamp, (_, _) => end)
        .RuleFor(x => x.ActiveEnergyL1ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT0_Wh, f => measureFakerEnd.Generate())
        .RuleFor(x => x.ActiveEnergyL1ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT1_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT2_Wh, f => measureFakerNoise.Generate())
        .Generate();

    var aggregates = noiseAggregates
      .Append(startAggregate)
      .Append(endAggregate)
      .OrderBy(_ => Random.Shared.Next())
      .ToList<AggregateModel>();

    var input = new CalculationItemBasisModel {
      Aggregates = aggregates,
      Price_EUR = expected.Price_EUR
    };

    var calculator =
      new SupplyBusinessUsageCalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<SupplyBusinessUsageCalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
