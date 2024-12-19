using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
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

  public async Task<List<IMeasurementEntity>> Create(CancellationToken cancellationToken)
  {
    var result = new List<IMeasurementEntity>();

    var now = DateTimeOffset.UtcNow;
    var aMonthAgo = now.AddMonths(-1);

    var fixture = context.ContextualFixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(aMonthAgo, now));

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

      var abbB2xMeasurements = fixture
        .Build<AbbB2xMeasurementEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .CreateMany(MeasurementCount);
      var abbB2xAggregatesQuarterHour = fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.QuarterHour)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMinutes(-i * 15)
        )
        .CreateMany(AggregateCount);
      var abbB2xAggregatesDay = fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Day)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddDays(-i)
        )
        .CreateMany(AggregateCount);
      var abbB2xAggregatesMonth = fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Month)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMonths(-i)
        )
        .CreateMany(AggregateCount);
      result.AddRange(abbB2xMeasurements);
      result.AddRange(abbB2xAggregatesQuarterHour);
      result.AddRange(abbB2xAggregatesDay);
      result.AddRange(abbB2xAggregatesMonth);

      var schneideriEM3xxxMeasurements = fixture
        .Build<SchneideriEM3xxxMeasurementEntity>()
        .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
        .With(x => x.MeasurementLocationId, schneideriEM3xxxMeasurementLocation.Id)
        .CreateMany(MeasurementCount);
      var schneideriEM3xxxAggregates = fixture
        .Build<SchneideriEM3xxxAggregateEntity>()
        .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
        .With(x => x.MeasurementLocationId, schneideriEM3xxxMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.QuarterHour)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMinutes(-i * 15)
        )
        .CreateMany(AggregateCount);
      var schneideriEM3xxxAggregatesDay = fixture
        .Build<SchneideriEM3xxxAggregateEntity>()
        .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
        .With(x => x.MeasurementLocationId, schneideriEM3xxxMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Day)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddDays(-i)
        )
        .CreateMany(AggregateCount);
      var schneideriEM3xxxAggregatesMonth = fixture
        .Build<SchneideriEM3xxxAggregateEntity>()
        .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
        .With(x => x.MeasurementLocationId, schneideriEM3xxxMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Month)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMonths(-i)
        )
        .CreateMany(AggregateCount);
      result.AddRange(schneideriEM3xxxMeasurements);
      result.AddRange(schneideriEM3xxxAggregates);
      result.AddRange(schneideriEM3xxxAggregatesDay);
      result.AddRange(schneideriEM3xxxAggregatesMonth);
    }

    return result;
  }
}
