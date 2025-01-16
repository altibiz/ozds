using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Activation.Joins;

public class NotificationRecipientModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<NotificationRecipientModel, JoinModel>(
      serviceProvider
    )
{
  public override void Initialize(NotificationRecipientModel model)
  {
    base.Initialize(model);

    model.NotificationId = default!;
    model.RepresentativeId = default!;
  }
}
