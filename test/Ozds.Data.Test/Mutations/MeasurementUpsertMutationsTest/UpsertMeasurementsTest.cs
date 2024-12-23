using System.Diagnostics;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Mutations;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Mutations.MeasurementUpsertMutationsTest;

// TODO: check upsert logic

public class UpsertMeasurementsTest(IServiceProvider services)
{
  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public async Task FinishesInTimeTest(int _)
  {
    await using var manager = services
      .GetRequiredService<EphemeralDataDbContextManager>();
    var context = await manager.GetContext();

    var factory = new MeasurementUpsertFactory(context);
    var expected = await factory.CreateMany(CancellationToken.None);

    var mutations = services.GetRequiredService<MeasurementUpsertMutations>();
    var stopwatch = Stopwatch.StartNew();
    await mutations.UpsertMeasurements(
      context,
      expected,
      CancellationToken.None
    );
    stopwatch.Stop();
    stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(10));
  }

  [Theory]
  [InlineData(1)]
  [InlineData(2)]
  [InlineData(3)]
  public async Task IsValidTest(int _)
  {
    await using var manager = services
      .GetRequiredService<EphemeralDataDbContextManager>();
    var context = await manager.GetContext();

    var factory = new MeasurementUpsertFactory(context);
    var measurements = await factory.CreateDerivedNull(CancellationToken.None);

    var mutations = services.GetRequiredService<MeasurementUpsertMutations>();
    var byproduct = (await mutations
      .UpsertMeasurements(
        context,
        measurements,
        CancellationToken.None))
      .OrderBy(x => x is IAggregateEntity aggregate
        ? aggregate.Interval
        : (IntervalEntity?)null)
      .ThenBy(x => (
        x.GetType().Name,
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x is IAggregateEntity aggregate
          ? aggregate.Interval
          : (IntervalEntity?)null
      ))
      .ToList();

    var actual = (await context.AbbB2xAggregates
        .ToListAsync(CancellationToken.None))
      .OfType<IMeasurementEntity>()
      .Concat(await context.AbbB2xMeasurements
        .ToListAsync(CancellationToken.None))
      .Concat(await context.SchneideriEM3xxxAggregates
        .ToListAsync(CancellationToken.None))
      .Concat(await context.SchneideriEM3xxxMeasurements
        .ToListAsync(CancellationToken.None))
      .OrderBy(x => x is IAggregateEntity aggregate
        ? aggregate.Interval
        : (IntervalEntity?)null)
      .ThenBy(x => (
        x.GetType().Name,
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x is IAggregateEntity aggregate
          ? aggregate.Interval
          : (IntervalEntity?)null
      ))
      .ToList();

    byproduct.Should().BeContextuallyEquivalentTo(context, actual);

    measurements.Should().NotBeContextuallyEquivalentTo(context, actual);

    var expected = measurements
      .GroupBy(x => (
        x.GetType(),
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x is IAggregateEntity aggregate
          ? aggregate.Interval
          : (IntervalEntity?)null
      ))
      .Select(x =>
        x.Key.Item4 is { }
        ? x.Aggregate((lhs, rhs) => (lhs, rhs) switch
          {
            (AbbB2xAggregateEntity lhsAggregate,
              AbbB2xAggregateEntity rhsAggregate) => Upserts
                .Upsert(lhsAggregate, rhsAggregate),
            (SchneideriEM3xxxAggregateEntity lhsAggregate,
              SchneideriEM3xxxAggregateEntity rhsAggregate) => Upserts
                .Upsert(lhsAggregate, rhsAggregate),
            _ => lhs
          })
        : x.First())
      .ToList();
    expected = expected.Select(item =>
      item is IAggregateEntity aggregateItem
        && aggregateItem.Interval != IntervalEntity.QuarterHour
      ? aggregateItem switch
      {
        AbbB2xAggregateEntity abbB2XAggregateItem =>
          expected
            .OfType<AbbB2xAggregateEntity>()
            .Where(x => x.Interval == IntervalEntity.QuarterHour)
            .Where(x => x.MeterId == abbB2XAggregateItem.MeterId)
            .Where(x => x.MeasurementLocationId == abbB2XAggregateItem.MeasurementLocationId)
            .Where(x => x.Timestamp >= abbB2XAggregateItem.Timestamp
              && x.Timestamp < abbB2XAggregateItem.Timestamp
                .Add(abbB2XAggregateItem.Interval
                  .ToModel()
                  .ToTimeSpan(abbB2XAggregateItem.Timestamp)))
            .Aggregate(abbB2XAggregateItem, Upserts.Upsert),
        SchneideriEM3xxxAggregateEntity schneideriEM3xxxAggregateItem =>
          expected
            .OfType<SchneideriEM3xxxAggregateEntity>()
            .Where(x => x.Interval == IntervalEntity.QuarterHour)
            .Where(x => x.MeterId == schneideriEM3xxxAggregateItem.MeterId)
            .Where(x => x.MeasurementLocationId == schneideriEM3xxxAggregateItem.MeasurementLocationId)
            .Where(x => x.Timestamp >= schneideriEM3xxxAggregateItem.Timestamp
              && x.Timestamp < schneideriEM3xxxAggregateItem.Timestamp
                .Add(schneideriEM3xxxAggregateItem.Interval
                  .ToModel()
                  .ToTimeSpan(schneideriEM3xxxAggregateItem.Timestamp)))
            .Aggregate(schneideriEM3xxxAggregateItem, Upserts.Upsert),
        _ => item
      }
      : item)
      .OrderBy(x => x is IAggregateEntity aggregate
        ? aggregate.Interval
        : (IntervalEntity?)null)
      .ThenBy(x => (
        x.GetType().Name,
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x is IAggregateEntity aggregate
          ? aggregate.Interval
          : (IntervalEntity?)null
      ))
      .ToList();

    actual.Should().BeContextuallyEquivalentTo(context, expected);
  }

  private static class Upserts
  {
    public static AbbB2xAggregateEntity Upsert(
      AbbB2xAggregateEntity lhs,
      AbbB2xAggregateEntity rhs
    )
    {
      var interval = lhs.Interval;
      var timestamp = lhs.Timestamp;
      var meterId = lhs.MeterId;
      var measurementLocationId = lhs.MeasurementLocationId;

      var result = new AbbB2xAggregateEntity();

      if (lhs.Interval == rhs.Interval)
      {
        result.Interval = interval;
        result.Timestamp = timestamp;
        result.MeterId = meterId;
        result.MeasurementLocationId = measurementLocationId;
        result.Count = lhs.Count + rhs.Count;
        result.VoltageL1AnyT0_V = Upsert(
          lhs.VoltageL1AnyT0_V,
          lhs.Count,
          rhs.VoltageL1AnyT0_V,
          rhs.Count);
        result.VoltageL2AnyT0_V = Upsert(
          lhs.VoltageL2AnyT0_V,
          lhs.Count,
          rhs.VoltageL2AnyT0_V,
          rhs.Count);
        result.VoltageL3AnyT0_V = Upsert(
          lhs.VoltageL3AnyT0_V,
          lhs.Count,
          rhs.VoltageL3AnyT0_V,
          rhs.Count);
        result.CurrentL1AnyT0_A = Upsert(
          lhs.CurrentL1AnyT0_A,
          lhs.Count,
          rhs.CurrentL1AnyT0_A,
          rhs.Count);
        result.CurrentL2AnyT0_A = Upsert(
          lhs.CurrentL2AnyT0_A,
          lhs.Count,
          rhs.CurrentL2AnyT0_A,
          rhs.Count);
        result.CurrentL3AnyT0_A = Upsert(
          lhs.CurrentL3AnyT0_A,
          lhs.Count,
          rhs.CurrentL3AnyT0_A,
          rhs.Count);
        result.ActivePowerL1NetT0_W = Upsert(
          lhs.ActivePowerL1NetT0_W,
          lhs.Count,
          rhs.ActivePowerL1NetT0_W,
          rhs.Count);
        result.ActivePowerL2NetT0_W = Upsert(
          lhs.ActivePowerL2NetT0_W,
          lhs.Count,
          rhs.ActivePowerL2NetT0_W,
          rhs.Count);
        result.ActivePowerL3NetT0_W = Upsert(
          lhs.ActivePowerL3NetT0_W,
          lhs.Count,
          rhs.ActivePowerL3NetT0_W,
          rhs.Count);
        result.ReactivePowerL1NetT0_VAR = Upsert(
          lhs.ReactivePowerL1NetT0_VAR,
          lhs.Count,
          rhs.ReactivePowerL1NetT0_VAR,
          rhs.Count);
        result.ReactivePowerL2NetT0_VAR = Upsert(
          lhs.ReactivePowerL2NetT0_VAR,
          lhs.Count,
          rhs.ReactivePowerL2NetT0_VAR,
          rhs.Count);
        result.ReactivePowerL3NetT0_VAR = Upsert(
          lhs.ReactivePowerL3NetT0_VAR,
          lhs.Count,
          rhs.ReactivePowerL3NetT0_VAR,
          rhs.Count);
        result.ActiveEnergyL1ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh);
        result.ActiveEnergyL2ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh);
        result.ActiveEnergyL3ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh);
        result.ReactiveEnergyL1ImportT0_VARh = Upsert(
          lhs.ReactiveEnergyL1ImportT0_VARh,
          rhs.ReactiveEnergyL1ImportT0_VARh);
        result.ReactiveEnergyL2ImportT0_VARh = Upsert(
          lhs.ReactiveEnergyL2ImportT0_VARh,
          rhs.ReactiveEnergyL2ImportT0_VARh);
        result.ReactiveEnergyL3ImportT0_VARh = Upsert(
          lhs.ReactiveEnergyL3ImportT0_VARh,
          rhs.ReactiveEnergyL3ImportT0_VARh);
        result.ActiveEnergyL1ExportT0_Wh = Upsert(
          lhs.ActiveEnergyL1ExportT0_Wh,
          rhs.ActiveEnergyL1ExportT0_Wh);
        result.ActiveEnergyL2ExportT0_Wh = Upsert(
          lhs.ActiveEnergyL2ExportT0_Wh,
          rhs.ActiveEnergyL2ExportT0_Wh);
        result.ActiveEnergyL3ExportT0_Wh = Upsert(
          lhs.ActiveEnergyL3ExportT0_Wh,
          rhs.ActiveEnergyL3ExportT0_Wh);
        result.ReactiveEnergyL1ExportT0_VARh = Upsert(
          lhs.ReactiveEnergyL1ExportT0_VARh,
          rhs.ReactiveEnergyL1ExportT0_VARh);
        result.ReactiveEnergyL2ExportT0_VARh = Upsert(
          lhs.ReactiveEnergyL2ExportT0_VARh,
          rhs.ReactiveEnergyL2ExportT0_VARh);
        result.ReactiveEnergyL3ExportT0_VARh = Upsert(
          lhs.ReactiveEnergyL3ExportT0_VARh,
          rhs.ReactiveEnergyL3ExportT0_VARh);
        result.ActiveEnergyTotalImportT0_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT0_Wh,
          rhs.ActiveEnergyTotalImportT0_Wh);
        result.ActiveEnergyTotalExportT0_Wh = Upsert(
          lhs.ActiveEnergyTotalExportT0_Wh,
          rhs.ActiveEnergyTotalExportT0_Wh);
        result.ReactiveEnergyTotalImportT0_VARh = Upsert(
          lhs.ReactiveEnergyTotalImportT0_VARh,
          rhs.ReactiveEnergyTotalImportT0_VARh);
        result.ReactiveEnergyTotalExportT0_VARh = Upsert(
          lhs.ReactiveEnergyTotalExportT0_VARh,
          rhs.ReactiveEnergyTotalExportT0_VARh);
        result.ActiveEnergyTotalImportT1_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT1_Wh,
          rhs.ActiveEnergyTotalImportT1_Wh);
        result.ActiveEnergyTotalImportT2_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT2_Wh,
          rhs.ActiveEnergyTotalImportT2_Wh);
      }
      else
      {
        result.Interval = lhs.Interval;
        result.Timestamp = timestamp;
        result.MeterId = lhs.MeterId;
        result.MeasurementLocationId = lhs.MeasurementLocationId;
        result.Count = lhs.Count;
        result.VoltageL1AnyT0_V = lhs.VoltageL1AnyT0_V;
        result.VoltageL2AnyT0_V = lhs.VoltageL2AnyT0_V;
        result.VoltageL3AnyT0_V = lhs.VoltageL3AnyT0_V;
        result.CurrentL1AnyT0_A = lhs.CurrentL1AnyT0_A;
        result.CurrentL2AnyT0_A = lhs.CurrentL2AnyT0_A;
        result.CurrentL3AnyT0_A = lhs.CurrentL3AnyT0_A;
        result.ActivePowerL1NetT0_W = lhs.ActivePowerL1NetT0_W;
        result.ActivePowerL2NetT0_W = lhs.ActivePowerL2NetT0_W;
        result.ActivePowerL3NetT0_W = lhs.ActivePowerL3NetT0_W;
        result.ReactivePowerL1NetT0_VAR = lhs.ReactivePowerL1NetT0_VAR;
        result.ReactivePowerL2NetT0_VAR = lhs.ReactivePowerL2NetT0_VAR;
        result.ReactivePowerL3NetT0_VAR = lhs.ReactivePowerL3NetT0_VAR;
        result.ActiveEnergyL1ImportT0_Wh = lhs.ActiveEnergyL1ImportT0_Wh;
        result.ActiveEnergyL2ImportT0_Wh = lhs.ActiveEnergyL2ImportT0_Wh;
        result.ActiveEnergyL3ImportT0_Wh = lhs.ActiveEnergyL3ImportT0_Wh;
        result.ReactiveEnergyL1ImportT0_VARh = lhs.ReactiveEnergyL1ImportT0_VARh;
        result.ReactiveEnergyL2ImportT0_VARh = lhs.ReactiveEnergyL2ImportT0_VARh;
        result.ReactiveEnergyL3ImportT0_VARh = lhs.ReactiveEnergyL3ImportT0_VARh;
        result.ActiveEnergyL1ExportT0_Wh = lhs.ActiveEnergyL1ExportT0_Wh;
        result.ActiveEnergyL2ExportT0_Wh = lhs.ActiveEnergyL2ExportT0_Wh;
        result.ActiveEnergyL3ExportT0_Wh = lhs.ActiveEnergyL3ExportT0_Wh;
        result.ReactiveEnergyL1ExportT0_VARh = lhs.ReactiveEnergyL1ExportT0_VARh;
        result.ReactiveEnergyL2ExportT0_VARh = lhs.ReactiveEnergyL2ExportT0_VARh;
        result.ReactiveEnergyL3ExportT0_VARh = lhs.ReactiveEnergyL3ExportT0_VARh;
        result.ActiveEnergyTotalImportT0_Wh = lhs.ActiveEnergyTotalImportT0_Wh;
        result.ActiveEnergyTotalExportT0_Wh = lhs.ActiveEnergyTotalExportT0_Wh;
        result.ReactiveEnergyTotalImportT0_VARh = lhs.ReactiveEnergyTotalImportT0_VARh;
        result.ReactiveEnergyTotalExportT0_VARh = lhs.ReactiveEnergyTotalExportT0_VARh;
        result.ActiveEnergyTotalImportT1_Wh = lhs.ActiveEnergyTotalImportT1_Wh;
        result.ActiveEnergyTotalImportT2_Wh = lhs.ActiveEnergyTotalImportT2_Wh;
      }

      if (lhs.Interval == IntervalEntity.QuarterHour)
      {
        result.QuarterHourCount = 1;
        result.DerivedActivePowerL1ImportT0_W = Upsert(
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL2ImportT0_W = Upsert(
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL3ImportT0_W = Upsert(
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh,
          timestamp);
        result.DerivedReactivePowerL1ImportT0_VAR = Upsert(
          lhs.ReactiveEnergyL1ImportT0_VARh,
          rhs.ReactiveEnergyL1ImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerL2ImportT0_VAR = Upsert(
          lhs.ReactiveEnergyL2ImportT0_VARh,
          rhs.ReactiveEnergyL2ImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerL3ImportT0_VAR = Upsert(
          lhs.ReactiveEnergyL3ImportT0_VARh,
          rhs.ReactiveEnergyL3ImportT0_VARh,
          timestamp);
        result.DerivedActivePowerL1ExportT0_W = Upsert(
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL2ExportT0_W = Upsert(
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL3ExportT0_W = Upsert(
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh,
          timestamp);
        result.DerivedReactivePowerL1ExportT0_VAR = Upsert(
          lhs.ReactiveEnergyL1ImportT0_VARh,
          rhs.ReactiveEnergyL1ImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerL2ExportT0_VAR = Upsert(
          lhs.ReactiveEnergyL2ImportT0_VARh,
          rhs.ReactiveEnergyL2ImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerL3ExportT0_VAR = Upsert(
          lhs.ReactiveEnergyL3ImportT0_VARh,
          rhs.ReactiveEnergyL3ImportT0_VARh,
          timestamp);
        result.DerivedActivePowerTotalImportT0_W = Upsert(
          lhs.ActiveEnergyTotalImportT0_Wh,
          rhs.ActiveEnergyTotalImportT0_Wh,
          timestamp);
        result.DerivedActivePowerTotalExportT0_W = Upsert(
          lhs.ActiveEnergyTotalExportT0_Wh,
          rhs.ActiveEnergyTotalExportT0_Wh,
          timestamp);
        result.DerivedReactivePowerTotalImportT0_VAR = Upsert(
          lhs.ReactiveEnergyTotalImportT0_VARh,
          rhs.ReactiveEnergyTotalImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerTotalExportT0_VAR = Upsert(
          lhs.ReactiveEnergyTotalExportT0_VARh,
          rhs.ReactiveEnergyTotalExportT0_VARh,
          timestamp);
        result.DerivedActivePowerTotalImportT1_W = Upsert(
          lhs.ActiveEnergyTotalImportT1_Wh,
          rhs.ActiveEnergyTotalImportT1_Wh,
          timestamp);
        result.DerivedActivePowerTotalImportT2_W = Upsert(
          lhs.ActiveEnergyTotalImportT2_Wh,
          rhs.ActiveEnergyTotalImportT2_Wh,
          timestamp);
      }
      else
      {
        result.QuarterHourCount = lhs.QuarterHourCount + rhs.QuarterHourCount;
        result.DerivedActivePowerL1ImportT0_W = Upsert(
          lhs.DerivedActivePowerL1ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL1ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL2ImportT0_W = Upsert(
          lhs.DerivedActivePowerL2ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL2ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL3ImportT0_W = Upsert(
          lhs.DerivedActivePowerL3ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL3ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL1ImportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL1ImportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL1ImportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL2ImportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL2ImportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL2ImportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL3ImportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL3ImportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL3ImportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL1ExportT0_W = Upsert(
          lhs.DerivedActivePowerL1ExportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL1ExportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL2ExportT0_W = Upsert(
          lhs.DerivedActivePowerL2ExportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL2ExportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL3ExportT0_W = Upsert(
          lhs.DerivedActivePowerL3ExportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL3ExportT0_W,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL1ExportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL1ExportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL1ExportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL2ExportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL2ExportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL2ExportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerL3ExportT0_VAR = Upsert(
          lhs.DerivedReactivePowerL3ExportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerL3ExportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT0_W = Upsert(
          lhs.DerivedActivePowerTotalImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalExportT0_W = Upsert(
          lhs.DerivedActivePowerTotalExportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalExportT0_W,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerTotalImportT0_VAR = Upsert(
          lhs.DerivedReactivePowerTotalImportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalImportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerTotalExportT0_VAR = Upsert(
          lhs.DerivedReactivePowerTotalExportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalExportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT1_W = Upsert(
          lhs.DerivedActivePowerTotalImportT1_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT1_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT2_W = Upsert(
          lhs.DerivedActivePowerTotalImportT2_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT2_W,
          rhs.QuarterHourCount);
      }

      return result;
    }

    public static SchneideriEM3xxxAggregateEntity Upsert(
      SchneideriEM3xxxAggregateEntity lhs,
      SchneideriEM3xxxAggregateEntity rhs
    )
    {
      var interval = lhs.Interval;
      var timestamp = lhs.Timestamp;
      var meterId = lhs.MeterId;
      var measurementLocationId = lhs.MeasurementLocationId;

      var result = new SchneideriEM3xxxAggregateEntity();

      if (lhs.Interval == rhs.Interval)
      {
        result.Interval = interval;
        result.Timestamp = timestamp;
        result.MeterId = meterId;
        result.MeasurementLocationId = measurementLocationId;
        result.Count = lhs.Count + rhs.Count;
        result.VoltageL1AnyT0_V = Upsert(
          lhs.VoltageL1AnyT0_V,
          lhs.Count,
          rhs.VoltageL1AnyT0_V,
          rhs.Count);
        result.VoltageL2AnyT0_V = Upsert(
          lhs.VoltageL2AnyT0_V,
          lhs.Count,
          rhs.VoltageL2AnyT0_V,
          rhs.Count);
        result.VoltageL3AnyT0_V = Upsert(
          lhs.VoltageL3AnyT0_V,
          lhs.Count,
          rhs.VoltageL3AnyT0_V,
          rhs.Count);
        result.CurrentL1AnyT0_A = Upsert(
          lhs.CurrentL1AnyT0_A,
          lhs.Count,
          rhs.CurrentL1AnyT0_A,
          rhs.Count);
        result.CurrentL2AnyT0_A = Upsert(
          lhs.CurrentL2AnyT0_A,
          lhs.Count,
          rhs.CurrentL2AnyT0_A,
          rhs.Count);
        result.CurrentL3AnyT0_A = Upsert(
          lhs.CurrentL3AnyT0_A,
          lhs.Count,
          rhs.CurrentL3AnyT0_A,
          rhs.Count);
        result.ActivePowerL1NetT0_W = Upsert(
          lhs.ActivePowerL1NetT0_W,
          lhs.Count,
          rhs.ActivePowerL1NetT0_W,
          rhs.Count);
        result.ActivePowerL2NetT0_W = Upsert(
          lhs.ActivePowerL2NetT0_W,
          lhs.Count,
          rhs.ActivePowerL2NetT0_W,
          rhs.Count);
        result.ActivePowerL3NetT0_W = Upsert(
          lhs.ActivePowerL3NetT0_W,
          lhs.Count,
          rhs.ActivePowerL3NetT0_W,
          rhs.Count);
        result.ReactivePowerTotalNetT0_VAR = Upsert(
          lhs.ReactivePowerTotalNetT0_VAR,
          lhs.Count,
          rhs.ReactivePowerTotalNetT0_VAR,
          rhs.Count);
        result.ApparentPowerTotalNetT0_VA = Upsert(
          lhs.ApparentPowerTotalNetT0_VA,
          lhs.Count,
          rhs.ApparentPowerTotalNetT0_VA,
          rhs.Count);
        result.ActiveEnergyL1ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh);
        result.ActiveEnergyL2ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh);
        result.ActiveEnergyL3ImportT0_Wh = Upsert(
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh);
        result.ActiveEnergyTotalImportT0_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT0_Wh,
          rhs.ActiveEnergyTotalImportT0_Wh);
        result.ActiveEnergyTotalExportT0_Wh = Upsert(
          lhs.ActiveEnergyTotalExportT0_Wh,
          rhs.ActiveEnergyTotalExportT0_Wh);
        result.ReactiveEnergyTotalImportT0_VARh = Upsert(
          lhs.ReactiveEnergyTotalImportT0_VARh,
          rhs.ReactiveEnergyTotalImportT0_VARh);
        result.ReactiveEnergyTotalExportT0_VARh = Upsert(
          lhs.ReactiveEnergyTotalExportT0_VARh,
          rhs.ReactiveEnergyTotalExportT0_VARh);
        result.ActiveEnergyTotalImportT1_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT1_Wh,
          rhs.ActiveEnergyTotalImportT1_Wh);
        result.ActiveEnergyTotalImportT2_Wh = Upsert(
          lhs.ActiveEnergyTotalImportT2_Wh,
          rhs.ActiveEnergyTotalImportT2_Wh);
      }
      else
      {
        result.Interval = lhs.Interval;
        result.Timestamp = lhs.Timestamp;
        result.MeterId = lhs.MeterId;
        result.MeasurementLocationId = lhs.MeasurementLocationId;
        result.Count = lhs.Count;
        result.VoltageL1AnyT0_V = lhs.VoltageL1AnyT0_V;
        result.VoltageL2AnyT0_V = lhs.VoltageL2AnyT0_V;
        result.VoltageL3AnyT0_V = lhs.VoltageL3AnyT0_V;
        result.CurrentL1AnyT0_A = lhs.CurrentL1AnyT0_A;
        result.CurrentL2AnyT0_A = lhs.CurrentL2AnyT0_A;
        result.CurrentL3AnyT0_A = lhs.CurrentL3AnyT0_A;
        result.ActivePowerL1NetT0_W = lhs.ActivePowerL1NetT0_W;
        result.ActivePowerL2NetT0_W = lhs.ActivePowerL2NetT0_W;
        result.ActivePowerL3NetT0_W = lhs.ActivePowerL3NetT0_W;
        result.ReactivePowerTotalNetT0_VAR = lhs.ReactivePowerTotalNetT0_VAR;
        result.ApparentPowerTotalNetT0_VA = lhs.ApparentPowerTotalNetT0_VA;
        result.ActiveEnergyL1ImportT0_Wh = lhs.ActiveEnergyL1ImportT0_Wh;
        result.ActiveEnergyL2ImportT0_Wh = lhs.ActiveEnergyL2ImportT0_Wh;
        result.ActiveEnergyL3ImportT0_Wh = lhs.ActiveEnergyL3ImportT0_Wh;
        result.ActiveEnergyTotalImportT0_Wh = lhs.ActiveEnergyTotalImportT0_Wh;
        result.ActiveEnergyTotalExportT0_Wh = lhs.ActiveEnergyTotalExportT0_Wh;
        result.ReactiveEnergyTotalImportT0_VARh = lhs.ReactiveEnergyTotalImportT0_VARh;
        result.ReactiveEnergyTotalExportT0_VARh = lhs.ReactiveEnergyTotalExportT0_VARh;
        result.ActiveEnergyTotalImportT1_Wh = lhs.ActiveEnergyTotalImportT1_Wh;
        result.ActiveEnergyTotalImportT2_Wh = lhs.ActiveEnergyTotalImportT2_Wh;
      }

      if (lhs.Interval == IntervalEntity.QuarterHour)
      {
        result.QuarterHourCount = 1;
        result.DerivedActivePowerL1ImportT0_W = Upsert(
          lhs.ActiveEnergyL1ImportT0_Wh,
          rhs.ActiveEnergyL1ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL2ImportT0_W = Upsert(
          lhs.ActiveEnergyL2ImportT0_Wh,
          rhs.ActiveEnergyL2ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerL3ImportT0_W = Upsert(
          lhs.ActiveEnergyL3ImportT0_Wh,
          rhs.ActiveEnergyL3ImportT0_Wh,
          timestamp);
        result.DerivedActivePowerTotalImportT0_W = Upsert(
          lhs.ActiveEnergyTotalImportT0_Wh,
          rhs.ActiveEnergyTotalImportT0_Wh,
          timestamp);
        result.DerivedActivePowerTotalExportT0_W = Upsert(
          lhs.ActiveEnergyTotalExportT0_Wh,
          rhs.ActiveEnergyTotalExportT0_Wh,
          timestamp);
        result.DerivedReactivePowerTotalImportT0_VAR = Upsert(
          lhs.ReactiveEnergyTotalImportT0_VARh,
          rhs.ReactiveEnergyTotalImportT0_VARh,
          timestamp);
        result.DerivedReactivePowerTotalExportT0_VAR = Upsert(
          lhs.ReactiveEnergyTotalExportT0_VARh,
          rhs.ReactiveEnergyTotalExportT0_VARh,
          timestamp);
        result.DerivedActivePowerTotalImportT1_W = Upsert(
          lhs.ActiveEnergyTotalImportT1_Wh,
          rhs.ActiveEnergyTotalImportT1_Wh,
          timestamp);
        result.DerivedActivePowerTotalImportT2_W = Upsert(
          lhs.ActiveEnergyTotalImportT2_Wh,
          rhs.ActiveEnergyTotalImportT2_Wh,
          timestamp);
      }
      else
      {
        result.QuarterHourCount = lhs.QuarterHourCount + rhs.QuarterHourCount;
        result.DerivedActivePowerL1ImportT0_W = Upsert(
          lhs.DerivedActivePowerL1ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL1ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL2ImportT0_W = Upsert(
          lhs.DerivedActivePowerL2ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL2ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerL3ImportT0_W = Upsert(
          lhs.DerivedActivePowerL3ImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerL3ImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT0_W = Upsert(
          lhs.DerivedActivePowerTotalImportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT0_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalExportT0_W = Upsert(
          lhs.DerivedActivePowerTotalExportT0_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalExportT0_W,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerTotalImportT0_VAR = Upsert(
          lhs.DerivedReactivePowerTotalImportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalImportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedReactivePowerTotalExportT0_VAR = Upsert(
          lhs.DerivedReactivePowerTotalExportT0_VAR,
          lhs.QuarterHourCount,
          rhs.DerivedReactivePowerTotalExportT0_VAR,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT1_W = Upsert(
          lhs.DerivedActivePowerTotalImportT1_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT1_W,
          rhs.QuarterHourCount);
        result.DerivedActivePowerTotalImportT2_W = Upsert(
          lhs.DerivedActivePowerTotalImportT2_W,
          lhs.QuarterHourCount,
          rhs.DerivedActivePowerTotalImportT2_W,
          rhs.QuarterHourCount);
      }

      return result;
    }

    public static InstantaneousAggregateMeasureEntity Upsert(
      InstantaneousAggregateMeasureEntity lhs,
      long lhsCount,
      InstantaneousAggregateMeasureEntity rhs,
      long rhsCount
    )
    {
      return new InstantaneousAggregateMeasureEntity
      {
        Avg = (lhs.Avg * lhsCount + rhs.Avg * rhsCount)
          / (lhsCount + rhsCount),
        Min = Math.Min(lhs.Min, rhs.Min),
        MinTimestamp = lhs.Min < rhs.Min
          ? lhs.MinTimestamp
          : rhs.MinTimestamp,
        Max = Math.Max(lhs.Max, rhs.Max),
        MaxTimestamp = lhs.Max > rhs.Max
          ? lhs.MaxTimestamp
          : rhs.MaxTimestamp,
      };
    }

    public static CumulativeAggregateMeasureEntity Upsert(
      CumulativeAggregateMeasureEntity lhs,
      CumulativeAggregateMeasureEntity rhs
    )
    {
      return new CumulativeAggregateMeasureEntity
      {
        Min = Math.Min(lhs.Min, rhs.Min),
        Max = Math.Max(lhs.Max, rhs.Max),
      };
    }

    public static InstantaneousAggregateMeasureEntity Upsert(
      CumulativeAggregateMeasureEntity lhs,
      CumulativeAggregateMeasureEntity rhs,
      DateTimeOffset timestamp
    )
    {
      var value =
        (Math.Max(lhs.Max, rhs.Max) - Math.Min(lhs.Min, rhs.Min)) * 4;
      return new InstantaneousAggregateMeasureEntity
      {
        Avg = value,
        Min = value,
        MinTimestamp = timestamp,
        Max = value,
        MaxTimestamp = timestamp,
      };
    }
  }
}
