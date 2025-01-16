using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class MeasurementLocationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  MeasurementLocationModel,
  AuditableModel>(serviceProvider)
{
  public override void Initialize(MeasurementLocationModel model)
  {
    base.Initialize(model);
    model.MeterId = default!;
    model.Kind = default!;
  }
}
