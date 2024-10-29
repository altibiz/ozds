using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Extensions;

namespace Ozds.Data.Test.Fixtures;

public sealed class DataDbContextFixture(
  IDbContextFactory<DataDbContext> factory
) : IAsyncDisposable
{
  public Lazy<Task<DataDbContext>> Context { get; } = new(async () =>
  {
    var context = await factory.CreateDbContextAsync();
    await context.Database.BeginTransactionAsync();

    foreach (var entity in context.Model.GetEntityTypes())
    {
#pragma warning disable EF1002 // Risk of vulnerability to SQL injection.
      await context.Database.ExecuteSqlRawAsync($@"
        DELETE FROM {entity.GetTableName()} WHERE TRUE;
      ");
#pragma warning restore EF1002 // Risk of vulnerability to SQL injection.
    }

    return context;
  });

  public async ValueTask DisposeAsync()
  {
    if (!Context.IsValueCreated)
    {
      return;
    }

    var context = await Context.Value;
    await context.Database.RollbackTransactionAsync();
    await context.DisposeAsync();
  }
}
