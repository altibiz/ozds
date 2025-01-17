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
  public async Task<T?> ReadSingle<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : INotification
  {
    var entity = await ReadSingleDynamic(typeof(T), id, cancellationToken);
    return (T?)entity;
  }

  public async Task<INotification?> ReadSingleDynamic(
    Type type,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!type.IsAssignableTo(typeof(INotification)))
    {
      throw new ArgumentException(
        "Type must be a concrete type of Notification",
        nameof(type)
      );
    }

    var entityType = modelEntityConverter.EntityType(type);
    var entity = await queries
      .ReadSingleDynamic(entityType, id, cancellationToken);
    return entity is null
      ? null
      : modelEntityConverter.ToModel<INotification>(entity);
  }

  public async Task<PaginatedList<T>> ReadForRecipient<T>(
    string representativeId,
    int pageNumber,
    CancellationToken cancellationToken,
    bool seen = false,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, INotification
  {
    var entityType = modelEntityConverter.EntityType(typeof(T));
    var entities = await queries.ReadForRecipientDynamic(
      entityType,
      representativeId,
      seen,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToEntity<T>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<T>> ReadForRecipient<T>(
    string representativeId,
    CancellationToken cancellationToken,
    bool seen = false
  )
    where T : class, INotification
  {
    var entityType = modelEntityConverter.EntityType(typeof(T));
    var entities = await queries.ReadForRecipientDynamic(
      entityType,
      representativeId,
      seen,
      cancellationToken
    );

    return entities
      .Select(modelEntityConverter.ToModel<T>)
      .ToList();
  }

  public async Task<PaginatedList<INotification>> ReadForRecipientDynamic(
    Type modelType,
    string representativeId,
    bool seen,
    int pageNumber,
    CancellationToken cancellationToken,
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
    var entities = await queries.ReadForRecipientDynamic(
      entityType,
      representativeId,
      seen,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<INotification>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<List<INotification>> ReadForRecipientDynamic(
    Type modelType,
    string representativeId,
    bool seen,
    CancellationToken cancellationToken
  )
  {
    if (!modelType.IsAssignableTo(typeof(INotification)))
    {
      throw new InvalidOperationException(
        $"{modelType} is not a ${typeof(INotification)}"
      );
    }

    var entityType = modelEntityConverter.EntityType(modelType);
    var entities = await queries.ReadForRecipientDynamic(
      entityType,
      representativeId,
      seen,
      cancellationToken
    );

    return entities
      .Select(modelEntityConverter.ToModel<INotification>)
      .ToList();
  }

  public async Task<List<NotificationRecipientModel>> Recipients(
    INotification notification)
  {
    var recipients = await queries.Recipients(
      modelEntityConverter.ToEntity<INotificationEntity>(notification));

    return recipients
      .Select(modelEntityConverter.ToModel<NotificationRecipientModel>)
      .ToList();
  }
}
