using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class UsersPage : OzdsOwningComponentBase
{
  private async Task<PaginatedList<MaybeRepresentingUserModel>> OnPageAsync(
    int page
  )
  {
    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var users = await queries.ReadMaybeRepresentingUsers(page, CancellationToken);

    return users;
  }
}
