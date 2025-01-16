using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class
  LocationMeasurementLocationModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
  LocationMeasurementLocationModel,
  MeasurementLocationModel>(serviceProvider)
{
  public override void Initialize(LocationMeasurementLocationModel model)
  {
    base.Initialize(model);
    model.LocationId = "0";
  }
}
