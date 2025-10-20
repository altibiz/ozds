using Ozds.Business.Models;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class DataNetworkUserChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataNetworkUserChangeHandler>(serviceProvider)
{
}

public class DataNetworkUserChangeHandler(
  IBillingJobManager manager,
  TrackableQueries trackableQueries
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 0;
    var networkUsers = await trackableQueries
      .Read<NetworkUserModel>(page, cancellationToken);
    while (networkUsers.Items.Count > 0)
    {
      await manager.EnsureMonthlyBillingJobs(
        networkUsers.Items.Select(x => x.Id),
        cancellationToken);

      networkUsers = await trackableQueries
        .Read<NetworkUserModel>(++page, cancellationToken);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var added = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<NetworkUserModel>()
      .ToList();
    if (added.Count > 0)
    {
      await manager.EnsureMonthlyBillingJobs(
        added.Select(x => x.Id),
        cancellationToken);
    }

    var modified = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<NetworkUserModel>()
      .ToList();
    if (modified.Count > 0)
    {
      await manager.RescheduleMonthlyBillingJobs(
        modified.Select(x => x.Id),
        cancellationToken);
    }

    var removed = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Removed)
      .Select(x => x.Model)
      .OfType<NetworkUserModel>()
      .ToList();
    if (removed.Count > 0)
    {
      await manager.UnscheduleMonthlyBillingJobs(
        removed.Select(x => x.Id),
        cancellationToken);
    }
  }
}
