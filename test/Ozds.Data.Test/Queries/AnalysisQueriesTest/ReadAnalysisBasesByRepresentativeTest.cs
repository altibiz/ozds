using Ozds.Data.Queries;
using Ozds.Data.Test.Context;

namespace Ozds.Data.Test.Queries.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(
  DataDbContextManager manager,
  AnalysisQueries queries
)
{
  public record TheoryDataRecord(
    string RepresentativeId,
    DateTimeOffset DateFrom,
    DateTimeOffset DateTo
  );

  public static readonly TheoryData<TheoryDataRecord> TestData =
    new(
      Enumerable.Range(1, Constants.DefaultFuzzCount)
        .Select(i => new TheoryDataRecord(
          i.ToString(),
          DateTimeOffset.UtcNow.AddMonths(-i),
          DateTimeOffset.UtcNow
        ))
    );

  [Theory]
  [MemberData(nameof(TestData))]
  public async Task IsValid(TheoryDataRecord data)
  {
    var context = await manager.Context.Value;

    var factory = new AnalysisBasisEntityFactory(context);

    var analysisBases = await factory.Create(
      data.RepresentativeId,
      data.DateFrom,
      data.DateTo
    );

    var representative = analysisBases.First().Representative;
    var fromDate = analysisBases.First().FromDate;
    var toDate = analysisBases.First().ToDate;

    var result = await queries.ReadAnalysisBasesByRepresentative(
      context,
      representative.Id,
      representative.Role,
      fromDate,
      toDate,
      CancellationToken.None
    );

    Assert.Equal(analysisBases, result);
  }
}
