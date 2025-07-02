using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Mutations;
using Ozds.Business.Queries;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NetworkUserPage
  : OzdsIdentifiableModelPageComponentBase<NetworkUserModel>
{
  private DateTime invoiceSelectedMonth =
    // NOTE: just so something is there
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture).DateTime;

  private DateTime monthlyAggregatesSelectedMonth =
    // NOTE: just so something is there
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture).DateTime;

  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [Inject]
  private ClockQueries ClockQueries { get; set; } = default!;

  [Inject]
  private TimeQueries TimeQueries { get; set; } = default!;

  protected override void OnInitialized()
  {
    var now = ClockQueries.Now();
    var startOfLastMonth = TimeQueries.GetStartOfLastMonth(now).DateTime;

    invoiceSelectedMonth = startOfLastMonth;
    monthlyAggregatesSelectedMonth = startOfLastMonth;
  }

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
    if (Id is not { } id)
    {
      return;
    }

    var (dateFrom, dateTo) = TimeQueries.GetMonthRange(
      invoiceSelectedMonth.Year,
      invoiceSelectedMonth.Month
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
