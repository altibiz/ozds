using Ozds.Business.Models.Abstractions;
using Ozds.Business.Mutations;
using Ozds.Fake.Faking;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public class TestAuditableFixture(
  ServiceComposition composition
)
{
  public async Task<T> Create<T>(
    CancellationToken cancellationToken,
    Action<T>? configure = null
  )
    where T : IAuditable
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<AuditableMutations>();
    var faker = scope.ServiceProvider
      .GetRequiredService<ModelFaker>();

    var auditable = faker.Fake<T>();

    if (configure is not null)
    {
      configure(auditable);
    }

    await mutations.Create(auditable, cancellationToken);

    return auditable;
  }

  public async Task<object> Create(
    Type type,
    CancellationToken cancellationToken,
    Action<object>? configure = null
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<AuditableMutations>();
    var faker = scope.ServiceProvider
      .GetRequiredService<ModelFaker>();

    if (faker.FakeDynamic(type) is not IAuditable auditable)
    {
      throw new InvalidOperationException(
        $"Cannot create auditable of type {type}"
      );
    }

    if (configure is not null)
    {
      configure(auditable);
    }

    await mutations.Create(auditable, cancellationToken);

    return auditable;
  }
}
