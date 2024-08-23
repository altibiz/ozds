using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Base;

// TODO: check if user is representative

namespace Ozds.Data.Interceptors;

public class InvoiceIssuingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    if (eventData.Context is null)
    {
      return base.SavingChanges(eventData, result);
    }

    var representativeId = GetRepresentativeId();
    var now = DateTimeOffset.UtcNow;

    var entries = eventData.Context.ChangeTracker
      .Entries<InvoiceEntity>()
      .ToList();

    foreach (var entity in entries
      .Where(e => e.State is EntityState.Added)
      .Select(e => e.Entity))
    {
      entity.IssuedOn = now;
      entity.IssuedById = representativeId;
    }

    return base.SavingChanges(eventData, result);
  }

  private string? GetRepresentativeId()
  {
    if (serviceProvider.GetService<IHttpContextAccessor>() is not null)
    {
      return null;
    }

    return null;
  }
}
