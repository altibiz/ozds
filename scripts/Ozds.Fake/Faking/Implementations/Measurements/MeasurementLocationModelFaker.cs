using Ozds.Business.Models.Base;
using Ozds.Fake.Faking.Base;

namespace Ozds.Fake.Faking.Implementations.Measurements;

public class MeasurementLocationModelFaker(IServiceProvider serviceProvider)
  : InheritingModelFaker<MeasurementLocationModel, TrackableModel>(
    serviceProvider
  ) { }
