using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Complex;
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
    MeasurementCount / 10;
  private const int AggregateCountFew =
    MeasurementCountFew / 10;

  public async Task<List<IMeasurementEntity>> CreateDerivedNull(
    CancellationToken cancellationToken
  )
  {
    var derived = new InstantaneousAggregateMeasureEntity
    {
      Min = 0.0f,
      Max = 0.0f,
      Avg = 0.0f,
      MinTimestamp = DateTimeOffset.UtcNow,
      MaxTimestamp = DateTimeOffset.UtcNow
    };

    return await Create(
      cancellationToken,
      x => x.CreateMany(MeasurementCountFew),
      x => x
        .With(x => x.DerivedActivePowerL1ImportT0_W, derived)
        .With(x => x.DerivedActivePowerL2ImportT0_W, derived)
        .With(x => x.DerivedActivePowerL3ImportT0_W, derived)
        .With(x => x.DerivedActivePowerTotalImportT0_W, derived)
        .With(x => x.DerivedActivePowerL1ExportT0_W, derived)
        .With(x => x.DerivedActivePowerL2ExportT0_W, derived)
        .With(x => x.DerivedActivePowerL3ExportT0_W, derived)
        .With(x => x.DerivedActivePowerTotalExportT0_W, derived)
        .With(x => x.DerivedReactivePowerL1ImportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerL2ImportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerL3ImportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerTotalImportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerL1ExportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerL2ExportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerL3ExportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerTotalExportT0_VAR, derived)
        .With(x => x.DerivedActivePowerTotalImportT1_W, derived)
        .With(x => x.DerivedActivePowerTotalImportT2_W, derived)
        .CreateMany(AggregateCountFew),
      x => x.CreateMany(MeasurementCountFew),
      x => x
        .With(x => x.DerivedActivePowerL1ImportT0_W, derived)
        .With(x => x.DerivedActivePowerL2ImportT0_W, derived)
        .With(x => x.DerivedActivePowerL3ImportT0_W, derived)
        .With(x => x.DerivedActivePowerTotalImportT0_W, derived)
        .With(x => x.DerivedActivePowerTotalExportT0_W, derived)
        .With(x => x.DerivedReactivePowerTotalImportT0_VAR, derived)
        .With(x => x.DerivedReactivePowerTotalExportT0_VAR, derived)
        .With(x => x.DerivedActivePowerTotalImportT1_W, derived)
        .With(x => x.DerivedActivePowerTotalImportT2_W, derived)
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
      abbB2xMeasurementUpdater,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<AbbB2xAggregateEntity>,
      IEnumerable<AbbB2xAggregateEntity>>
      abbB2xAggregateUpdater,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<SchneideriEM3xxxMeasurementEntity>,
      IEnumerable<SchneideriEM3xxxMeasurementEntity>>
      schneideriEM3xxxMeasurementUpdater,
    Func<
      AutoFixture.Dsl.IPostprocessComposer<SchneideriEM3xxxAggregateEntity>,
      IEnumerable<SchneideriEM3xxxAggregateEntity>>
      schneideriEM3xxxAggregateUpdater
  )
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

      var abbB2xMeasurements = abbB2xMeasurementUpdater(fixture
        .Build<AbbB2xMeasurementEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id));
      var abbB2xAggregatesQuarterHour = abbB2xAggregateUpdater(fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.QuarterHour)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMinutes(-i * 15)
        ));
      var abbB2xAggregatesDay = abbB2xAggregateUpdater(fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Day)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddDays(-i)
        ));
      var abbB2xAggregatesMonth = abbB2xAggregateUpdater(fixture
        .Build<AbbB2xAggregateEntity>()
        .With(x => x.MeterId, abbB2xMeter.Id)
        .With(x => x.MeasurementLocationId, abbB2xMeasurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Month)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMonths(-i)
        ));
      result.AddRange(abbB2xMeasurements);
      result.AddRange(abbB2xAggregatesQuarterHour);
      result.AddRange(abbB2xAggregatesDay);
      result.AddRange(abbB2xAggregatesMonth);

      var schneideriEM3xxxMeasurements = schneideriEM3xxxMeasurementUpdater(
        fixture
          .Build<SchneideriEM3xxxMeasurementEntity>()
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id));
      var schneideriEM3xxxAggregates = schneideriEM3xxxAggregateUpdater(
        fixture
          .Build<SchneideriEM3xxxAggregateEntity>()
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id)
          .With(agg => agg.Interval, IntervalEntity.QuarterHour)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMinutes(-i * 15)
        ));
      var schneideriEM3xxxAggregatesDay = schneideriEM3xxxAggregateUpdater(
        fixture
          .Build<SchneideriEM3xxxAggregateEntity>()
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id)
          .With(agg => agg.Interval, IntervalEntity.Day)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddDays(-i)
        ));
      var schneideriEM3xxxAggregatesMonth = schneideriEM3xxxAggregateUpdater(
        fixture
          .Build<SchneideriEM3xxxAggregateEntity>()
          .With(x => x.MeterId, schneideriEM3xxxMeter.Id)
          .With(
            x => x.MeasurementLocationId,
            schneideriEM3xxxMeasurementLocation.Id)
          .With(agg => agg.Interval, IntervalEntity.Month)
        .IndexedWith(
          agg => agg.Timestamp,
          (i) => now.AddMonths(-i)
        ));
      result.AddRange(schneideriEM3xxxMeasurements);
      result.AddRange(schneideriEM3xxxAggregates);
      result.AddRange(schneideriEM3xxxAggregatesDay);
      result.AddRange(schneideriEM3xxxAggregatesMonth);
    }

    return result;
  }
}
