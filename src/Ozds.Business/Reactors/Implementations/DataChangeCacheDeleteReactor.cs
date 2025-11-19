using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;
using Ozds.Caching.Entities.Abstractions;
using CachingMutations = Ozds.Caching.Mutations.EntityMutations;

namespace Ozds.Business.Reactors.Implementations;

public class DataChangeCacheDeleteReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataChangeCacheDeleteHandler>(serviceProvider)
{
}

public class DataChangeCacheDeleteHandler(
  CachingMutations mutations,
  ModelCachingEntityConverter converter
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var model in eventArgs.Models
      .Where(
        entry => entry.State
          is DataModelChangedState.Removed
          or DataModelChangedState.Modified)
      .Select(entry => entry.Model)
      .OfType<ICached>())
    {
      var entity = converter.ToEntity<IEntity>(model);
      await mutations.Delete(entity, model.CacheId, cancellationToken);
    }
  }
}
