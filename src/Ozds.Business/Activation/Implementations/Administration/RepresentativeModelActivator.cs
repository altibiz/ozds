using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.Administration;

public class RepresentativeModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<RepresentativeModel, AuditableModel>(
    serviceProvider
  )
{
  private readonly ModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(RepresentativeModel model)
  {
    base.Initialize(model);
    model.Role = RoleModel.NetworkUserRepresentative;
    model.PhysicalPerson = _agnosticModelActivator
      .Activate<PhysicalPersonModel>();
    model.Topics = RoleModel.NetworkUserRepresentative.ToTopics();
  }
}
