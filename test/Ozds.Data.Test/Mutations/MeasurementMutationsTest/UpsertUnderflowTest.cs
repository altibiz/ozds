using Ozds.Data.Entities;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Mutations;
using Ozds.Data.Test.Base;

namespace Ozds.Data.Test.Mutations.MeasurementMutationsTest;

public class UpsertUnderflowTest : OzdsDataTestBase
{
  // Instantaneous fields: C# float -> PSQL real (4-byte IEEE 754)
  // Smallest positive subnormal: ~1.4e-45
  private const float FloatNearUnderflowSentinel = float.Epsilon;
  private const float FloatZeroSentinel = 0.0f;

  // Derived Avg field: C# double -> PSQL double precision (8-byte IEEE 754)
  // Smallest positive subnormal: ~4.9e-324
  private const double DoubleNearUnderflowSentinel = double.Epsilon;
  private const double DoubleZeroSentinel = 0.0d;

  [Test]
  public async Task
    SchneideriEM3xxxAggregateUpsertWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(SchneideriEM3xxxMeterEntity))
    );

    var initial = CreateSchneideriEM3xxxAggregate(
      infrastructure.Meter.Id,
      infrastructure.MeasurementLocation.Id,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    await mutations.Create(
      new[] { initial },
      cancellationToken
    );

    var conflicting = CreateSchneideriEM3xxxAggregate(
      infrastructure.Meter.Id,
      infrastructure.MeasurementLocation.Id,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 96,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );

    await mutations.Create(
      new[] { conflicting },
      cancellationToken
    );
  }

  [Test]
  public async Task
    AbbB2xAggregateUpsertWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(AbbB2xMeterEntity))
    );

    var initial = CreateAbbB2xAggregate(
      infrastructure.Meter.Id,
      infrastructure.MeasurementLocation.Id,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    await mutations.Create(
      new[] { initial },
      cancellationToken
    );

    var conflicting = CreateAbbB2xAggregate(
      infrastructure.Meter.Id,
      infrastructure.MeasurementLocation.Id,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 96,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );

    await mutations.Create(
      new[] { conflicting },
      cancellationToken
    );
  }

  [Test]
  public async Task
    SchneideriEM3xxxDailyDeriveAverageWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(SchneideriEM3xxxMeterEntity))
    );
    var meterId = infrastructure.Meter.Id;
    var measurementLocationId = infrastructure.MeasurementLocation.Id;

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    var dailySeed = CreateSchneideriEM3xxxAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfDay,
      IntervalEntity.Day,
      count: 96,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );
    dailySeed.QuarterHourCount = 95;

    await mutations.Create(
      new[] { dailySeed },
      cancellationToken
    );

    var qhAggregate = CreateSchneideriEM3xxxAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    await mutations.Create(
      new[] { qhAggregate },
      cancellationToken
    );
  }

  [Test]
  public async Task
    SchneideriEM3xxxMonthlyDeriveAverageWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(SchneideriEM3xxxMeterEntity))
    );
    var meterId = infrastructure.Meter.Id;
    var measurementLocationId = infrastructure.MeasurementLocation.Id;

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    var monthlySeed = CreateSchneideriEM3xxxAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfMonth,
      IntervalEntity.Month,
      count: 2880,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );
    monthlySeed.QuarterHourCount = 2879;

    await mutations.Create(
      new[] { monthlySeed },
      cancellationToken
    );

    var dailySeed = CreateSchneideriEM3xxxAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfDay,
      IntervalEntity.Day,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );
    dailySeed.QuarterHourCount = 0;

    await mutations.Create(
      new[] { dailySeed },
      cancellationToken
    );

    var qhAggregate = CreateSchneideriEM3xxxAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    await mutations.Create(
      new[] { qhAggregate },
      cancellationToken
    );
  }

  [Test]
  public async Task
    AbbB2xDailyDeriveAverageWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(AbbB2xMeterEntity))
    );
    var meterId = infrastructure.Meter.Id;
    var measurementLocationId = infrastructure.MeasurementLocation.Id;

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    var dailySeed = CreateAbbB2xAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfDay,
      IntervalEntity.Day,
      count: 96,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );
    dailySeed.QuarterHourCount = 95;

    await mutations.Create(
      new[] { dailySeed },
      cancellationToken
    );

    var qhAggregate = CreateAbbB2xAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    await mutations.Create(
      new[] { qhAggregate },
      cancellationToken
    );
  }

  [Test]
  public async Task
    AbbB2xMonthlyDeriveAverageWithNearUnderflowValues_Succeeds(
      CancellationToken cancellationToken
    )
  {
    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(AbbB2xMeterEntity))
    );
    var meterId = infrastructure.Meter.Id;
    var measurementLocationId = infrastructure.MeasurementLocation.Id;

    var mutations = ServiceProvider
      .GetRequiredService<MeasurementMutations>();

    var monthlySeed = CreateAbbB2xAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfMonth,
      IntervalEntity.Month,
      count: 2880,
      instantaneousAvgValue: FloatZeroSentinel,
      derivedAvgValue: DoubleZeroSentinel
    );
    monthlySeed.QuarterHourCount = 2879;

    await mutations.Create(
      new[] { monthlySeed },
      cancellationToken
    );

    var dailySeed = CreateAbbB2xAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfDay,
      IntervalEntity.Day,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );
    dailySeed.QuarterHourCount = 0;

    await mutations.Create(
      new[] { dailySeed },
      cancellationToken
    );

    var qhAggregate = CreateAbbB2xAggregate(
      meterId,
      measurementLocationId,
      Fixtures.Constants.NowStartOfQuarterHour,
      IntervalEntity.QuarterHour,
      count: 1,
      instantaneousAvgValue: FloatNearUnderflowSentinel,
      derivedAvgValue: DoubleNearUnderflowSentinel
    );

    await mutations.Create(
      new[] { qhAggregate },
      cancellationToken
    );
  }

  private static SchneideriEM3xxxAggregateEntity
    CreateSchneideriEM3xxxAggregate(
      string meterId,
      string measurementLocationId,
      DateTimeOffset timestamp,
      IntervalEntity interval,
      long count,
      float instantaneousAvgValue,
      double derivedAvgValue
    )
  {
    return new SchneideriEM3xxxAggregateEntity
    {
      MeterId = meterId,
      MeasurementLocationId = measurementLocationId,
      Timestamp = timestamp,
      Interval = interval,
      Count = count,
      QuarterHourCount = interval == IntervalEntity.QuarterHour ? 1 : 0,
      VoltageL1AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      VoltageL2AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      VoltageL3AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL1AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL2AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL3AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL1NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL2NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL3NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ReactivePowerTotalNetT0_VAR = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ApparentPowerTotalNetT0_VA = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActiveEnergyL1ImportT0_Wh = CreateCumulative(),
      ActiveEnergyL2ImportT0_Wh = CreateCumulative(),
      ActiveEnergyL3ImportT0_Wh = CreateCumulative(),
      ActiveEnergyTotalImportT0_Wh = CreateCumulative(),
      ActiveEnergyTotalExportT0_Wh = CreateCumulative(),
      ReactiveEnergyTotalImportT0_VARh = CreateCumulative(),
      ReactiveEnergyTotalExportT0_VARh = CreateCumulative(),
      ActiveEnergyTotalImportT1_Wh = CreateCumulative(),
      ActiveEnergyTotalImportT2_Wh = CreateCumulative(),
      DerivedActivePowerL1ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL2ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL3ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalExportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerTotalImportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerTotalExportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT1_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT2_W = CreateDerived(derivedAvgValue, timestamp),
    };
  }

  private static AbbB2xAggregateEntity CreateAbbB2xAggregate(
    string meterId,
    string measurementLocationId,
    DateTimeOffset timestamp,
    IntervalEntity interval,
    long count,
    float instantaneousAvgValue,
    double derivedAvgValue
  )
  {
    return new AbbB2xAggregateEntity
    {
      MeterId = meterId,
      MeasurementLocationId = measurementLocationId,
      Timestamp = timestamp,
      Interval = interval,
      Count = count,
      QuarterHourCount = interval == IntervalEntity.QuarterHour ? 1 : 0,
      VoltageL1AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      VoltageL2AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      VoltageL3AnyT0_V = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL1AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL2AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      CurrentL3AnyT0_A = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL1NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL2NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActivePowerL3NetT0_W = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ReactivePowerL1NetT0_VAR = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ReactivePowerL2NetT0_VAR = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ReactivePowerL3NetT0_VAR = CreateInstantaneous(instantaneousAvgValue, timestamp),
      ActiveEnergyL1ImportT0_Wh = CreateCumulative(),
      ActiveEnergyL2ImportT0_Wh = CreateCumulative(),
      ActiveEnergyL3ImportT0_Wh = CreateCumulative(),
      ReactiveEnergyL1ImportT0_VARh = CreateCumulative(),
      ReactiveEnergyL2ImportT0_VARh = CreateCumulative(),
      ReactiveEnergyL3ImportT0_VARh = CreateCumulative(),
      ActiveEnergyL1ExportT0_Wh = CreateCumulative(),
      ActiveEnergyL2ExportT0_Wh = CreateCumulative(),
      ActiveEnergyL3ExportT0_Wh = CreateCumulative(),
      ReactiveEnergyL1ExportT0_VARh = CreateCumulative(),
      ReactiveEnergyL2ExportT0_VARh = CreateCumulative(),
      ReactiveEnergyL3ExportT0_VARh = CreateCumulative(),
      ActiveEnergyTotalImportT0_Wh = CreateCumulative(),
      ActiveEnergyTotalExportT0_Wh = CreateCumulative(),
      ReactiveEnergyTotalImportT0_VARh = CreateCumulative(),
      ReactiveEnergyTotalExportT0_VARh = CreateCumulative(),
      ActiveEnergyTotalImportT1_Wh = CreateCumulative(),
      ActiveEnergyTotalImportT2_Wh = CreateCumulative(),
      DerivedActivePowerL1ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL2ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL3ImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL1ImportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL2ImportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL3ImportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL1ExportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL2ExportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerL3ExportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL1ExportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL2ExportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerL3ExportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalExportT0_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerTotalImportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedReactivePowerTotalExportT0_VAR = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT1_W = CreateDerived(derivedAvgValue, timestamp),
      DerivedActivePowerTotalImportT2_W = CreateDerived(derivedAvgValue, timestamp),
    };
  }

  private static InstantaneousAggregateMeasureEntity CreateInstantaneous(
    float avgValue,
    DateTimeOffset timestamp
  )
  {
    return new InstantaneousAggregateMeasureEntity
    {
      Avg = avgValue,
      Min = avgValue,
      MinTimestamp = timestamp,
      Max = avgValue,
      MaxTimestamp = timestamp,
    };
  }

  private static CumulativeAggregateMeasureEntity CreateCumulative()
  {
    return new CumulativeAggregateMeasureEntity
    {
      Min = 100,
      Max = 200,
    };
  }

  private static DerivedAggregateMeasureEntity CreateDerived(
    double avgValue,
    DateTimeOffset timestamp
  )
  {
    return new DerivedAggregateMeasureEntity
    {
      Avg = avgValue,
      Min = 400,
      MinTimestamp = timestamp,
      Max = 400,
      MaxTimestamp = timestamp,
    };
  }
}
