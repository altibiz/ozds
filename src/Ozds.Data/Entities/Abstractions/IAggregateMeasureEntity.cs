namespace Ozds.Data.Entities.Abstractions;

public interface IAggregateMeasureEntity
{
  public float Min { get; }

  public float Max { get; }
}
