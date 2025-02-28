using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class NotificationsStateProvider : OzdsComponentBase
{
  private string? _previousRepresentativeId;

  private NotificationsState _state = new(new List<INotification>());

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [Inject]
  private INotificationRecipientCreatedSubscriber NotificationCreatedSubscriber
  {
    get;
    set;
  } = default!;

  [Inject]
  private IDataModelsChangedSubscriber DataModelsChangedSubscriber
  {
    get;
    set;
  } = default!;

  protected override void OnInitialized()
  {
    NotificationCreatedSubscriber.Subscribe(OnNotificationRecipientCreated);
    DataModelsChangedSubscriber.Subscribe(OnDataModelsChanged);
  }

  protected override void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
      DataModelsChangedSubscriber.Unsubscribe(OnDataModelsChanged);
      NotificationCreatedSubscriber.Unsubscribe(OnNotificationRecipientCreated);
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

  private void OnNotificationRecipientCreated(
    object? sender,
    NotificationRecipientCreatedEventArgs args
  )
  {
    if (args.Recipient.RepresentativeId
      != RepresentativeState.Representative.Id)
    {
      return;
    }

    InvokeAsync(
      () =>
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

    var actualNotificationsMarkedAsSeen = _state.Notifications
      .Where(
        x => notificationsMarkedAsSeen
          .Exists(
            y =>
              y.NotificationId == x.Id
              && y.RepresentativeId == RepresentativeState.Representative.Id))
      .ToList();

    var indexedNotificationsMarkedAsResolved = args.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<IResolvableNotification>()
      .Where(x => x.ResolvedOn is not null)
      .Select(
        x => new
        {
          Notification = x,
          Index = _state.Notifications.FindIndex(y => y.Id == x.Id)
        })
      .Where(x => x.Index is not -1)
      .ToList();

    if (actualNotificationsMarkedAsSeen.Count > 0 ||
      indexedNotificationsMarkedAsResolved.Count > 0)
    {
      InvokeAsync(
        () =>
        {
          foreach (var notification in actualNotificationsMarkedAsSeen)
          {
            _state.Notifications.Remove(notification);
          }

          foreach (var x in indexedNotificationsMarkedAsResolved)
          {
            _state.Notifications[x.Index] = x.Notification;
          }

          StateHasChanged();
        });
    }
  }
}
