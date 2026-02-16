using System.Globalization;
using Ozds.Business.Authorization;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Fake.Identification;
using Ozds.Sdk.Contracts.V1;
using Ozds.Server.Test.Base;

namespace Ozds.Server.Test.Controllers.Api.V1;

public class ApiV1MeasurementsControllerTest : OzdsServerTestBase
{
  private const string DateTo = "2025-10-16T11:43:02.931923222+02:00";
  private const int NumberOfMeasurementLocations = 10;

  [Test]
  public async Task
    MeasurementsController_GetsQuarterHourlyAggregatesByLocation(
      CancellationToken cancellationToken
    )
  {
    var dateTo = DateTimeOffset.Parse(DateTo, CultureInfo.InvariantCulture);
    var dateFrom = dateTo.AddDays(-1);

    var testRegisters = TestRegister.SchneideriEM3xxxSet;

    var measurementLocation = await MeasurementLocation.Create(
      cancellationToken,
      x => x
        .WithMeter(
          x => x
            .WithMeterType(typeof(SchneideriEM3xxxMeterModel))));

    var scope = await Scope.CreateMeasurementForLocationWithRegisters(
      measurementLocation.Location,
      testRegisters,
      cancellationToken
    );

    var apiKey = await ApiKey.CreateForUserAndScope(
      TestUser.Operator,
      scope.MeasurementScope,
      cancellationToken);

    var inserted = await Measurement
      .Insert(
        [
          new MeasurementLocationMeterId(
            measurementLocation.MeasurementLocation.Id,
            measurementLocation.Meter.Id)
        ],
        dateFrom,
        dateTo,
        cancellationToken
      )
      .OfType<IAggregate>()
      .Where(aggregate => aggregate.Interval == IntervalModel.QuarterHour)
      .ToListAsync(cancellationToken);

    var client = Services.GetRequiredService<IOzdsApiV1Client>();
    var apiKeyManager = Services.GetRequiredService<ApiKeyManager>();

    client.ApiKey = apiKeyManager.Tokenize(apiKey.ApiKey);

    var fetched = await client.QuarterHourlyAggregatesByLocationAsync(
      measurementLocation.Location.Id,
      dateFrom,
      dateTo,
      0,
      cancellationToken
    );

    fetched.LocationId.Should().Be(measurementLocation.Location.Id);
    fetched.DateFrom.Should().BeCloseTo(dateFrom, TimeSpan.FromSeconds(1));
    fetched.DateTo.Should().BeCloseTo(dateTo, TimeSpan.FromSeconds(1));
    fetched.Page.Should().Be(0);
    fetched.PageSize.Should().BeGreaterThan(inserted.Count);
    fetched.TotalCount.Should().Be(fetched.Measurements.Count);
    fetched.TotalCount.Should().Be(inserted.Count);

    var registers = testRegisters
      .Select(testRegister => Scope.TestRegisterToRegisterModel(testRegister))
      .ToList();

    foreach (var fetchedMeasurement in fetched.Measurements)
    {
      fetchedMeasurement.MeasurementLocationId
        .Should()
        .Be(measurementLocation.MeasurementLocation.Id);
      fetchedMeasurement.MeterId.Should().Be(measurementLocation.Meter.Id);
      fetchedMeasurement.Timestamp.Should().BeAfter(dateFrom);
      fetchedMeasurement.Timestamp.Should().BeBefore(dateTo);

      var insertedMeasurement = inserted
        .FirstOrDefault(
          insertedMeasurement =>
            insertedMeasurement.Timestamp == fetchedMeasurement.Timestamp
            && insertedMeasurement.MeasurementLocationId
            == fetchedMeasurement.MeasurementLocationId
            && insertedMeasurement.MeterId == fetchedMeasurement.MeterId)!;
      insertedMeasurement.Should().NotBeNull();

      foreach (var register in registers)
      {
        var insertedValue = insertedMeasurement.RegisterValue(register);

        var fetchedValue = fetchedMeasurement.Registers
          .FirstOrDefault(
            fetchedRegister =>
              fetchedRegister.Key == register.Name)!;
        fetchedValue.Should().NotBeNull();

        fetchedValue.Value.Should().Be(insertedValue.ToString());
      }
    }
  }

