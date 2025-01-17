using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class SystemAuditEventModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<SystemAuditEventModel, AuditEventModel>(
      serviceProvider
    )
{
}
