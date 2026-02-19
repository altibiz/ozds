using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations;

public class TrackableModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clock
) : InheritingModelActivator<TrackableModel, IdentifiableModel>(serviceProvider)
{
  public override void Initialize(TrackableModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.CreatedOn = now;
    model.CreatedById = default!;
    model.LastUpdatedOn = default!;
    model.LastUpdatedById = default!;
    model.IsDeleted = false;
    model.DeletedOn = default!;
    model.DeletedById = default!;
  }
}
