using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class AuditableModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<AuditableModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(AuditableModel model)
  {
    base.Initialize(model);

    var now = DateTimeOffset.UtcNow;

    model.CreatedOn = now;
    model.CreatedById = default!;
    model.LastUpdatedOn = default!;
    model.LastUpdatedById = default!;
    model.IsDeleted = false;
    model.DeletedOn = default!;
    model.DeletedById = default!;
  }
}
