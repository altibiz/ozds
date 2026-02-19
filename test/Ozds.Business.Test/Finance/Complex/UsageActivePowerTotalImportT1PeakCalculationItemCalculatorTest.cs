using Ozds.Business.Finance.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Test.Finance.Complex;

public class UsageActivePowerTotalImportT1PeakCalculationItemCalculatorTest
{
  public static IEnumerable<UsageActivePowerTotalImportT1PeakCalculationItemModel> TestData()
  {
    return new Faker<UsageActivePowerTotalImportT1PeakCalculationItemModel>()
      .RuleFor(
        x => x.Peak_kW,
        (f, m) =>
          System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue,
              Constants.MaxEnergyValue
            ),
            2
          )
      )
      .RuleFor(x => x.Amount_kW, (_, m) => System.Math.Round(m.Peak_kW, 0))
      .RuleFor(
        x => x.Price_EUR,
        (f, _) =>
          System.Math.Round(
            f.Random.Decimal(
              Constants.MinEnergyValue,
              Constants.MaxEnergyValue
            ),
            3
          )
      )
      .RuleFor(
        x => x.Total_EUR,
        (_, m) => System.Math.Round(m.Amount_kW * m.Price_EUR, 2)
      )
      .GenerateLazy(Constants.DefaultFuzzCount);
  }

  [Test]
  [MethodDataSource(nameof(TestData))]
  public void CalculatesCorrectlyWithFuzzyAbbB2xAggregates(
    UsageActivePowerTotalImportT1PeakCalculationItemModel expected
  )
  {
    var start = Constants.DefaultDateTimeOffset;
    var end = start.AddMonths(1);

    var derivedMeasureFakerNoise =
      new Faker<DerivedAggregateMeasureModel>().RuleFor(
        m => m.Max,
        f => f.Random.Decimal(Constants.MinPowerValue, Constants.MaxPowerValue)
      );

    var measureFakerDud = new Faker<DerivedAggregateMeasureModel>().RuleFor(
      x => x.Max,
      (f, m) => f.Random.Decimal(0, expected.Peak_kW * 1000M - 0.01M)
    );

    var measureFakerPeak = new Faker<DerivedAggregateMeasureModel>().RuleFor(
      x => x.Max,
      (_, m) => expected.Peak_kW * 1000M
    );

    var noiseAggregates = new Faker<AbbB2xAggregateModel>()
      .RuleFor(x => x.Interval, (f, _) => IntervalModel.QuarterHour)
      .RuleFor(
        x => x.Timestamp,
        (f, _) =>
          f.Date.BetweenOffset(
            start.Add(TimeSpan.FromMinutes(15)),
            end.Subtract(TimeSpan.FromMinutes(15))
          )
      )
      .RuleFor(
        x => x.DerivedActivePowerL1ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL2ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL3ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL1ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL2ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL3ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT1_W,
        (_, _) => measureFakerDud.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT2_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .Generate(Constants.DefaultFuzzCount);
    var peakAggregate = new Faker<AbbB2xAggregateModel>()
      .RuleFor(x => x.Interval, (f, _) => IntervalModel.QuarterHour)
      .RuleFor(
        x => x.Timestamp,
        (f, _) =>
          f.Date.BetweenOffset(
            start.Add(TimeSpan.FromMinutes(15)),
            end.Subtract(TimeSpan.FromMinutes(15))
          )
      )
      .RuleFor(
        x => x.DerivedActivePowerL1ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL2ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL3ImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL1ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL2ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerL3ExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalExportT0_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT1_W,
        (_, _) => measureFakerPeak.Generate()
      )
      .RuleFor(
        x => x.DerivedActivePowerTotalImportT2_W,
        f => derivedMeasureFakerNoise.Generate()
      )
      .Generate();

    var aggregates = noiseAggregates
      .SkipLast(1)
      .Append(peakAggregate)
      .OrderBy(_ => Random.Shared.Next())
      .Append(noiseAggregates.Last())
      .ToList<AggregateModel>();

    var input = new CalculationItemBasisModel
    {
      FromDate = aggregates.Select(x => x.Timestamp).Min(),
      ToDate = aggregates.Select(x => x.Timestamp).Max().AddTicks(1),
      Aggregates = aggregates,
      Price_EUR = expected.Price_EUR,
    };

    var calculator =
      new UsageActivePowerTotalImportT1PeakCalculationItemCalculator();
    var actual = calculator.Calculate(input);

    actual
      .Should()
      .BeOfType<UsageActivePowerTotalImportT1PeakCalculationItemModel>()
      .And.BeEquivalentTo(expected);
  }
}
