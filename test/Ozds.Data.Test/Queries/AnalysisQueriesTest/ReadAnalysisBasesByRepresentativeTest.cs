using Ozds.Data.Entities.Enums;
using Ozds.Data.Queries;

namespace Ozds.Data.Test.AnalysisQueriesTest;

public class ReadAnalysisBasesByRepresentativeTest
{
  [Fact]
  public async Task IsValidForOperator()
  {
    var serviceCollection = new ServiceCollection();
    var serviceProvider = serviceCollection.BuildServiceProvider();

    var queries = ActivatorUtilities
      .CreateInstance<AnalysisQueries>(serviceProvider);

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
