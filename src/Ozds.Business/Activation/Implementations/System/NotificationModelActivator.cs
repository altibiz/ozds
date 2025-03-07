using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.System;

public class NotificationModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<NotificationModel, IdentifiableModel>(
    serviceProvider
  )
{
  public override void Initialize(NotificationModel model)
  {
    base.Initialize(model);
    model.Title = string.Empty;
    model.Timestamp = DateTimeOffset.UtcNow;
    model.Content = string.Empty;
    model.Summary = string.Empty;
    model.EventId = default!;
    model.Topics = new HashSet<TopicModel>();
  }
}