  [Test]
  public async Task
    MeasurementsController_GetsQuarterHourlyAggregatesByLocationLast(
      CancellationToken cancellationToken
    )
  {
    DateTimeOffset? dateFrom = null;
    DateTimeOffset? dateTo = null;
    var testRegisters = TestRegister.SchneideriEM3xxxSet;

    Action<TestMeasurementLocationFixture.Configurator>
      measurementLocationMeterConfigurator =
        x => x
          .WithMeter(
            x => x
              .WithMeterType(typeof(SchneideriEM3xxxMeterModel)));

    var measurementLocation = await MeasurementLocation.Create(
      cancellationToken,
      measurementLocationMeterConfigurator
    );

    var measurementLocations = (await Task.WhenAll(
      Enumerable
        .Range(0, NumberOfMeasurementLocations - 1)
        .Select(
          _ => MeasurementLocation.Create(
            measurementLocation,
            cancellationToken,
            measurementLocationMeterConfigurator
          )))).ToList();

    measurementLocations.Add(measurementLocation);

    var scope = await Scope.CreateMeasurementForLocationWithRegisters(
      measurementLocation.Location,
      testRegisters,
      cancellationToken
    );

    var apiKey = await ApiKey.CreateForUserAndScope(
      TestUser.Operator,
      scope.MeasurementScope,
      cancellationToken);

    var insertionDateTo = DateTimeOffset.Parse(
      DateTo, CultureInfo.InvariantCulture);
    var insertionDateFrom = insertionDateTo.AddDays(-1);

    var inserted = await Measurement.Insert(
        measurementLocations.Select(
          m => new MeasurementLocationMeterId(
            m.MeasurementLocation.Id,
            m.Meter.Id
          )
        ),
        insertionDateFrom,
        insertionDateTo,
        cancellationToken
      ).OfType<IAggregate>()
      .Where(
        aggregate =>
          aggregate.Interval == IntervalModel.QuarterHour
      ).ToListAsync(cancellationToken);

    var client = Services.GetRequiredService<IOzdsApiV1Client>();
    var apiKeyManager = Services.GetRequiredService<ApiKeyManager>();

    client.ApiKey = apiKeyManager.Tokenize(apiKey.ApiKey);

    var fetched = await client.QuarterHourlyAggregatesByLocationAsync(
      measurementLocation.Location.Id,
      dateFrom,
      dateTo,
      0,
      cancellationToken
    );

    var insertedLatestMeasurements = inserted
      .GroupBy(
        m => (m.MeterId, m.MeasurementLocationId)
      )
      .Select(
        mg =>
          mg.MaxBy(m => m.Timestamp)!
      );

    fetched.LocationId.Should().Be(measurementLocation.Location.Id);
    fetched.Page.Should().Be(0);
    fetched.PageSize.Should().BeGreaterThan(inserted.Count);
    fetched.TotalCount.Should().Be(fetched.Measurements.Count);
    fetched.TotalCount.Should().Be(insertedLatestMeasurements!.Count());

    var registers = testRegisters
      .Select(testRegister => Scope.TestRegisterToRegisterModel(testRegister))
      .ToList();

    foreach (var fetchedMeasurement in fetched.Measurements)
    {
      var insertedMeasurement = insertedLatestMeasurements
        .FirstOrDefault(
          insertedMeasurement =>
            insertedMeasurement.Timestamp == fetchedMeasurement.Timestamp
            && insertedMeasurement.MeasurementLocationId
            == fetchedMeasurement.MeasurementLocationId
            && insertedMeasurement.MeterId == fetchedMeasurement.MeterId)!;
      insertedMeasurement.Should().NotBeNull();

      foreach (var register in registers)
      {
        var insertedValue = insertedMeasurement.RegisterValue(register);

        var fetchedValue = fetchedMeasurement.Registers
          .FirstOrDefault(
            fetchedRegister =>
              fetchedRegister.Key == register.Name)!;
        fetchedValue.Should().NotBeNull();

        fetchedValue.Value.Should().Be(insertedValue.ToString());
      }
    }
  }

