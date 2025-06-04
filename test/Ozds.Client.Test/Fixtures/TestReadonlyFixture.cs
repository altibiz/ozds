using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Client.Test.Containers;
using Ozds.Client.Test.Faking;

namespace Ozds.Client.Test.Fixtures;

public class TestReadonlyFixture(
  ServiceComposition composition
)
{
  public async Task<T> Create<T>(
    CancellationToken cancellationToken,
    Action<T>? configure = null
  )
    where T : IReadonly
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<ReadonlyMutations>();
    var faker = scope.ServiceProvider
      .GetRequiredService<ModelFaker>();

    var @readonly = faker.Fake<T>();

    if (configure is not null)
    {
      configure(@readonly);
    }

    await mutations.Create(@readonly, cancellationToken);

    return @readonly;
  }
}
