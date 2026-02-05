using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Data.Interceptors;

public abstract class StatefulInterceptor<T>(IServiceProvider serviceProvider)
  : ServedInterceptor(serviceProvider)
  where T : class
{
  private readonly ConditionalWeakTable<DbContext, T> _contextAsyncState =
    new();

  private readonly ConditionalWeakTable<DbContext, T> _contextState = new();

  protected T State(DbContext context)
  {
    if (_contextState.TryGetValue(context, out var state))
    {
      return state;
    }

    throw new InvalidOperationException(
      $"No state found for {context.GetType().Name}."
    );
  }

  protected T AsyncState(DbContext context)
  {
    if (_contextAsyncState.TryGetValue(context, out var state))
    {
      return state;
    }

    throw new InvalidOperationException(
      $"No state found for {context.GetType().Name}."
    );
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavingChanges(eventData, result);
    }

    var entries = ProcessSavingChanges(context);
    _contextState.AddOrUpdate(context, entries);

    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return await base.SavingChangesAsync(
        eventData,
        result,
        cancellationToken
      );
    }

    var entries = ProcessSavingChanges(context);
    _contextAsyncState.AddOrUpdate(context, entries);

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result
  )
  {
    var context = eventData.Context;
    if (context == null)
    {
      return base.SavedChanges(eventData, result);
    }

    _contextState.Remove(context);

    return base.SavedChanges(eventData, result);
  }

  public override async ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default
  )
  {
    if (eventData.Context is null)
    {
      return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    _contextAsyncState.Remove(eventData.Context);

    return await base.SavedChangesAsync(eventData, result, cancellationToken);
  }

  protected abstract T ProcessSavingChanges(DbContext context);
}
