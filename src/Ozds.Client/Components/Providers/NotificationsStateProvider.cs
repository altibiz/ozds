using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class NotificationsStateProvider : OzdsOwningComponentBase
{
  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private string? _previousRepresentativeId;

  private NotificationsState _state = new(
    new(),
    _ => { },
    _ => { }
  );

  protected override async Task OnParametersSetAsync()
  {
    if (_previousRepresentativeId == RepresentativeState.Representative.Id)
    {
      return;
    }
    _previousRepresentativeId = RepresentativeState.Representative.Id;

    var notificationQueries = ScopedServices
      .GetRequiredService<NotificationQueries>();

    var notifications = await notificationQueries
      .ReadForRecipient<INotification>(
        RepresentativeState.Representative.Id,
        CancellationToken
      );

    var state = new NotificationsState(
      notifications,
      (notification) =>
      {
        InvokeAsync(() =>
        {
          notifications.Add(notification);
          StateHasChanged();
        });
      },
      (notification) =>
      {
        InvokeAsync(() =>
        {
          notifications.Remove(notification);
          StateHasChanged();
        });
      }
    );

    _state = state;
  }
}
