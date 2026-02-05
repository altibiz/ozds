using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Activation.Implementations.Administration;

public class ApiKeyScopeModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<ApiKeyScopeModel, AuditableJoinModel>(
    serviceProvider
  )
{
  public override void Initialize(ApiKeyScopeModel model)
  {
    base.Initialize(model);

    model.ApiKeyId = Guid.Empty.ToString();
    model.ScopeId = Guid.Empty.ToString();
  }
}