  [Test]
  public async Task
    MeasurementsController_GetsQuarterHourlyAggregatesByNetworkUser(
      CancellationToken cancellationToken
    )
  {
    var dateTo = DateTimeOffset.Parse(DateTo, CultureInfo.InvariantCulture);
    var dateFrom = dateTo.AddDays(-1);

    var testRegisters = TestRegister.SchneideriEM3xxxSet;

    var measurementLocation = await MeasurementLocation.Create(
      cancellationToken,
      x => x
        .WithMeter(
          x => x
            .WithMeterType(typeof(SchneideriEM3xxxMeterModel))));

    var scope = await Scope.CreateMeasurementForNetworkUserWithRegisters(
      measurementLocation.NetworkUser,
      testRegisters,
      cancellationToken
    );

    var apiKey = await ApiKey.CreateForUserAndScope(
      TestUser.Operator,
      scope.MeasurementScope,
      cancellationToken);

    var inserted = await Measurement
      .Insert(
        [
          new MeasurementLocationMeterId(
            measurementLocation.MeasurementLocation.Id,
            measurementLocation.Meter.Id)
        ],
        dateFrom,
        dateTo,
        cancellationToken
      )
      .OfType<IAggregate>()
      .Where(aggregate => aggregate.Interval == IntervalModel.QuarterHour)
      .ToListAsync(cancellationToken);

    var client = Services.GetRequiredService<IOzdsApiV1Client>();
    var apiKeyManager = Services.GetRequiredService<ApiKeyManager>();

    client.ApiKey = apiKeyManager.Tokenize(apiKey.ApiKey);

    var fetched = await client.QuarterHourlyAggregatesByNetworkUserAsync(
      measurementLocation.NetworkUser.Id,
      dateFrom,
      dateTo,
      0,
      cancellationToken
    );

    fetched.NetworkUserId.Should().Be(measurementLocation.NetworkUser.Id);
    fetched.DateFrom.Should().BeCloseTo(dateFrom, TimeSpan.FromSeconds(1));
    fetched.DateTo.Should().BeCloseTo(dateTo, TimeSpan.FromSeconds(1));
    fetched.Page.Should().Be(0);
    fetched.PageSize.Should().BeGreaterThan(inserted.Count);
    fetched.TotalCount.Should().Be(fetched.Measurements.Count);
    fetched.TotalCount.Should().Be(inserted.Count);

    var registers = testRegisters
      .Select(testRegister => Scope.TestRegisterToRegisterModel(testRegister))
      .ToList();

    foreach (var fetchedMeasurement in fetched.Measurements)
    {
      fetchedMeasurement.MeasurementLocationId
        .Should()
        .Be(measurementLocation.MeasurementLocation.Id);
      fetchedMeasurement.MeterId.Should().Be(measurementLocation.Meter.Id);
      fetchedMeasurement.Timestamp.Should().BeAfter(dateFrom);
      fetchedMeasurement.Timestamp.Should().BeBefore(dateTo);

      var insertedMeasurement = inserted
        .FirstOrDefault(
          insertedMeasurement =>
            insertedMeasurement.Timestamp == fetchedMeasurement.Timestamp
            && insertedMeasurement.MeasurementLocationId
            == fetchedMeasurement.MeasurementLocationId
            && insertedMeasurement.MeterId == fetchedMeasurement.MeterId)!;
      insertedMeasurement.Should().NotBeNull();

      foreach (var register in registers)
      {
        var insertedValue = insertedMeasurement.RegisterValue(register);

        var fetchedValue = fetchedMeasurement.Registers
          .FirstOrDefault(
            fetchedRegister =>
              fetchedRegister.Key == register.Name)!;
        fetchedValue.Should().NotBeNull();

        fetchedValue.Value.Should().Be(insertedValue.ToString());
      }
    }
  }

