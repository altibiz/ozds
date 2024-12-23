using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Time;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Test.Extensions;
using Ozds.Data.Test.Specimens;

namespace Ozds.Data.Test.Fixtures;

public class MeasurementUpsertFactory(DbContext context)
{
  private const int MeasurementCount =
    1000 / Constants.DefaultDbFuzzCount / 2;
  private const int AggregateCount =
    MeasurementCount / 10;
  private const int MeasurementCountFew =
    MeasurementCount / 4;
  private const int AggregateCountFew =
    MeasurementCountFew / 10;
  private static readonly DateTimeOffset Now = new(
    DateTime.SpecifyKind(
      DateTimeOffset.Parse(
        "2024-10-27T05:15:00Z",
        CultureInfo.InvariantCulture).UtcDateTime,
      DateTimeKind.Utc),
    TimeSpan.Zero);

  private static readonly DateTimeOffset NowStartOfQuarterHour =
    Now.GetStartOfQuarterHour().ToUniversalTime();

  private static readonly DateTimeOffset NowStartOfDay =
    Now.GetStartOfDay().ToUniversalTime();

  private static readonly DateTimeOffset NowStartOfMonth =
    Now.GetStartOfMonth().ToUniversalTime();

  public async Task<List<IMeasurementEntity>> CreateDerivedNull(
    CancellationToken cancellationToken
  )
  {
    var faker = new Faker();
    InstantaneousAggregateMeasureEntity DerivedPowerFactory(int i)
    {
      var interval = IntervalByIndex(i);
      var timestamp = AggregateTimestampByIndex(i);
      var min = faker.Random
        .Float(Constants.MinAggregateValue, Constants.MaxAggregateValue);
      var max = faker.Random
        .Float(min, Constants.MaxAggregateValue);
      var avg = (min + max) / 2;
      return interval == IntervalEntity.QuarterHour
        ? new InstantaneousAggregateMeasureEntity
        {
          Min = min,
          Max = max,
          Avg = avg,
          MinTimestamp = timestamp,
          MaxTimestamp = timestamp
        }
        : new InstantaneousAggregateMeasureEntity
        {
          Min = float.PositiveInfinity,
          Max = float.NegativeInfinity,
          Avg = 0.0f,
          MinTimestamp = timestamp,
          MaxTimestamp = timestamp
        };
    }

    return await Create(
      cancellationToken,
      x => x
        .CreateMany(MeasurementCountFew),
      x => x
        .IndexedWith(x => x.DerivedActivePowerL1ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL2ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL3ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL1ExportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL2ExportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL3ExportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalExportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL1ImportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL2ImportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL3ImportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerTotalImportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL1ExportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL2ExportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerL3ExportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerTotalExportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT1_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT2_W, DerivedPowerFactory)
        .CreateMany(AggregateCountFew),
      x => x
        .CreateMany(MeasurementCountFew),
      x => x
        .IndexedWith(x => x.DerivedActivePowerL1ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL2ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerL3ImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalExportT0_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerTotalImportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedReactivePowerTotalExportT0_VAR, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT1_W, DerivedPowerFactory)
        .IndexedWith(x => x.DerivedActivePowerTotalImportT2_W, DerivedPowerFactory)
        .CreateMany(AggregateCountFew)
    );
  }

  public async Task<List<IMeasurementEntity>> CreateMany(
    CancellationToken cancellationToken
  )
  {
    return await Create(
      cancellationToken,
      x => x.CreateMany(MeasurementCount),
      x => x.CreateMany(AggregateCount),
      x => x.CreateMany(MeasurementCount),
      x => x.CreateMany(AggregateCount)
    );
  }

  private async Task<List<IMeasurementEntity>> Create(
    CancellationToken cancellationToken,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<AbbB2xMeasurementEntity>,
      IEnumerable<AbbB2xMeasurementEntity>>
      abbB2xMeasurementFactory,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<AbbB2xAggregateEntity>,
      IEnumerable<AbbB2xAggregateEntity>>
      abbB2xAggregateFactory,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<SchneideriEM3xxxMeasurementEntity>,
      IEnumerable<SchneideriEM3xxxMeasurementEntity>>
      schneideriEM3xxxMeasurementFactory,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<SchneideriEM3xxxAggregateEntity>,
      IEnumerable<SchneideriEM3xxxAggregateEntity>>
      schneideriEM3xxxAggregateFactory
  )
  {
    var result = new List<IMeasurementEntity>();

    var fixture = context.ContextualFixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(
        Now.AddMonths(-1), Now));

