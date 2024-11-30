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

  private async Task OnMarkAsSeen(NotificationModel model)
  {
    await ScopedServices
      .GetRequiredService<NotificationMutations>()
      .MarkNotificationAsSeen(
        model.Id,
        RepresentativeState.Representative.Id,
        CancellationToken.None);

    NavigateToPage<NotificationsPage>();
  }

  private async Task<INotification> OnNewAsync()
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
