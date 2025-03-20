namespace Ozds.Data.Entities.Abstractions;

public interface ICumulativeMeasureEntity : IAggregateMeasureEntity
{
  public long Min { get; }

  public long Max { get; }
}
