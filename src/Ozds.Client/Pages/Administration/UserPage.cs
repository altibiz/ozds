using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Dialogs;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class UserPage
  : OzdsIdentifiableModelPageComponentBase<RepresentativeModel>
{
  private PasswordModel? password;

  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  private UserState UserState { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  [Inject]
  private IHostEnvironment HostEnvironment { get; set; } = default!;

  protected override void OnInitialized()
  {
    base.OnInitialized();

    if (
      Id is not null
      && RepresentativeState.Representative.Role
        is RoleModel.OperatorRepresentative
    )
    {
      var activator = ScopedServices.GetRequiredService<ModelActivator>();
      password = activator.Activate<PasswordModel>();
      password.UserId = Id;
    }
  }

  private async Task OnUserCreateAsync(MaybeRepresentingUserModel model)
  {
    var userMutations = ScopedServices.GetRequiredService<UserMutations>();

    var representativeMutations =
      ScopedServices.GetRequiredService<TrackableMutations>();

    var newId = await userMutations.Create(model.User, CancellationToken);

    if (model.NewPassword is { } newPwd && newId is { })
    {
      newPwd.UserId = newId;
      var passwordMutations =
        ScopedServices.GetRequiredService<PasswordMutations>();
      await passwordMutations.Create(newPwd, CancellationToken);
    }

    if (model.Representative is { } representative && newId is { })
    {
      representative.Id = newId;
      await representativeMutations.Create(
        model.Representative,
        CancellationToken
      );
    }
  }

  private async Task<MaybeRepresentingUserModel?> OnUserLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    if (
      RepresentativeState.Representative.Role
      is not RoleModel.OperatorRepresentative
    )
    {
      if (RepresentativeState.Representative.Id != Id)
      {
        return null;
      }

      return new MaybeRepresentingUserModel
      {
        User = UserState.User,
        Representative = RepresentativeState.Representative,
      };
    }

    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var user = await queries.ReadMaybeRepresentingUserByUserId(
      Id,
      CancellationToken
    );

    return user;
  }

  private async Task OnUserUpdateAsync(MaybeRepresentingUserModel model)
  {
    if (model.Representative is null)
    {
      return;
    }

    var representativeMutations =
      ScopedServices.GetRequiredService<TrackableMutations>();
    var userMutations = ScopedServices.GetRequiredService<UserMutations>();

    await representativeMutations.Update(
      model.Representative,
      CancellationToken
    );
    await userMutations.Update(model.User, CancellationToken);
  }

  private async Task OnUserDeleteAsync(MaybeRepresentingUserModel model)
  {
    if (model.Representative is null)
    {
      return;
    }

    var representativeMutations =
      ScopedServices.GetRequiredService<TrackableMutations>();
    var userMutations = ScopedServices.GetRequiredService<UserMutations>();

    await representativeMutations.Delete(
      model.Representative,
      CancellationToken
    );
    await userMutations.Delete(model.User.Id, CancellationToken);
  }

  private async Task OnPasswordUpdateAsync()
  {
    if (password is null)
    {
      return;
    }

    var passwordMutations =
      ScopedServices.GetRequiredService<PasswordMutations>();

    await passwordMutations.Update(password, CancellationToken);

    var message = HostEnvironment.IsDevelopment()
      ? Translate("Successfully updated password to")
        + " "
        + password.NewPassword
      : Translate("Successfully updated password");

    if (Id == UserState.User.Id)
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Success"),
        new DialogParameters
        {
          {
            nameof(MutatingResult.Body),
            message
              + "\n"
              + Translate("You will be logged out when this dialog is closed.")
          },
          {
            nameof(MutatingResult.NavigationBehavior),
            MutatingResultNavigationBehavior.Logout
          },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
    }
    else
    {
      await DialogService.ShowAsync<MutatingResult>(
        Translate("Success"),
        new DialogParameters
        {
          { nameof(MutatingResult.Body), message },
          { nameof(MutatingResult.NavigationBehavior), null },
        },
        new DialogOptions { CloseOnEscapeKey = true }
      );
    }
  }

  private async Task<PaginatedList<LocationModel>> OnLocationsPageAsync(
    string search,
    int pageNumber,
    int pageCount
  )
  {
    if (Id is null)
    {
      return PaginatedList<LocationModel>.Empty;
    }

    var queries = ScopedServices.GetRequiredService<LocationQueries>();

    var locations = await queries.ReadByRepresentativeId(
      Id,
      pageNumber,
      CancellationToken,
      pageCount,
      false,
      search
    );

    return locations;
  }

  private async Task<PaginatedList<NetworkUserModel>> OnNetworkUsersPageAsync(
    string search,
    int pageNumber,
    int pageCount
  )
  {
    if (Id is null)
    {
      return PaginatedList<NetworkUserModel>.Empty;
    }

    var queries = ScopedServices.GetRequiredService<NetworkUserQueries>();

    var networkUsers = await queries.ReadByRepresentativeId(
      Id,
      pageNumber,
      CancellationToken,
      QueryConstants.DefaultPageCount,
      false,
      search
    );

    return networkUsers;
  }

  private async Task<PaginatedList<ApiKeyModel>> OnApiKeysPageAsync(
    string search,
    int pageNumber,
    int pageCount
  )
  {
    if (Id is null)
    {
      return PaginatedList<ApiKeyModel>.Empty;
    }

    var queries = ScopedServices.GetRequiredService<ApiKeyQueries>();

    var apiKeys = await queries.ReadByRepresentativeId(
      Id,
      pageNumber,
      CancellationToken,
      pageCount,
      false,
      search
    );

    return apiKeys;
  }
}
