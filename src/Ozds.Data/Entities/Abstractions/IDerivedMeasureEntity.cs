namespace Ozds.Data.Entities.Abstractions;

public interface IDerivedMeasureEntity : IAggregateMeasureEntity
{
  public long Min { get; }

  public long Max { get; }

  public double Avg { get; }
}
