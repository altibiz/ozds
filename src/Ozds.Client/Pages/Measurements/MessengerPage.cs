using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class MessengerPage
  : OzdsIdentifiableModelPageComponentBase<IMessenger>
{
  [Parameter]
  public string Id { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

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

    var apiKeys = await queries.ReadByMessengerId(
      Id,
      pageNumber,
      CancellationToken.None,
      pageCount,
      false,
      search
    );

    return apiKeys;
  }
}
