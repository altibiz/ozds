using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Activation.Implementations.Administration;

public class NetworkUserRepresentativeModelActivator(
  IServiceProvider serviceProvider
)
  : InheritingModelActivator<
    NetworkUserRepresentativeModel,
    AuditableJoinModel
  >(serviceProvider)
{
  public override void Initialize(NetworkUserRepresentativeModel model)
  {
    base.Initialize(model);

    model.NetworkUserId = "0";
    model.RepresentativeId = string.Empty;
  }
}
