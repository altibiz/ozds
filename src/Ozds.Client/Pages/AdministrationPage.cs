using Microsoft.AspNetCore.Components;
using Ozds.Business.Analysis;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class AdministrationPage : OzdsComponentBase
{
  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  [Inject]
  private Analyzer Analyzer { get; set; } = default!;

  private async Task<PaginatedList<MaybeRepresentingUserModel>>
    OnUserPageAsync(string search, int page, int pageCount)
  {
    var queries = ScopedServices.GetRequiredService<RepresentativeQueries>();

    var users = await queries.ReadMaybeRepresentingUsers(
      page,
      CancellationToken,
      pageCount,
      search: search
    );

    return users;
  }
}
