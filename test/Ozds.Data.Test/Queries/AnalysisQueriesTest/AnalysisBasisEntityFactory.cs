using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Test.Extensions;
using Ozds.Data.Test.Specimens;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class AnalysisBasisEntityFactory(DbContext context)
{
  public async Task<List<AnalysisBasisEntity>>
    Create(
      string representativeId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var result = new List<AnalysisBasisEntity>();

    var fixture = context.Fixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(fromDate, toDate));

    var representative = await fixture
      .Build<RepresentativeEntity>()
      .With(x => x.Id, representativeId)
      .CreateInDb(context);

    foreach (var i in Enumerable.Range(1, Constants.DefaultFuzzCount))
    {
      var blueLowCatalogue = await fixture
        .CreateInDb<BlueLowNetworkUserCatalogueEntity>(context);
      var redLowCatalogue = await fixture
        .CreateInDb<RedLowNetworkUserCatalogueEntity>(context);
      var regulatoryCatalogue = await fixture
        .CreateInDb<RegulatoryCatalogueEntity>(context);
      var whiteMediumCatalogue = await fixture
        .CreateInDb<WhiteMediumNetworkUserCatalogueEntity>(context);
      var whiteLowCatalogue = await fixture
        .CreateInDb<WhiteLowNetworkUserCatalogueEntity>(context);

      var location = await fixture
        .Build<LocationEntity>()
        .With(x => x.BlueLowNetworkUserCatalogueId, blueLowCatalogue.Id)
        .With(x => x.RedLowNetworkUserCatalogueId, redLowCatalogue.Id)
        .With(x => x.RegulatoryCatalogueId, regulatoryCatalogue.Id)
        .With(x => x.WhiteMediumNetworkUserCatalogueId, whiteMediumCatalogue.Id)
        .With(x => x.WhiteLowNetworkUserCatalogueId, whiteLowCatalogue.Id)
        .CreateInDb(context);

      var networkUser = await fixture
        .Build<NetworkUserEntity>()
        .With(x => x.LocationId, location.Id)
        .CreateInDb(context);

      _ = await fixture
        .Build<NetworkUserRepresentativeEntity>()
        .With(x => x.RepresentativeId, representative.Id)
        .With(x => x.NetworkUserId, networkUser.Id)
        .CreateInDb(context);

      var validator = await fixture
        .CreateInDb<AbbB2xMeasurementValidatorEntity>(context);

      var meter = await fixture
        .Build<AbbB2xMeterEntity>()
        .IndexedWith(x => x.Id, (i) => $"abb-b2x-{i}")
        .With(x => x.MeasurementValidatorId, validator.Id)
        .CreateInDb(context);

      var measurementLocation = await fixture
        .Build<NetworkUserMeasurementLocationEntity>()
        .With(ml => ml.NetworkUserId, networkUser.Id)
        .With(ml => ml.MeterId, meter.Id)
        .With(ml => ml.NetworkUserCatalogueId, redLowCatalogue.Id)
        .CreateInDb(context);

      var invoices = await fixture
        .Build<NetworkUserInvoiceEntity>()
        .With(inv => inv.NetworkUserId, networkUser.Id)
        .CreateManyInDb(context);

      var calculations = await fixture
        .Build<NetworkUserCalculationEntity>()
        .With(
          calc => calc.MeterId,
          meter.Id)
        .With(
          calc => calc.NetworkUserMeasurementLocationId,
          measurementLocation.Id)
        .IndexedWith(
          calc => calc.NetworkUserInvoiceId,
          i => invoices[i % invoices.Count].Id)
        .CreateManyInDb(context, Constants.DefaultFuzzCount * invoices.Count);

      var measurements = await fixture
        .Build<AbbB2xMeasurementEntity>()
        .With(meas => meas.MeterId, meter.Id)
        .CreateManyInDb(context);

      var monthlyAggregates = await fixture
        .Build<AbbB2xAggregateEntity>()
        .With(agg => agg.MeterId, meter.Id)
        .CreateManyInDb(context);

      var monthlyMaxPowerAggregates = await fixture
        .Build<AbbB2xAggregateEntity>()
        .With(agg => agg.MeterId, meter.Id)
        .CreateManyInDb(context);

      var analysisBasisEntity = new AnalysisBasisEntity(
          Representative: representative,
          FromDate: fromDate,
          ToDate: toDate,
          Location: location,
          NetworkUser: networkUser,
          MeasurementLocation: measurementLocation,
          Meter: meter,
          Calculations: calculations.Cast<CalculationEntity>().ToList(),
          Invoices: invoices.Cast<InvoiceEntity>().ToList(),
          LastMeasurement: measurements.Last(),
          MonthlyAggregates: monthlyAggregates
            .Cast<AggregateEntity>()
            .ToList(),
          MonthlyMaxPowerAggregates: monthlyMaxPowerAggregates
            .Cast<AggregateEntity>()
            .ToList()
      );

      result.Add(analysisBasisEntity);
    }

    return result;
  }
}
