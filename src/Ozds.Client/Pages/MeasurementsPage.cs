using Microsoft.AspNetCore.Components;
using Ozds.Business.Analysis;
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

  [Inject]
  private Analyzer Analyzer { get; set; } = default!;

  private Task<PaginatedList<IMessenger>> OnMessengersPageAsync(
    string search,
    int pageNumber,
    int pageCount
  )
  {
    var queries = ScopedServices.GetRequiredService<MessengerQueries>();

    return queries.ReadByLocationId(
      LocationState.Location?.Id
      ?? throw new InvalidOperationException(
        $"Location is null for {nameof(MeasurementsPage)}"),
      pageNumber,
      CancellationToken,
      pageCount,
      false,
      search
    );
  }
}
