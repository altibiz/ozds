using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Validation.Agnostic;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Mutations.Abstractions;
using DataNotificationMutations = Ozds.Data.Mutations.NotificationMutations;

namespace Ozds.Business.Mutations;

public class NotificationMutations(
  DataNotificationMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticValidator validator
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

  public async Task MarkNotificationAsSeen(
    string notificationId,
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    await mutations.MarkNotificationAsSeen(
      notificationId,
      representativeId,
      cancellationToken
    );
  }
}
