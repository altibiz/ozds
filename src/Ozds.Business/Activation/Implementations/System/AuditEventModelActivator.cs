using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.System;

public class AuditEventModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<AuditEventModel, EventModel>(
      serviceProvider
    )
{
  public override void Initialize(AuditEventModel model)
  {
    base.Initialize(model);

    model.Audit = AuditModel.Query;
  }
}
