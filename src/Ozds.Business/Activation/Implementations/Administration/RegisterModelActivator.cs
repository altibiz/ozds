using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.Administration;

public class RegisterModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<RegisterModel, TrackableModel>(
  serviceProvider
)
{
  public override void Initialize(RegisterModel model)
  {
    base.Initialize(model);

    model.ScopeId = "0";
    model.Name = string.Empty;
    model.Measure = MeasureModel.Current;
  }
}
