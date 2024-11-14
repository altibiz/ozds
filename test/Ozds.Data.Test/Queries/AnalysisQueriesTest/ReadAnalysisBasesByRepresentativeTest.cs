using Ozds.Data.Entities.Composite;
using Ozds.Data.Queries;
using Ozds.Data.Test.Context;
using Ozds.Data.Test.Extensions;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(IServiceProvider services)
{
  [Fact]
  public async Task IsValidTest()
  {
    var results = await Task.WhenAll(Enumerable
      .Range(1, Constants.DefaultRepeatCount)
      .Select(async i =>
      {
        await using var manager = services
          .GetRequiredService<EphemeralDataDbContextManager>();
        var context = await manager.GetContext();
        var factory = new AnalysisBasisEntityFactory(context);

        var representativeId = i.ToString();
        var dateTo = DateTime.UtcNow;
        var dateFrom = dateTo.AddMonths(-i);

        var expected = await factory.Create(
          representativeId,
          dateFrom,
          dateTo
        );

        var representative = expected.First().Representative;
        var fromDate = expected.First().FromDate;
        var toDate = expected.First().ToDate;

        var queries = services.GetRequiredService<AnalysisQueries>();
        var actual = await queries.ReadAnalysisBasesByRepresentative(
          context,
          representative.Id,
          representative.Role,
          fromDate,
          toDate,
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

        return new TestResult<List<AnalysisBasisEntity>>(expected, actual);
      })
    );

    await using var manager = services
      .GetRequiredService<DataDbContextManager>();
    var context = await manager.GetContext();

    results.Should().AllSatisfy(result =>
    {
      result.Actual.Should().HaveCount(result.Actual.Count);

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
      });
    });
  }
}
