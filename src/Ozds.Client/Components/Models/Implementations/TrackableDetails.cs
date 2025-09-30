using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class TrackableDetails : OzdsDetailsComponentBase<ITrackable>
{
  private async Task<PaginatedList<IAuditEvent>> OnPageAsync(
    string search,
    int page,
    int pageCount
  )
  {
    var queries = ScopedServices
      .GetRequiredService<EventQueries>();

    var events = await queries.ReadAuditEvents<IAuditEvent>(
      Model,
      page,
      CancellationToken,
      pageCount,
      search);

    return events;
  }
}
