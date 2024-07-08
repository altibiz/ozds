using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Base;

// TODO: check if user is representative

namespace Ozds.Business.Interceptors;

public class InvoiceIssuingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
  {
    IssueInvoices(eventData);
    return base.SavingChanges(eventData, result);
  }

  private void IssueInvoices(DbContextEventData eventData)
  {
    if (eventData.Context is null)
    {
      return;
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
