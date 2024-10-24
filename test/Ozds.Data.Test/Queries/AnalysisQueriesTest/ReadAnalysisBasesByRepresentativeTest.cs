using Ozds.Data.Entities.Enums;
using Ozds.Data.Queries;

namespace Ozds.Data.Test.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest(
  AnalysisQueries queries
)
{
  [Fact]
  public async Task IsValidForOperator()
  {
    var result = await queries.ReadAnalysisBasesByRepresentative(
      "id",
      RoleEntity.OperatorRepresentative,
      DateTimeOffset.MinValue,
      DateTimeOffset.MaxValue,
      CancellationToken.None
    );

    Assert.NotNull(result);
  }
}
