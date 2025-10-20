using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Reflection;

namespace Ozds.Business.Activation.Implementations.Administration;

public class ScopeModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<ScopeModel, TrackableModel>(
  serviceProvider
)
{
  private readonly ModelReflector modelReflector =
    serviceProvider.GetRequiredService<ModelReflector>();

  public override void Initialize(ScopeModel model)
  {
    base.Initialize(model);

    model.Id = Guid.NewGuid().ToString();

    model.ScopeModelType =
      modelReflector.ScopeTypeList.First() is { } first
        ? modelReflector.ResolveModelName(first)
        : null;
    model.ScopeModelId = string.Empty;

    model.ScopeAction = ActionModel.Read;
  }
}
