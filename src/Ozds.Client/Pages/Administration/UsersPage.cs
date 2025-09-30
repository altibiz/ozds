using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class UsersPage : OzdsComponentBase
{
  private bool deleted;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<PaginatedList<MaybeRepresentingUserModel>> OnPageAsync(
    string search,
    int page,
    int pageCount
  )
  {
    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var users = await queries.ReadMaybeRepresentingUsers(
      page,
      CancellationToken,
      pageCount,
      deleted,
      search);

    return users;
  }
}
