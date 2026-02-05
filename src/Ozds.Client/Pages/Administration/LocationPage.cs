using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class LocationPage
  : OzdsIdentifiableModelPageComponentBase<LocationModel>
{
  private DateTime selectedMonth =
    // NOTE: just so something is there
    DateTimeOffset
      .Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture)
      .DateTime;

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

    selectedMonth = TimeQueries.GetStartOfLastMonth(now).DateTime;
  }

  private async Task<LocationModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    var queries = ScopedServices.GetRequiredService<LocationQueries>();

    var location = await queries.ReadIndirectByRepresentativeIdAndId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      Id,
      CancellationToken
    );

    return location;
  }
}
