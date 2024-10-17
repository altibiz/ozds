using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Composite;

public record MetricsEntity(
  AggregateEntity MonthlyAggregate,

);
