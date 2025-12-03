using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Mutations;
using Ozds.Data.Queries;
using Ozds.Data.Test.Base;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Queries.BillingQueriesTest;

public class ReadInvoiceBasisByNetworkUserTest : OzdsDataTestBase
{
  public record BillingScenario(
    string Name,
    Dictionary<string, bool> MeasurementsMap,
    string QueryMonth,
    string ExpectedStart,
    string ExpectedEnd,
    int NumberOfAggregates
  );

  public static IEnumerable<BillingScenario> BillingScenarios()
  {
    const string Oct1 = "2023-10-01T00:00:00+02:00";
    const string Nov1 = "2023-11-01T00:00:00+01:00";
    const string Dec1 = "2023-12-01T00:00:00+01:00";
    const string Jan1 = "2024-01-01T00:00:00+01:00";

    return new[]
    {
      new BillingScenario(
        "1. Metered-Metered-Metered (Normal Month)",
        new()
        {
          { Oct1, true },
          { Nov1, true },
          { Dec1, true }
        },
        QueryMonth: Nov1,
        ExpectedStart: Nov1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 11
      ),
      new BillingScenario(
        "2. Blackout-Metered-Metered (Prev Blackout ignored for current)",
        new()
        {
          { Oct1, false },
          { Nov1, true },
          { Dec1, true }
        },
        QueryMonth: Nov1,
        ExpectedStart: Nov1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 11
      ),
      new BillingScenario(
        "3. Blackout-Blackout-Metered (No history to anchor to)",
        new()
        {
          { Oct1, false },
          { Nov1, false },
          { Dec1, true }
        },
        QueryMonth: Nov1,
        ExpectedStart: Nov1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 1
      ),
      new BillingScenario(
        "4. Metered-Blackout-Metered (Bridge the gap)",
        new()
        {
          { Oct1, true },
          { Nov1, false },
          { Dec1, true }
        },
        QueryMonth: Nov1,
        ExpectedStart: Oct1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 2
      ),
      new BillingScenario(
        "5. Metered-Blackout-Blackout (Anchor to Oct, End at requested)",
        new()
        {
          { Oct1, true },
          { Nov1, false },
          { Dec1, false }
        },
        QueryMonth: Nov1,
        ExpectedStart: Oct1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 1
      ),
      new BillingScenario(
        "6. Metered-Metered-Blackout (Normal start, End at requested)",
        new()
        {
          { Oct1, true },
          { Nov1, true },
          { Dec1, false }
        },
        QueryMonth: Nov1,
        ExpectedStart: Nov1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 10
      ),
      new BillingScenario(
        "7. Metered-Blackout-Metered-Metered (Bridge gap with future data)",
        new()
        {
          { Oct1, true },
          { Nov1, false },
          { Dec1, true },
          { Jan1, true }
        },
        QueryMonth: Nov1,
        ExpectedStart: Oct1,
        ExpectedEnd: Dec1,
        NumberOfAggregates: 2
      ),
      new BillingScenario(
        "8. Metered-Metered-Blackout-Metered (Bridge gap for Dec)",
        new()
        {
          { Oct1, true },
          { Nov1, true },
          { Dec1, false },
          { Jan1, true }
        },
        QueryMonth: Dec1,
        ExpectedStart: Nov1,
        ExpectedEnd: Jan1,
        NumberOfAggregates: 2
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(BillingScenarios))]
  public async Task Calculate_Billing_Period_Correctly(
    BillingScenario scenario,
    CancellationToken cancellationToken
  )
  {
    var queryMonth = ParseDate(scenario.QueryMonth);
    var expectedStart = ParseDate(scenario.ExpectedStart);
    var expectedEnd = ParseDate(scenario.ExpectedEnd);

    var fromDate = queryMonth;
    var toDate = queryMonth.AddMonths(1);

    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x
        .WithMeterType(typeof(AbbB2xMeterEntity))
        .WithNetworkUserCatalogueId(x => x.RedLowNetworkUserCatalogueId));

    var allMeasurements = new List<IMeasurementEntity>();

    foreach (var (dateStr, hasData) in scenario.MeasurementsMap)
    {
      if (!hasData)
      {
        continue;
      }

      var monthStart = ParseDate(dateStr);

      var monthData = await Measurements.Create(
        infrastructure,
        cancellationToken,
        x => x
          .WithInterval(IntervalEntity.QuarterHour)
          .WithFromDate(monthStart)
          .WithToDate(monthStart.AddMonths(1))
          .WithCount(10)
      );

      allMeasurements.AddRange(monthData);
    }

    await ServiceProvider
      .GetRequiredService<MeasurementMutations>()
      .Create(allMeasurements, cancellationToken);

    var result = await ServiceProvider
      .GetRequiredService<BillingQueries>()
      .ReadInvoiceBasisByNetworkUser(
        infrastructure.NetworkUser.Id,
        fromDate,
        toDate,
        cancellationToken
      );

    result.Should().NotBeNull();
    result.NetworkUserCalculationBases.Should().HaveCount(1);

    var basis = result.NetworkUserCalculationBases.First();

    basis.BilledFromDate.Should().Be(expectedStart,
      $"Scenario '{scenario.Name}': Billing Start mismatch");

    basis.BilledToDate.Should().Be(expectedEnd,
      $"Scenario '{scenario.Name}': Billing End mismatch");

    if (expectedStart < expectedEnd)
    {
      basis.Aggregates.Should().NotBeEmpty();
      basis.Aggregates.First().Timestamp.Should()
        .BeOnOrAfter(basis.MeasuredFromDate);
    }

    basis.Aggregates.Should().HaveCount(scenario.NumberOfAggregates);

    var expected = new NetworkUserInvoiceBasisEntity
    {
      Location = infrastructure.Location,
      NetworkUser = infrastructure.NetworkUser,
      RegulatoryCatalogue = infrastructure.RegulatoryCatalogue,
      FromDate = fromDate,
      ToDate = toDate,
      NetworkUserCalculationBases = [
        new()
        {
          FromDate = fromDate,
          ToDate = toDate,
          MeasuredFromDate = expectedStart,
          MeasuredToDate = expectedEnd,
          BilledFromDate = expectedStart,
          BilledToDate = expectedEnd,
          Location = infrastructure.Location,
          NetworkUser = infrastructure.NetworkUser,
          MeasurementLocation = infrastructure.MeasurementLocation,
          UsageNetworkUserCatalogue = infrastructure.RedLowNetworkUserCatalogue,
          SupplyRegulatoryCatalogue = infrastructure.RegulatoryCatalogue,
          Meter = infrastructure.Meter,
          Aggregates = allMeasurements
            .OfType<AggregateEntity>()
            .Where(x =>
              x.Timestamp >= fromDate
              && x.Timestamp <= toDate)
            .Concat(allMeasurements
              .OfType<AggregateEntity>()
              .Where(x => x.Timestamp == expectedStart))
            .Concat(allMeasurements
              .OfType<AggregateEntity>()
              .Where(x => x.Timestamp == expectedEnd))
            .DistinctBy(x => x.Timestamp)
            .ToList()
        }
      ]
    };

    await using var context = await ServiceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync(cancellationToken);

    result.Should().BeContextuallyEquivalentTo(context, expected);
  }

  private static DateTimeOffset ParseDate(string isoString)
  {
    return new DateTimeOffset(
      DateTime.SpecifyKind(
        DateTimeOffset.Parse(
          isoString,
          CultureInfo.InvariantCulture).UtcDateTime,
        DateTimeKind.Utc),
      TimeSpan.Zero);
  }
}
