using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Activation.Implementations.System;

public class LocationRepresentativeModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<LocationRepresentativeModel, JoinModel>(
  serviceProvider
)
{
  public override void Initialize(LocationRepresentativeModel model)
  {
    base.Initialize(model);

    model.LocationId = "0";
    model.RepresentativeId = string.Empty;
  }
}
