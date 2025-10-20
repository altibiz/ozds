using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Streaming;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NotificationsPage : OzdsComponentBase
{
  private bool seen;

  private Table<INotification>? table;

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  public NotificationsState NotificationsState { get; set; } = default!;

  private Task<PaginatedList<INotification>> OnSeenPageAsync(
    string search,
    int page,
    int pageCount
  )
  {
    var queries = ScopedServices
      .GetRequiredService<NotificationQueries>();
    return queries.ReadForRecipient<INotification>(
      RepresentativeState.Representative.Id,
      page,
      CancellationToken,
      true,
      search,
      pageCount);
  }

  private async Task OnSeenChanged()
  {
    seen = !seen;

    if (table is not null)
    {
      await table.Fetch();
    }
  }
}
