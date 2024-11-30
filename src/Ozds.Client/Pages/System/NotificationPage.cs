using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NotificationPage : OzdsOwningComponentBase
{
  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task OnMarkAsSeen(INotification model)
  {
    await ScopedServices
      .GetRequiredService<NotificationMutations>()
      .MarkNotificationAsSeen(
        model.Id,
        RepresentativeState.Representative.Id,
        CancellationToken.None);

    NavigateToPage<NotificationsPage>();
  }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  private async Task<INotification> OnNewAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    return ScopedServices
      .GetRequiredService<AgnosticModelActivator>()
      .Activate<SystemNotificationModel>();
  }

  private async Task<INotification?> OnLoadAsync()
  {
    return Id is null
      ? null
      : await ScopedServices
          .GetRequiredService<NotificationQueries>()
          .ReadSingle<INotification>(Id, CancellationToken.None);
  }

  private async Task OnCreateAsync(INotification model)
  {
    await ScopedServices
      .GetRequiredService<NotificationMutations>()
      .Create(model, CancellationToken.None);

    NavigateToPage<NotificationsPage>();
  }
}
