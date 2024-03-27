using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Business.Interceptors;

public class CascadingSoftDeleteInterceptor : ServedSaveChangesInterceptor
{
  public CascadingSoftDeleteInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData, InterceptionResult<int> result)
  {
    AddCascadingSoftDeletes(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData, InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    AddCascadingSoftDeletes(eventData);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void AddCascadingSoftDeletes(DbContextEventData eventData)
  {
  }
}
