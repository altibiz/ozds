using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class AuditableDetails : OzdsDetailsComponentBase<IAuditable>
{
  private async Task<PaginatedList<IAuditEvent>> OnPageAsync(int page)
  {
    var queries = ScopedServices
      .GetRequiredService<EventQueries>();

    var events = await queries.ReadAuditEvents<IAuditEvent>(
      Model,
      page,
      CancellationToken);

    return events;
  }
}
