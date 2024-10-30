using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Joins;

namespace Ozds.Data.Test.Fixtures;

public class AnalysisBasisEntityFactory(
  DataDbContextFixtureFactory contextFixture,
  NoiseFixtureService noiseFixture
)
{
  public async Task<List<AnalysisBasisEntity>>
    Create(
      string representativeId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate
    )
  {
    var context = await contextFixture.Context.Value;

    var result = new List<AnalysisBasisEntity>();

    var fixture = new Fixture();
    fixture.Customizations.Add(
      new DateTimeOffsetInRangeSpecimenBuilder(fromDate, toDate));

    var representative = fixture
      .Build<RepresentativeEntity>()
      .With(x => x.Id, representativeId)
      .Create();
    context.Add(representative);
    await context.SaveChangesAsync();

    foreach (var i in Enumerable.Range(1, Constants.DefaultFuzzCount))
    {
      var location = fixture.Create<LocationEntity>();
      context.Add(location);
      await context.SaveChangesAsync();

      var networkUser = fixture
        .Build<NetworkUserEntity>()
        .With(x => x.LocationId, location.Id)
        .Create();
      context.Add(networkUser);
      await context.SaveChangesAsync();

      var networkUserRepresentative = fixture
        .Build<NetworkUserRepresentativeEntity>()
        .With(x => x.RepresentativeId, representative.Id)
        .With(x => x.NetworkUserId, networkUser.Id)
        .Create();
      context.Add(networkUserRepresentative);
      await context.SaveChangesAsync();

      var meter = fixture.Create<AbbB2xMeterEntity>();
      context.Add(meter);
      await context.SaveChangesAsync();

      var measurementLocation = fixture
        .Build<NetworkUserMeasurementLocationEntity>()
        .With(ml => ml.NetworkUserId, networkUser.Id)
        .With(ml => ml.MeterId, meter.Id)
        .Create();
      context.Add(measurementLocation);
      await context.SaveChangesAsync();

      var invoices = fixture
        .Build<NetworkUserInvoiceEntity>()
        .With(inv => inv.NetworkUserId, networkUser.Id)
        .CreateMany(Constants.DefaultFuzzCount)
        .ToList();
      context.AddRange(invoices);
      await context.SaveChangesAsync();

      var calculations = invoices
        .SelectMany(invoice => fixture
          .Build<NetworkUserCalculationEntity>()
          .With(
            calc => calc.NetworkUserMeasurementLocationId,
            measurementLocation.Id)
          .With(calc => calc.NetworkUserInvoiceId, invoice.Id)
          .CreateMany(Constants.DefaultFuzzCount)
          .ToList());
      context.AddRange(calculations);
      await context.SaveChangesAsync();

      var measurements = fixture
        .Build<AbbB2xMeasurementEntity>()
        .With(meas => meas.MeterId, meter.Id)
        .CreateMany(Constants.DefaultFuzzCount)
        .ToList();
      context.AddRange(measurements);
      await context.SaveChangesAsync();

      var monthlyAggregates = fixture
        .Build<AbbB2xAggregateEntity>()
        .With(agg => agg.MeterId, meter.Id)
        .CreateMany(Constants.DefaultFuzzCount)
        .ToList();
      context.AddRange(monthlyAggregates);
      await context.SaveChangesAsync();

      var monthlyMaxPowerAggregates = fixture
        .Build<AbbB2xAggregateEntity>()
        .With(agg => agg.MeterId, meter.Id)
        .CreateMany(Constants.DefaultFuzzCount)
        .ToList();
      context.AddRange(monthlyMaxPowerAggregates);
      await context.SaveChangesAsync();

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

  public async Task CreateNoise()
  {
    await noiseFixture.Create<RepresentativeEntity>();
    await noiseFixture.Create<LocationEntity>();
    await noiseFixture.Create<NetworkUserRepresentativeEntity>();
    await noiseFixture.Create<NetworkUserEntity>();
    await noiseFixture.Create<NetworkUserInvoiceEntity>();
    await noiseFixture.Create<NetworkUserCalculationEntity>();
    await noiseFixture.Create<AbbB2xMeterEntity>();
    await noiseFixture.Create<NetworkUserMeasurementLocationEntity>();
    await noiseFixture.Create<AbbB2xAggregateEntity>();
  }
}
