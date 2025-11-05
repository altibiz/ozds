using Ozds.Caching.Mutations;
using Ozds.Caching.Queries;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  protected IServiceProvider Services =>
    (host ?? throw new InvalidOperationException()).Services;

  private CompositeEntityQueries? compositeQueries;

  protected CompositeEntityQueries CompositeQueries =>
    compositeQueries ??= Services.GetRequiredService<CompositeEntityQueries>();

  private IdentifiableEntityQueries? identifiableQueries;

  protected IdentifiableEntityQueries IdentifiableQueries =>
    identifiableQueries ??= Services.GetRequiredService<IdentifiableEntityQueries>();

  private EntityQueries? entityQueries;

  protected EntityQueries EntityQueries =>
    entityQueries ??= Services.GetRequiredService<EntityQueries>();

  private JoinEntityQueries? joinQueries;

  protected JoinEntityQueries JoinQueries =>
    joinQueries ??= Services.GetRequiredService<JoinEntityQueries>();

  private CompositeEntityMutations? compositeMutations;

  protected CompositeEntityMutations CompositeMutations =>
    compositeMutations ??= Services.GetRequiredService<CompositeEntityMutations>();

  private IdentifiableEntityMutations? identifiableMutations;

  protected IdentifiableEntityMutations IdentifiableMutations =>
    identifiableMutations ??= Services.GetRequiredService<IdentifiableEntityMutations>();

  private EntityMutations? entityMutations;

  protected EntityMutations EntityMutations =>
    entityMutations ??= Services.GetRequiredService<EntityMutations>();

  private JoinEntityMutations? joinMutations;

  protected JoinEntityMutations JoinMutations =>
    joinMutations ??= Services.GetRequiredService<JoinEntityMutations>();

  private EntityFactory? entityFactory;

  protected EntityFactory EntityFactory =>
    entityFactory ??= new EntityFactory();
}
