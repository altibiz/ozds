using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Base;

// TODO: check if user is representative

namespace Ozds.Business.Interceptors;

public class InvoiceIssuingInterceptor : ServedSaveChangesInterceptor
{
  public InvoiceIssuingInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData, InterceptionResult<int> result)
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

    foreach (var invoice in entries)
    {
      if (invoice.State is EntityState.Added)
      {
        invoice.Entity.IssuedOn = now;
        invoice.Entity.IssuedById = representativeId;
      }
    }
  }

  private string? GetRepresentativeId()
  {
    if (_serviceProvider.GetService<IHttpContextAccessor>() is not null)
    {
      return null;
      // return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    return null;
  }
}
