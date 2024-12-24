using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class AggregateFlushEventArgs : System.EventArgs
{
  public IReadOnlyList<IAggregate> Aggregates { get; set; } = default!;
}
