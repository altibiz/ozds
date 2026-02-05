using Ozds.Business.Activation.Base;
using Ozds.Business.Authorization;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Reflection;

namespace Ozds.Business.Activation.Implementations.Administration;

public class ApiKeyModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<ApiKeyModel, TrackableModel>(serviceProvider)
{
  private readonly ApiKeyManager apiKeyManager =
    serviceProvider.GetRequiredService<ApiKeyManager>();

  private readonly ModelReflector modelReflector =
    serviceProvider.GetRequiredService<ModelReflector>();

  public override void Initialize(ApiKeyModel model)
  {
    base.Initialize(model);

    model.Id = Guid.NewGuid().ToString();

    model.PrincipalModelType = modelReflector.ResolveModelName(
      modelReflector.PrincipalTypeList.First()
    );
    model.PrincipalModelId = string.Empty;

    model.Value = apiKeyManager.Generate();
    model.Hash = apiKeyManager.Hash(model.Value);

    model.ExpiresOn = default;
  }
}
