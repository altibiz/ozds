using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;

namespace Ozds.Business.Queries.Agnostic;

public class NewOzdsNotificationQueries(
  IDbContextFactory<DataDbContext> contextFactory,
  IServiceProvider serviceProvider
) : IQueries
{
  public async Task<PaginatedList<NotificationModel>> Read(
    string representativeId = default!,
    bool includeSeen = false,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var modelEntityConverter = serviceProvider
        .GetServices<IModelEntityConverter>()
        .FirstOrDefault(
          converter => converter
            .CanConvertToEntity(typeof(NotificationModel)))
      ?? throw new InvalidOperationException(
        $"No model entity converter found for model {typeof(NotificationModel)}");

    using var context = await contextFactory.CreateDbContextAsync();

    var filtered = representativeId is null
      ? context.Notifications
      : includeSeen
        ? context.NotificationRecipients
            .Where(x => x.RepresentativeId == representativeId)
            .Include(x => x.Notification)
            .Select(x => x.Notification)
        : context.NotificationRecipients
            .Where(x => x.RepresentativeId == representativeId && x.SeenOn == null)
            .Include(x => x.Notification)
            .Select(x => x.Notification);

    var ordered = filtered.OrderByDescending(x => x.Timestamp);

    var count = await filtered.CountAsync();

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();

    return items
      .Select(modelEntityConverter.ToModel)
      .OfType<NotificationModel>()
      .ToPaginatedList(count);
  }
}
