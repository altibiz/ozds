using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class RepresentativeAuditEventModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<RepresentativeAuditEventModel, AuditEventModel>(
      serviceProvider
    )
{
  public override void Initialize(RepresentativeAuditEventModel model)
  {
    base.Initialize(model);

    model.RepresentativeId = string.Empty;
  }
}
