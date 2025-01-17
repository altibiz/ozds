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
  AuditableQueries auditableQueries
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 1;
    var networkUsers = await auditableQueries
      .Read<NetworkUserModel>(page, cancellationToken);
    while (networkUsers.Items.Count > 0)
    {
      foreach (var networkUser in networkUsers.Items)
      {
        await manager.EnsureMonthlyBillingJob(
          networkUser.Id,
          cancellationToken);
      }

      networkUsers = await auditableQueries
        .Read<NetworkUserModel>(++page, cancellationToken);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not NetworkUserModel networkUser)
      {
        continue;
      }

      if (entry.State is DataModelChangedState.Added)
      {
        await manager.EnsureMonthlyBillingJob(
          networkUser.Id,
          cancellationToken);
      }

      if (entry.State is DataModelChangedState.Removed)
      {
        await manager.UnscheduleMonthlyBillingJob(
          networkUser.Id,
          cancellationToken);
      }

      if (entry.State is DataModelChangedState.Modified)
      {
        await manager.RescheduleMonthlyBillingJob(
          networkUser.Id,
          cancellationToken);
      }
    }
  }
}
