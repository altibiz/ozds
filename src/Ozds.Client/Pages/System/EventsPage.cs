using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class EventsPage : OzdsComponentBase
{
  private LevelModel _minLevel = LevelModel.Warning;

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<PaginatedList<IEvent>> OnPageAsync(int page)
  {
    return await ScopedServices
      .GetRequiredService<EventQueries>()
      .ReadByMinLevel<IEvent>(_minLevel, page, CancellationToken);
  }
}
