using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Interceptors;

public class InvoiceIssuingInterceptor : ServedSaveChangesInterceptor
{
  public InvoiceIssuingInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider) { }

  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    IssueInvoices(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    IssueInvoices(eventData);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
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
    if (_serviceProvider.GetService<IHttpContextAccessor>() is { } httpContextAccessor)
    {
      return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    return null;
  }
}
