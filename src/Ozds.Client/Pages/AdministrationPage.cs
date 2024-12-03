using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class AdministrationPage : OzdsOwningComponentBase
{
  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  private async Task<PaginatedList<MaybeRepresentingUserModel>>
    OnUserPageAsync(int page)
  {
    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var users = await queries.MaybeRepresentingUsers(
      page,
      CancellationToken.None
    );

    return users;
  }
}