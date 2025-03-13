namespace Ozds.Data.Entities.Abstractions;

public interface IInstantaneousMeasureEntity : IAggregateMeasureEntity
{
  public float Min { get; }

  public float Max { get; }

  public float Avg { get; }
}
