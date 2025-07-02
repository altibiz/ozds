using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations.System;

public class NotificationModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clock
)
  : InheritingModelActivator<NotificationModel, IdentifiableModel>(
    serviceProvider
  )
{
  public override void Initialize(NotificationModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.Title = string.Empty;
    model.Timestamp = now;
    model.Content = string.Empty;
    model.Summary = string.Empty;
    model.EventId = default!;
    model.Topics = new HashSet<TopicModel>();
  }
}
