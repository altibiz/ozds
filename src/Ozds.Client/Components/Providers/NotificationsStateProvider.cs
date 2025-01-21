using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
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

  [Inject]
  private INotificationCreatedSubscriber NotificationCreatedSubscriber
  { get; set; } = default!;

  [Inject]
  private IDataModelsChangedSubscriber DataModelsChangedSubscriber
  { get; set; } = default!;

  private string? _previousRepresentativeId;

  private NotificationsState _state = new(new());

  protected override void OnInitialized()
  {
    base.OnInitialized();
    NotificationCreatedSubscriber.Subscribe(OnNotificationCreated);
    DataModelsChangedSubscriber.Subscribe(OnDataModelsChanged);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      DataModelsChangedSubscriber.Unsubscribe(OnDataModelsChanged);
      NotificationCreatedSubscriber.Unsubscribe(OnNotificationCreated);
    }
    base.Dispose(disposing);
  }

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

    var state = new NotificationsState(notifications);

    _state = state;
  }

  private void OnNotificationCreated(
    object? sender,
    NotificationCreatedEventArgs args
  )
  {
    InvokeAsync(() =>
    {
      _state.Notifications.Add(args.Notification);
      StateHasChanged();
    });
  }

  private void OnDataModelsChanged(
    object? sender,
    DataModelsChangedEventArgs args
  )
  {
    var notificationsMarkedAsSeen = args.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<NotificationRecipientModel>()
      .Where(x => x.SeenOn is not null)
      .ToList();

    var relevantNotifications = _state.Notifications
      .Where(x => notificationsMarkedAsSeen
        .Exists(y =>
          y.NotificationId == x.Id
          && y.RepresentativeId == RepresentativeState.Representative.Id))
      .ToList();

    if (relevantNotifications.Count > 0)
    {
      InvokeAsync(() =>
      {
        foreach (var notification in relevantNotifications)
        {
          _state.Notifications.Remove(notification);
        }
        StateHasChanged();
      });
    }
  }
}
