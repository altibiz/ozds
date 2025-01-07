using Ozds.Business.Models.Composite;
using Ozds.Data.Entities.Composite;
using Ozds.Data.Queries;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(IServiceProvider services)
{
  [Theory]
  [InlineData("1")]
  public async Task IsValidTest(string representativeId)
  {
    await using var manager = services
      .GetRequiredService<EphemeralDataDbContextManager>();
    var context = await manager.GetContext();
    var factory = new AnalysisBasisEntityFactory(context);

    var dateTo = DateTime.UtcNow;
    var dateFrom = dateTo.AddMonths(-1);

    var noise = await factory.Create(
      representativeId,
      dateFrom,
      dateTo,
      CancellationToken.None
    );
    var expected = noise.Take(1).ToList();

    var representative = expected.First().Representative;
    var location = expected.First().Location;
    var fromDate = expected.First().FromDate;
    var toDate = expected.First().ToDate;

    var queries = services.GetRequiredService<AnalysisQueries>();
    var actual = await queries.ReadAnalysisBasesByRepresentative(
      context,
      representative.Id,
      representative.Role,
      fromDate,
      toDate,
      location.Id,
      CancellationToken.None
    );

    expected = expected
      .Select(x => x with
      {
        Calculations = x.Calculations.OrderBy(y => y.Id).ToList(),
        Invoices = x.Invoices.OrderBy(y => y.Id).ToList()
      })
      .OrderBy(x => x.MeasurementLocation.Id)
      .ToList();
    actual = actual
      .Select(x => x with
      {
        Calculations = x.Calculations.OrderBy(y => y.Id).ToList(),
        Invoices = x.Invoices.OrderBy(y => y.Id).ToList()
      })
      .OrderBy(x => x.MeasurementLocation.Id)
      .ToList();

    var result = new TestResult<List<AnalysisBasisEntity>>(expected, actual);

    result.Actual.Should().HaveCount(result.Expected.Count);

    result.Actual.Should().AllSatisfy(actual =>
    {
      var expected = result.Expected.FirstOrDefault(expected =>
        expected.MeasurementLocation.Id == actual.MeasurementLocation.Id)!;

      expected.Should().NotBeNull();

      actual.Representative
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Representative);

      actual.FromDate.Should().BeExactly(expected.FromDate);
      actual.ToDate.Should().BeExactly(expected.ToDate);

      actual.Location
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Location);

      actual.NetworkUser
        .Should()
        .BeContextuallyEquivalentTo(context, expected.NetworkUser);

      actual.MeasurementLocation
        .Should()
        .BeContextuallyEquivalentTo(context, expected.MeasurementLocation);

      actual.Meter
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Meter);

      actual.Calculations
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Calculations);

      actual.Invoices
        .Should()
        .BeContextuallyEquivalentTo(context, expected.Invoices);

      actual.LastMeasurement
        .Should()
        .BeContextuallyEquivalentTo(context, expected.LastMeasurement);

      actual.MonthlyAggregates
        .Should()
        .BeContextuallyEquivalentTo(context, expected.MonthlyAggregates);

      var actualModel = actual.ToModel();
      var expectedModel = expected.ToModel();

      actualModel.Should().BeBusinesswiseEquivalentTo(expectedModel);
    });
  }
}
