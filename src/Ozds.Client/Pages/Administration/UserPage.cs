using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class UserPage : OzdsOwningComponentBase
{
  [Parameter]
  public string? Id { get; set; } = default!;

  [CascadingParameter]
  private UserState UserState { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<MaybeRepresentingUserModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    if (RepresentativeState.Representative.Role
      is not RoleModel.OperatorRepresentative)
    {
      if (RepresentativeState.Representative.Id != Id)
      {
        return null;
      }

      return new MaybeRepresentingUserModel
      {
        User = UserState.User,
        Representative = RepresentativeState.Representative
      };
    }

    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var user = await queries.ReadMaybeRepresentingUserByUserId(
      Id,
      CancellationToken
    );

    return user;
  }

  private async Task OnUpdateAsync(MaybeRepresentingUserModel model)
  {
    if (model.Representative is null)
    {
      return;
    }

    var mutations = ScopedServices.GetRequiredService<AuditableMutations>();

    await mutations.Update(model.Representative, CancellationToken);
  }

  private async Task OnDeleteAsync(MaybeRepresentingUserModel model)
  {
    if (model.Representative is null)
    {
      return;
    }

    var mutations = ScopedServices.GetRequiredService<AuditableMutations>();

    await mutations.Delete(model.Representative, CancellationToken);
  }
}
