using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Interceptors;

public class InvoiceIssuingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    if (eventData.Context is null)
    {
      return await base.SavingChangesAsync(
        eventData, result, cancellationToken);
    }

    var representativeId = await GetRepresentativeId(eventData);
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

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private async Task<string?> GetRepresentativeId(DbContextEventData eventData)
  {
    ClaimsPrincipal? claimsPrincipal = null;
    if (serviceProvider
        .GetService<IHttpContextAccessor>()
        ?.HttpContext is { } httpContext)
    {
      claimsPrincipal = httpContext.User;
    }

    if (claimsPrincipal is null
      && serviceProvider.GetService<AuthenticationStateProvider>()
        is { } authStateProvider)
    {
      claimsPrincipal =
        (await authStateProvider.GetAuthenticationStateAsync()).User;
    }

    if (claimsPrincipal is null)
    {
      return null;
    }

    var id = claimsPrincipal.Claims
      .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(id))
    {
      return null;
    }

    var context = eventData.Context as OzdsDataDbContext;
    if (context is null)
    {
      return null;
    }

    var representative = await context.Representatives
      .Where(context.PrimaryKeyEquals<RepresentativeEntity>(id))
      .FirstOrDefaultAsync();

    return representative?.Id;
  }
}
