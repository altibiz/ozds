using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class NetworkUserMeasurementLocationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  NetworkUserMeasurementLocationModel,
  MeasurementLocationModel>(serviceProvider)
{
  public override void Initialize(NetworkUserMeasurementLocationModel model)
  {
    base.Initialize(model);
    model.NetworkUserId = "0";
    model.NetworkUserCatalogueId = "0";
  }
}
