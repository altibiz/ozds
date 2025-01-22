using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class MeasurementsPage : OzdsComponentBase
{
  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private LocationState LocationState { get; set; } = default!;

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  private Task<PaginatedList<IMessenger>> OnMessengersPageAsync(int page)
  {
    var queries = ScopedServices.GetRequiredService<MessengerQueries>();

    return queries.ReadByLocationId(
      LocationState.Location?.Id
        ?? throw new InvalidOperationException(
          $"Location is null for {nameof(MeasurementsPage)}"),
      page,
      CancellationToken
    );
  }
}
