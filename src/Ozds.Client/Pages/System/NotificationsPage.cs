using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NotificationsPage : OzdsOwningComponentBase
{
  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  public NotificationsState NotificationsState { get; set; } = default!;

  private bool _seen = false;

  private Task<PaginatedList<INotification>> OnPageAsync(int page)
  {
    return _seen
      ? ScopedServices
          .GetRequiredService<NotificationQueries>()
          .ReadForRecipient<INotification>(
            RepresentativeState.Representative.Id,
            page,
            CancellationToken,
            seen: _seen)
      : Task.FromResult(NotificationsState.Notifications
          .ToPaginated(NotificationsState.Notifications.Count));
  }
}
