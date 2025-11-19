using Ozds.Caching.Mutations;
using Ozds.Caching.Queries;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  private CompositeEntityMutations? compositeMutations;

  private CompositeEntityQueries? compositeQueries;

  private EntityFactory? entityFactory;

  private EntityMutations? entityMutations;

  private EntityQueries? entityQueries;

  private IdentifiableEntityMutations? identifiableMutations;

  private IdentifiableEntityQueries? identifiableQueries;

  private JoinEntityMutations? joinMutations;

  private JoinEntityQueries? joinQueries;

  protected IServiceProvider Services
  {
    get { return (host ?? throw new InvalidOperationException()).Services; }
  }

  protected CompositeEntityQueries CompositeQueries
  {
    get
    {
      return compositeQueries ??=
        Services.GetRequiredService<CompositeEntityQueries>();
    }
  }

  protected IdentifiableEntityQueries IdentifiableQueries
  {
    get
    {
      return identifiableQueries ??=
        Services.GetRequiredService<IdentifiableEntityQueries>();
    }
  }

  protected EntityQueries EntityQueries
  {
    get
    {
      return entityQueries ??= Services.GetRequiredService<EntityQueries>();
    }
  }

  protected JoinEntityQueries JoinQueries
  {
    get
    {
      return joinQueries ??= Services.GetRequiredService<JoinEntityQueries>();
    }
  }

  protected CompositeEntityMutations CompositeMutations
  {
    get
    {
      return compositeMutations ??=
        Services.GetRequiredService<CompositeEntityMutations>();
    }
  }

  protected IdentifiableEntityMutations IdentifiableMutations
  {
    get
    {
      return identifiableMutations ??=
        Services.GetRequiredService<IdentifiableEntityMutations>();
    }
  }

  protected EntityMutations EntityMutations
  {
    get
    {
      return entityMutations ??= Services.GetRequiredService<EntityMutations>();
    }
  }

  protected JoinEntityMutations JoinMutations
  {
    get
    {
      return joinMutations ??=
        Services.GetRequiredService<JoinEntityMutations>();
    }
  }

  protected EntityFactory EntityFactory
  {
    get { return entityFactory ??= new EntityFactory(); }
  }
}
