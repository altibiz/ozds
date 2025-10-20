using Microsoft.AspNetCore.Components;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class ApiKeyPage
  : OzdsIdentifiableModelPageComponentBase<ApiKeyModel>
{
  [Parameter]
  public string? Id { get; set; }

  [SupplyParameterFromQuery]
  [Parameter]
  public string? PrincipalModelType { get; set; }

  [SupplyParameterFromQuery]
  [Parameter]
  public string? PrincipalModelId { get; set; }

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  private Task<ApiKeyModel> OnNewAsync()
  {
    var modelActivator = ScopedServices
      .GetRequiredService<ModelActivator>();

    var model = modelActivator.Activate<ApiKeyModel>();

    if (PrincipalModelType is not null && PrincipalModelId is not null)
    {
      model.PrincipalModelType = PrincipalModelType;
      model.PrincipalModelId = PrincipalModelId;
    }

    return Task.FromResult(model);
  }

  private async Task<PaginatedList<ScopeModel>> OnScopesPageAsync(
    string search,
    int pageNumber,
    int pageSize
  )
  {
    if (Id is null)
    {
      return PaginatedList<ScopeModel>.Empty;
    }

    var queries = ScopedServices.GetRequiredService<ScopeQueries>();

    var scopes = await queries.ReadByApiKeyId(
      Id,
      pageNumber,
      CancellationToken.None,
      QueryConstants.DefaultPageCount,
      false,
      search
    );

    return scopes;
  }
}
