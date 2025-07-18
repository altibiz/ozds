using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Data.Entities.Abstractions;
using DataNotificationMutations = Ozds.Data.Mutations.NotificationMutations;

namespace Ozds.Business.Mutations;

public class NotificationMutations(
  DataNotificationMutations mutations,
  ModelEntityConverter modelEntityConverter,
  RepresentativeQueries representativeQueries
) : IMutations
{
  public async Task<NotificationRecipientModel?> MarkNotificationAsSeen(
    string notificationId,
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    var entity = await mutations.MarkNotificationAsSeen(
      notificationId,
      representativeId,
      cancellationToken
    );

    var model = entity is null
      ? null
      : modelEntityConverter.ToModel<NotificationRecipientModel>(entity);

    return model;
  }

  public async Task<IResolvableNotification> MarkNotificationAsResolved(
    IResolvableNotification notification,
    CancellationToken cancellationToken
  )
  {
    if (notification is not ResolvableNotificationModel resolvableNotification)
    {
      throw new ArgumentException(
        $"{
          nameof(notification)
        } is not of type {
          nameof(ResolvableNotificationModel)
        }"
      );
    }

    if (resolvableNotification.ResolvedOn is not null)
    {
      throw new InvalidOperationException(
        $"{nameof(resolvableNotification)} is already resolved"
      );
    }

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = modelEntityConverter.ToEntity<IResolvableNotificationEntity>(
      resolvableNotification
    );
    entity.RepresentativeId = representativeId;

    var updated = await mutations
      .MarkNotificationAsResolved(entity, cancellationToken);

    var model = modelEntityConverter.ToModel<IResolvableNotification>(updated);

    return model;
  }
}
