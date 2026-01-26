using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Streaming;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class EventsPage : OzdsComponentBase
{
  private LevelModel minLevel = LevelModel.Warning;

  private Table<IEvent>? table;

  [CascadingParameter]
  public RepresentativeState RepresentativeState { get; set; } = default!;

  private IEnumerable<LevelModel> Levels
  {
    get
    {
      return Enum.GetValues<LevelModel>().Where(l => Environment.IsDevelopment()
        ? l >= LevelModel.Trace
        : l >= LevelModel.Information);
    }
  }

  private async Task<PaginatedList<IEvent>> OnPageAsync(
    string search,
    int page,
    int pageCount)
  {
    return await ScopedServices
      .GetRequiredService<EventQueries>()
      .Read<IEvent>(minLevel, page, CancellationToken, pageCount, search);
  }

  private async Task OnMinLevelChanged(LevelModel level)
  {
    minLevel = level;

    if (table is not null)
    {
      await table.Fetch();
    }
  }
}
