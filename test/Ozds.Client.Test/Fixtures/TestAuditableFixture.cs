using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations;
using Ozds.Client.Test.Containers;
using Ozds.Client.Test.Faking;

namespace Ozds.Client.Test.Fixtures;

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

    var id = await mutations.Create(auditable, cancellationToken);

    if (auditable is IdentifiableModel identifiable)
    {
      identifiable.Id = id;
    }

    return auditable;
  }
}