    foreach (var i in Enumerable.Range(1, Constants.DefaultDbFuzzCount))
    {
      var blueLowCatalogue = await fixture
        .CreateInDb<BlueLowNetworkUserCatalogueEntity>(
          context, cancellationToken);
      var redLowCatalogue = await fixture
        .CreateInDb<RedLowNetworkUserCatalogueEntity>(
          context, cancellationToken);
      var regulatoryCatalogue = await fixture
        .CreateInDb<RegulatoryCatalogueEntity>(
          context, cancellationToken);
      var whiteMediumCatalogue = await fixture
        .CreateInDb<WhiteMediumNetworkUserCatalogueEntity>(
          context, cancellationToken);
      var whiteLowCatalogue = await fixture
        .CreateInDb<WhiteLowNetworkUserCatalogueEntity>(
          context, cancellationToken);
      var location = await fixture
        .Build<LocationEntity>()
        .With(x => x.BlueLowNetworkUserCatalogueId, blueLowCatalogue.Id)
        .With(x => x.RedLowNetworkUserCatalogueId, redLowCatalogue.Id)
        .With(x => x.RegulatoryCatalogueId, regulatoryCatalogue.Id)
        .With(x => x.WhiteMediumNetworkUserCatalogueId, whiteMediumCatalogue.Id)
        .With(x => x.WhiteLowNetworkUserCatalogueId, whiteLowCatalogue.Id)
        .CreateInDb(context, cancellationToken);
      var networkUser = await fixture
        .Build<NetworkUserEntity>()
        .With(x => x.LocationId, location.Id)
        .CreateInDb(context, cancellationToken);

      var abbB2xValidator = await fixture
        .CreateInDb<AbbB2xMeasurementValidatorEntity>(
          context, cancellationToken);
      var abbB2xMeter = await fixture
        .Build<AbbB2xMeterEntity>()
        .IndexedWith(x => x.Id, (j) => $"abb-b2x-{i + j}")
        .With(x => x.MeasurementValidatorId, abbB2xValidator.Id)
        .CreateInDb(context, cancellationToken);
      var abbB2xMeasurementLocation = await fixture
        .Build<NetworkUserMeasurementLocationEntity>()
        .With(ml => ml.NetworkUserId, networkUser.Id)
        .With(ml => ml.MeterId, abbB2xMeter.Id)
        .With(ml => ml.NetworkUserCatalogueId, redLowCatalogue.Id)
        .CreateInDb(context, cancellationToken);

      var schneideriEM3xxxValidator = await fixture
        .CreateInDb<SchneideriEM3xxxMeasurementValidatorEntity>(
          context, cancellationToken);
      var schneideriEM3xxxMeter = await fixture
        .Build<SchneideriEM3xxxMeterEntity>()
        .IndexedWith(x => x.Id, (j) => $"schneider-iEM3xxx-{i + j}")
        .With(x => x.MeasurementValidatorId, schneideriEM3xxxValidator.Id)
        .CreateInDb(context, cancellationToken);
      var schneideriEM3xxxMeasurementLocation = await fixture
        .Build<NetworkUserMeasurementLocationEntity>()
        .With(ml => ml.NetworkUserId, networkUser.Id)
        .With(ml => ml.MeterId, schneideriEM3xxxMeter.Id)
        .With(ml => ml.NetworkUserCatalogueId, redLowCatalogue.Id)
        .CreateInDb(context, cancellationToken);

      var abbB2xMeasurements = abbB2xMeasurementFactory(fixture
        .Build<AbbB2xMeasurementEntity>()
        .IndexedWith(x => x.Timestamp, MeasurementTimestampByIndex)
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id));
      var abbB2xAggregates = abbB2xAggregateFactory(fixture
        .Build<AbbB2xAggregateEntity>()
        .IndexedWith(x => x.Timestamp, AggregateTimestampByIndex)
        .IndexedWith(x => x.Interval, IntervalByIndex)
        .IndexedWith(x => x.QuarterHourCount, QuarterHourCountByIndex)
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id));
      result.AddRange(abbB2xMeasurements);
      result.AddRange(abbB2xAggregates);

      var schneideriEM3xxxMeasurements = schneideriEM3xxxMeasurementFactory(
        fixture
          .Build<SchneideriEM3xxxMeasurementEntity>()
        .IndexedWith(x => x.Timestamp, MeasurementTimestampByIndex)
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id));
      var schneideriEM3xxxAggregates = schneideriEM3xxxAggregateFactory(
        fixture
          .Build<SchneideriEM3xxxAggregateEntity>()
          .IndexedWith(x => x.Timestamp, AggregateTimestampByIndex)
          .IndexedWith(x => x.Interval, IntervalByIndex)
          .IndexedWith(x => x.QuarterHourCount, QuarterHourCountByIndex)
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id));
      result.AddRange(schneideriEM3xxxMeasurements);
      result.AddRange(schneideriEM3xxxAggregates);
    }

    return result;
  }

  private static DateTimeOffset MeasurementTimestampByIndex(int i) =>
    Now.AddMinutes(-i);

  private static DateTimeOffset AggregateTimestampByIndex(int i) =>
      i % 3 == 0
    ? NowStartOfQuarterHour.AddMinutes(-(i / 3) * 15)
    : i % 3 == 1
      ? NowStartOfDay.AddDays(-(i / 3))
      : NowStartOfMonth.AddMonths(-(i / 3));

  private static IntervalEntity IntervalByIndex(int i) =>
      i % 3 == 0
    ? IntervalEntity.QuarterHour
    : i % 3 == 1
      ? IntervalEntity.Day
      : IntervalEntity.Month;

  private static long QuarterHourCountByIndex(int i) =>
    IntervalByIndex(i) == IntervalEntity.QuarterHour
      ? 1
      : 0;
}
