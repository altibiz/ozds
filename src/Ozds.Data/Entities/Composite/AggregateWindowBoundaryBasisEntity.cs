using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public class AggregateWindowBoundaryBasisEntity
{
  public required string MeasurementLocationId { get; init; }
  public AggregateEntity? StartAggregate { get; init; }
  public AggregateEntity? EndAggregate { get; init; }
}
public class AggregateWindowLoadCurveBasisEnity : AggregateWindowBoundaryBasisEntity
{
  public List<AggregateEntity> InWindowAggregates { get; init; } = [];
}
