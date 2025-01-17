using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

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
    model.EventId = "0";
    model.Topics = new();
  }
}
