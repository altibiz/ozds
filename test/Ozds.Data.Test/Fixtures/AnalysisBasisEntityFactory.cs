using Microsoft.EntityFrameworkCore;
using Ozds.Business.Time;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Test.Extensions;
using Ozds.Data.Test.Specimens;

namespace Ozds.Data.Test.Fixtures;

public class AnalysisBasisEntityFactory(DbContext context)
{
  public async Task<List<AnalysisBasisEntity>>
    Create(
      string representativeId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var result = new List<AnalysisBasisEntity>();

    var fixture = context.ContextualFixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(fromDate, toDate));

    var representative = await fixture
      .Build<RepresentativeEntity>()
      .With(x => x.Id, representativeId)
      .CreateInDb(context, cancellationToken);

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

      _ = await fixture
        .Build<NetworkUserRepresentativeEntity>()
        .With(x => x.RepresentativeId, representative.Id)
        .With(x => x.NetworkUserId, networkUser.Id)
        .CreateInDb(context, cancellationToken);

      var validator = await fixture
        .CreateInDb<AbbB2xMeasurementValidatorEntity>(
          context, cancellationToken);

      var meter = await fixture
        .Build<AbbB2xMeterEntity>()
        .IndexedWith(x => x.Id, j => $"abb-b2x-{i + j}")
        .With(x => x.MeasurementValidatorId, validator.Id)
        .CreateInDb(context, cancellationToken);

      var measurementLocation = await fixture
        .Build<NetworkUserMeasurementLocationEntity>()
        .With(ml => ml.NetworkUserId, networkUser.Id)
        .With(ml => ml.MeterId, meter.Id)
        .With(ml => ml.NetworkUserCatalogueId, redLowCatalogue.Id)
        .CreateInDb(context, cancellationToken);

      var invoices = await fixture
        .Build<NetworkUserInvoiceEntity>()
        .With(inv => inv.ArchivedLocation, location)
        .With(inv => inv.NetworkUserId, networkUser.Id)
        .With(inv => inv.ArchivedNetworkUser, networkUser)
        .With(inv => inv.ArchivedRegulatoryCatalogue, regulatoryCatalogue)
        .CreateManyInDb(context, cancellationToken: cancellationToken);

      var calculations = await fixture
        .Build<RedLowNetworkUserCalculationEntity>()
        .With(
          calc => calc.MeterId,
          meter.Id)
        .With(
          calc => calc.NetworkUserMeasurementLocationId,
          measurementLocation.Id
        )
        .With(
          calc => calc.ArchivedNetworkUserMeasurementLocation,
          measurementLocation
        )
        .With(
          calc => calc.UsageNetworkUserCatalogueId,
          redLowCatalogue.Id
        )
        .With(
          calc => calc.ArchivedUsageNetworkUserCatalogue,
          redLowCatalogue
        )
        .With(
          calc => calc.SupplyRegulatoryCatalogueId,
          regulatoryCatalogue.Id
        )
        .With(
          calc => calc.ArchivedSupplyRegulatoryCatalogue,
          regulatoryCatalogue
        )
        .IndexedWith(
          calc => calc.NetworkUserInvoiceId,
          i => invoices[i % invoices.Count].Id)
        .CreateManyInDb(
          context,
          Constants.DefaultDbFuzzCount * invoices.Count,
          cancellationToken);

      var measurements = await fixture
        .Build<AbbB2xMeasurementEntity>()
        .With(meas => meas.MeterId, meter.Id)
        .With(meas => meas.MeasurementLocationId, measurementLocation.Id)
        .CreateManyInDb(context, cancellationToken: cancellationToken);

      var monthlyAggregates = await fixture
        .Build<AbbB2xAggregateEntity>()
        .With(agg => agg.MeterId, meter.Id)
        .With(agg => agg.MeasurementLocationId, measurementLocation.Id)
        .With(agg => agg.Interval, IntervalEntity.Month)
        .IndexedWith(
          agg => agg.Timestamp,
          i => toDate.AddMonths(-i).GetStartOfMonth()
        )
        .CreateManyInDb(context, cancellationToken: cancellationToken);

      var analysisBasisEntity = new AnalysisBasisEntity
      {
        Representative = representative,
        FromDate = fromDate,
        ToDate = toDate,
        Location = location,
        NetworkUser = networkUser,
        MeasurementLocation = measurementLocation,
        Meter = meter,
        Calculations = calculations.Cast<CalculationEntity>().ToList(),
        Invoices = invoices.Cast<InvoiceEntity>().ToList(),
        LastMeasurement = measurements.OrderBy(x => x.Timestamp).Last(),
        MonthlyAggregates = monthlyAggregates
          .Where(x => x.Timestamp >= fromDate && x.Timestamp < toDate)
          .Cast<AggregateEntity>()
          .ToList()
      };

      result.Add(analysisBasisEntity);
    }

    return result;
  }
}
