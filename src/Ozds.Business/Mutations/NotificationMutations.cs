using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;
using DataNotificationMutations = Ozds.Data.Mutations.NotificationMutations;

namespace Ozds.Business.Mutations;

public class NotificationMutations(
  DataNotificationMutations mutations
) : IMutations
{
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
