using System.Globalization;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Mutations;
using Ozds.Data.Queries;
using Ozds.Data.Test.Base;

namespace Ozds.Data.Test.Queries.ReportQueriesTest;

public class ReadEnergyCardReportBasisByNetworkUserTest : OzdsDataTestBase
{
  private const string Oct1 = "2023-10-01T00:00:00+00:00";
  private const string Nov1 = "2023-11-01T00:00:00+00:00";
  private const string Dec1 = "2023-12-01T00:00:00+00:00";
  private const string Jan1 = "2024-01-01T00:00:00+00:00";

  public static IEnumerable<EnergyCardScenario> Scenarios()
  {
    return new[]
    {
      new EnergyCardScenario(
        "1. Normal month - data in queried window returns result",
        new Dictionary<string, bool> { { Nov1, true }, { Dec1, true} },
        Nov1,
        Dec1,
        true
      ),
      new EnergyCardScenario(
        "2. No data anywhere - empty result",
        new Dictionary<string, bool>(),
        Nov1,
        Dec1,
        false
      ),
      new EnergyCardScenario(
        "3. Data only in previous month - empty result for queried window",
        new Dictionary<string, bool> { { Oct1, true } },
        Nov1,
        Dec1,
        false
      ),
      new EnergyCardScenario(
        "4. Data not in selected range - empty result for queried window",
        new Dictionary<string, bool> { { Jan1, true } },
        Nov1,
        Dec1,
        false
      ),
      new EnergyCardScenario(
        "5. Data in Oct, Nov, Dec - query Nov returns result bounded to Nov",
        new Dictionary<string, bool>
        {
          { Oct1, true },
          { Nov1, true },
          { Dec1, true },
        },
        Nov1,
        Dec1,
        true
      ),
      new EnergyCardScenario(
        "6. Data in Oct and Jan but not in Nov and Dec - empty result for Nov",
        new Dictionary<string, bool> { { Oct1, true }, { Jan1, true } },
        Nov1,
        Dec1,
        false
      ),
      new EnergyCardScenario(
        "7. Data in Nov and Dec - query Nov only returns for Nov boundaries",
        new Dictionary<string, bool> { { Nov1, true }, { Dec1, true } },
        Nov1,
        Dec1,
        true
      ),
      new EnergyCardScenario(
        "8. Data in Nov only - pending month query, does not have right anchor - empty result as it falls under blackout",
        new Dictionary<string, bool> { { Nov1, true } },
        Nov1,
        Dec1,
        false
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(Scenarios))]
  public async Task Returns_Correct_Energy_Card_Boundaries(
    EnergyCardScenario scenario,
    CancellationToken cancellationToken
  )
  {
    var fromDate = ParseDate(scenario.QueryFrom);
    var toDate = ParseDate(scenario.QueryTo);

    var infrastructure = await Infrastructure.Create(
      cancellationToken,
      x => x.WithMeterType(typeof(AbbB2xMeterEntity))
    );

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
        x =>
          x.WithInterval(IntervalEntity.QuarterHour)
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
      .GetRequiredService<ReportQueries>()
      .ReadEnergyCardReportBasisByNetworkUser(
        infrastructure.NetworkUser.Id,
        fromDate,
        toDate,
        cancellationToken
      );

    result
      .Should()
      .NotBeNull($"Scenario '{scenario.Name}': query should not return null");

    if (!scenario.ExpectResult)
    {
      result
        .Should()
        .BeEmpty(
          $"Scenario '{scenario.Name}': expected no energy card basis entries"
        );
      return;
    }

    result
      .Should()
      .HaveCount(
        1,
        $"Scenario '{scenario.Name}': expected exactly one energy card basis"
      );

    var basis = result!.First();

    var inWindowAggregates = allMeasurements
      .OfType<AggregateEntity>()
      .Where(x => x.Interval == IntervalEntity.QuarterHour)
      .Where(x => x.Timestamp >= fromDate && x.Timestamp <= toDate)
      .OrderBy(x => x.Timestamp)
      .ToList();

    inWindowAggregates
      .Should()
      .NotBeEmpty(
        $"Scenario '{scenario.Name}': test data must have in-window aggregates"
      );

    var expectedMinTimestamp = inWindowAggregates.First().Timestamp;
    var expectedMaxTimestamp = inWindowAggregates.Last().Timestamp;

    basis
      .MinAggregate.Timestamp.Should()
      .Be(
        expectedMinTimestamp,
        $"Scenario '{scenario.Name}': MinAggregate should be the first "
          + "aggregate in the queried window"
      );

    basis
      .MaxAggregate.Timestamp.Should()
      .Be(
        expectedMaxTimestamp,
        $"Scenario '{scenario.Name}': MaxAggregate should be the last "
          + "aggregate in the queried window"
      );

    basis
      .MinAggregate.Timestamp.Should()
      .NotBe(
        basis.MaxAggregate.Timestamp,
        $"Scenario '{scenario.Name}': MinAggregate and MaxAggregate "
          + "should be different aggregates for a valid energy card"
      );

    basis
      .MinAggregate.MeasurementLocationId.Should()
      .Be(
        infrastructure.MeasurementLocation.Id,
        $"Scenario '{scenario.Name}': MinAggregate must belong to "
          + "the queried measurement location"
      );

    basis
      .MaxAggregate.MeasurementLocationId.Should()
      .Be(
        infrastructure.MeasurementLocation.Id,
        $"Scenario '{scenario.Name}': MaxAggregate must belong to "
          + "the queried measurement location"
      );

    basis
      .MinAggregate.Timestamp.Should()
      .BeOnOrAfter(
        fromDate,
        $"Scenario '{scenario.Name}': MinAggregate must not precede fromDate"
      );

    basis
      .MaxAggregate.Timestamp.Should()
      .BeOnOrBefore(
        toDate,
        $"Scenario '{scenario.Name}': MaxAggregate must be on or before toDate"
      );
  }

  private static DateTimeOffset ParseDate(string isoString)
  {
    return new DateTimeOffset(
      DateTime.SpecifyKind(
        DateTimeOffset
          .Parse(isoString, CultureInfo.InvariantCulture)
          .UtcDateTime,
        DateTimeKind.Utc
      ),
      TimeSpan.Zero
    );
  }

  public record EnergyCardScenario(
    string Name,
    Dictionary<string, bool> MeasurementsMap,
    string QueryFrom,
    string QueryTo,
    bool ExpectResult
  );
}