  [Test]
  public async Task
    MeasurementsController_GetsQuarterHourlyAggregatesByMeasurementLocation(
      CancellationToken cancellationToken
    )
  {
    var dateTo = DateTimeOffset.Parse(DateTo, CultureInfo.InvariantCulture);
    var dateFrom = dateTo.AddDays(-1);

    var testRegisters = TestRegister.SchneideriEM3xxxSet;

    var measurementLocation = await MeasurementLocation.Create(
      cancellationToken,
      x => x
        .WithMeter(
          x => x
            .WithMeterType(typeof(SchneideriEM3xxxMeterModel))));

    var scope =
      await Scope.CreateMeasurementForMeasurementLocationWithRegisters(
        measurementLocation.MeasurementLocation,
        testRegisters,
        cancellationToken
      );

    var apiKey = await ApiKey.CreateForUserAndScope(
      TestUser.Operator,
      scope.MeasurementScope,
      cancellationToken);

    var inserted = await Measurement
      .Insert(
        [
          new MeasurementLocationMeterId(
            measurementLocation.MeasurementLocation.Id,
            measurementLocation.Meter.Id)
        ],
        dateFrom,
        dateTo,
        cancellationToken
      )
      .OfType<IAggregate>()
      .Where(aggregate => aggregate.Interval == IntervalModel.QuarterHour)
      .ToListAsync(cancellationToken);

    var client = Services.GetRequiredService<IOzdsApiV1Client>();
    var apiKeyManager = Services.GetRequiredService<ApiKeyManager>();

    client.ApiKey = apiKeyManager.Tokenize(apiKey.ApiKey);

    var fetched =
      await client.QuarterHourlyAggregatesByMeasurementLocationAsync(
        measurementLocation.MeasurementLocation.Id,
        dateFrom,
        dateTo,
        0,
        cancellationToken
      );

    fetched.MeasurementLocationId.Should()
      .Be(measurementLocation.MeasurementLocation.Id);
    fetched.DateFrom.Should().BeCloseTo(dateFrom, TimeSpan.FromSeconds(1));
    fetched.DateTo.Should().BeCloseTo(dateTo, TimeSpan.FromSeconds(1));
    fetched.Page.Should().Be(0);
    fetched.PageSize.Should().BeGreaterThan(inserted.Count);
    fetched.TotalCount.Should().Be(fetched.Measurements.Count);
    fetched.TotalCount.Should().Be(inserted.Count);

    var registers = testRegisters
      .Select(testRegister => Scope.TestRegisterToRegisterModel(testRegister))
      .ToList();

    foreach (var fetchedMeasurement in fetched.Measurements)
    {
      fetchedMeasurement.MeasurementLocationId
        .Should()
        .Be(measurementLocation.MeasurementLocation.Id);
      fetchedMeasurement.MeterId.Should().Be(measurementLocation.Meter.Id);
      fetchedMeasurement.Timestamp.Should().BeAfter(dateFrom);
      fetchedMeasurement.Timestamp.Should().BeBefore(dateTo);

      var insertedMeasurement = inserted
        .FirstOrDefault(
          insertedMeasurement =>
            insertedMeasurement.Timestamp == fetchedMeasurement.Timestamp
            && insertedMeasurement.MeasurementLocationId
            == fetchedMeasurement.MeasurementLocationId
            && insertedMeasurement.MeterId == fetchedMeasurement.MeterId)!;
      insertedMeasurement.Should().NotBeNull();

      foreach (var register in registers)
      {
        var insertedValue = insertedMeasurement.RegisterValue(register);

        var fetchedValue = fetchedMeasurement.Registers
          .FirstOrDefault(
            fetchedRegister =>
              fetchedRegister.Key == register.Name)!;
        fetchedValue.Should().NotBeNull();

        fetchedValue.Value.Should().Be(insertedValue.ToString());
      }
    }
  }
}
