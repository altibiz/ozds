using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Pages;

public partial class NetworkUserCalculationDocumentPage : OzdsComponentBase
{
  [Parameter]
  public string Id { get; set; } = default!;

  private async Task<NetworkUserCalculationModelWithHtml?> OnLoadAsync()
  {
    var calculation = await ScopedServices
      .GetRequiredService<IdentifiableQueries>()
      .ReadById<NetworkUserCalculationModel>(Id, CancellationToken);
    if (calculation is null)
    {
      return null;
    }

    var html = await ScopedServices
      .GetRequiredService<DocumentMutations>()
      .CreateHtmlForNetworkUserCalculation(calculation, CancellationToken);
    if (html is null)
    {
      return null;
    }

    return new NetworkUserCalculationModelWithHtml
    {
      Calculation = calculation,
      Html = html,
    };
  }

  private sealed class NetworkUserCalculationModelWithHtml
  {
    public NetworkUserCalculationModel Calculation { get; set; } = default!;

    public string Html { get; set; } = default!;
  }
}
