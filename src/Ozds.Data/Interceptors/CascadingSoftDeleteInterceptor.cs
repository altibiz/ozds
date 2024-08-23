using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ozds.Data.Interceptors;

public class CascadingSoftDeleteInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
  {
    AddCascadingSoftDeletes(eventData);
    return base.SavingChanges(eventData, result);
  }

  private static void AddCascadingSoftDeletes(DbContextEventData _)
  {
    // TODO: implement cascading soft delete
  }
}
