using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Fake.Faking;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

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

  public async Task<object> Create(
    Type type,
    CancellationToken cancellationToken,
    Action<object>? configure = null
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<ReadonlyMutations>();
    var faker = scope.ServiceProvider
      .GetRequiredService<ModelFaker>();

    if (faker.FakeDynamic(type) is not IReadonly @readonly)
    {
      throw new InvalidOperationException(
        $"Cannot create readonly of type {type}"
      );
    }

    if (configure is not null)
    {
      configure(@readonly);
    }

    await mutations.Create(@readonly, cancellationToken);

    return @readonly;
  }
}
