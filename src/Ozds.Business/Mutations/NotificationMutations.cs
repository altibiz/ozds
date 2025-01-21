using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Validation;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Joins;
using DataNotificationMutations = Ozds.Data.Mutations.NotificationMutations;

namespace Ozds.Business.Mutations;

public class NotificationMutations(
  DataNotificationMutations mutations,
  ModelEntityConverter modelEntityConverter,
  ModelValidator validator
) : IMutations
{
  public async Task<string> Create(
    INotification model,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator
      .ValidateAsync(model, cancellationToken);
    if (validationResults.Count > 0)
    {
      var result = string.Join("\n", validationResults);
      throw new InvalidOperationException(
        $"Model {model.GetType()} {model.Id} failed validation {result}"
      );
    }

    var entity = modelEntityConverter.ToEntity<INotificationEntity>(model);
    await mutations.Create(entity, cancellationToken);
    return entity.Id;
  }

  public async Task AddRecipients(
    IReadOnlyCollection<NotificationRecipientModel> recipients,
    CancellationToken cancellationToken
  )
  {
    var entities = recipients
      .Select(modelEntityConverter.ToEntity<NotificationRecipientEntity>)
      .ToList();
    await mutations.AddRecipients(entities, cancellationToken);
  }

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
}
