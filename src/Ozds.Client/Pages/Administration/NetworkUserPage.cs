using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NetworkUserPage
  : OzdsIdentifiableModelPageComponentBase<NetworkUserModel>
{
  private DateTime? invoiceSelectedMonth;
  private DateTime? monthlyAggregatesSelectedMonth;

  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<NetworkUserModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    var queries = ScopedServices.GetRequiredService<NetworkUserQueries>();

    var networkUser = await queries.ReadNetworkUserByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      Id,
      CancellationToken
    );

    return networkUser;
  }

  private async Task OnCreateInvoiceAsync()
  {
    if (invoiceSelectedMonth is not { } month ||
      Id is not { } id)
    {
      return;
    }
    var (dateFrom, dateTo) = DateTimeOffsetExtensions.GetMonthRange(
      month.Year,
      month.Month
    );

    var issuer = ScopedServices
      .GetRequiredService<NetworkUserInvoiceIssuer>();
    var invoice = await issuer.IssueNetworkUserInvoiceAsync(
      id,
      dateFrom,
      dateTo,
      CancellationToken
    );

    NavigateToPage<NetworkUserInvoicePage>(new { invoice.Invoice.Id });
  }
}
