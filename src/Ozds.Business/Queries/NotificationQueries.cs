using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Entities.Abstractions;
using DataNotificationQueries = Ozds.Data.Queries.NotificationQueries;

namespace Ozds.Business.Queries;

public class NotificationQueries(
  DataNotificationQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INotification
  {
    var entityType = modelEntityConverter.EntityType(typeof(T));
    var entities = await queries.ReadForRecipient(
      entityType,
      representativeId,
      pageNumber,
      cancellationToken,
      seen,
      title,
      pageCount
    );

    return entities
      .Items.Select(modelEntityConverter.ToModel<T>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<T>> ReadForRecipient<T>(
    string representativeId,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null
  )
    where T : class, INotification
  {
    var entityType = modelEntityConverter.EntityType(typeof(T));
    var entities = await queries.ReadForRecipient(
      entityType,
      representativeId,
      cancellationToken,
      seen,
      title
    );

    return entities.Select(modelEntityConverter.ToModel<T>).ToList();
  }

  public async Task<PaginatedList<INotification>> ReadForRecipient(
    Type modelType,
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    if (!modelType.IsAssignableTo(typeof(INotification)))
    {
      throw new InvalidOperationException(
        $"{modelType} is not a ${typeof(INotification)}"
      );
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadForRecipient(
      entityType,
      representativeId,
      pageNumber,
      cancellationToken,
      seen,
      title,
      pageCount
    );

    return entities
      .Items.Select(modelEntityConverter.ToModel<INotification>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<INotification>> ReadForRecipient(
    Type modelType,
    string representativeId,
    CancellationToken cancellationToken,
    bool seen = false,
    string? title = null
  )
  {
    if (!modelType.IsAssignableTo(typeof(INotification)))
    {
      throw new InvalidOperationException(
        $"{modelType} is not a ${typeof(INotification)}"
      );
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadForRecipient(
      entityType,
      representativeId,
      cancellationToken,
      seen,
      title
    );

    return entities
      .Select(modelEntityConverter.ToModel<INotification>)
      .ToList();
  }

  public async Task<List<NotificationRecipientModel>> Recipients(
    INotification notification
  )
  {
    var recipients = await queries.Recipients(
      modelEntityConverter.ToEntity<INotificationEntity>(notification)
    );

    return recipients
      .Select(modelEntityConverter.ToModel<NotificationRecipientModel>)
      .ToList();
  }

  public async Task<List<NotificationRecipientModel>> Recipients(
    IEnumerable<INotification> notifications
  )
  {
    var entities = notifications.Select(
      modelEntityConverter.ToEntity<INotificationEntity>
    );
    var recipients = await queries.Recipients(entities);

    return recipients
      .Select(modelEntityConverter.ToModel<NotificationRecipientModel>)
      .ToList();
  }
}
