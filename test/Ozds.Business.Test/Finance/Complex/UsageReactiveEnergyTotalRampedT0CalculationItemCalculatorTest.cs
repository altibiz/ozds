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
        .RuleFor(x => x.ReactiveEnergyL1ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL1ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalExportT0_VARh, f => measureFakerNoise.Generate())
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

    var measureFakerStartReactiveImport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ReactiveImportMin_kVARh * 1000M);

    var measureFakerStartReactiveExport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ReactiveExportMin_kVARh * 1000M);

    var measureFakerStartActiveImport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ActiveImportMin_kWh * 1000M);

    var startAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Timestamp,
          (_, _) => start)
        .RuleFor(x => x.ReactiveEnergyL1ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalImportT0_VARh, f => measureFakerStartReactiveImport.Generate())
        .RuleFor(x => x.ReactiveEnergyL1ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalExportT0_VARh, f => measureFakerStartReactiveExport.Generate())
        .RuleFor(x => x.ActiveEnergyL1ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT0_Wh, f => measureFakerStartActiveImport.Generate())
        .RuleFor(x => x.ActiveEnergyL1ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalExportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT1_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT2_Wh, f => measureFakerNoise.Generate())
        .Generate();

    var measureFakerEndReactiveImport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ReactiveImportMax_kVARh * 1000M);

    var measureFakerEndReactiveExport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ReactiveExportMax_kVARh * 1000M);

    var measureFakerEndActiveImport = new Faker<CumulativeAggregateMeasureModel>()
      .RuleFor(m => m.Min, (_, _) => expected.ActiveImportMax_kWh * 1000M);

    var endAggregate =
      new Faker<AbbB2xAggregateModel>()
        .RuleFor(
          x => x.Timestamp,
          (_, _) => end)
        .RuleFor(x => x.ReactiveEnergyL1ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ImportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalImportT0_VARh, f => measureFakerEndReactiveImport.Generate())
        .RuleFor(x => x.ReactiveEnergyL1ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL2ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyL3ExportT0_VARh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ReactiveEnergyTotalExportT0_VARh, f => measureFakerEndReactiveExport.Generate())
        .RuleFor(x => x.ActiveEnergyL1ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL2ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyL3ImportT0_Wh, f => measureFakerNoise.Generate())
        .RuleFor(x => x.ActiveEnergyTotalImportT0_Wh, f => measureFakerEndActiveImport.Generate())
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
      new UsageReactiveEnergyTotalRampedT0CalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual.Should()
      .BeOfType<UsageReactiveEnergyTotalRampedT0CalculationItemModel>().And
      .BeEquivalentTo(expected);
  }
}
