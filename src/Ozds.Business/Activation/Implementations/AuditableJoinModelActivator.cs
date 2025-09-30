using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;

namespace Ozds.Business.Activation.Implementations;

public class AuditableJoinModelActivator(
  IServiceProvider serviceProvider,
  ClockQueries clock
)
  : InheritingModelActivator<AuditableJoinModel, JoinModel>(serviceProvider)
{
  public override void Initialize(AuditableJoinModel model)
  {
    base.Initialize(model);

    var now = clock.Timestamp();

    model.CreatedOn = now;
    model.CreatedById = default!;
  }
}
