namespace Ozds.Business.Models.Abstractions;

public interface IAggregateMeasure
{
  decimal Min { get; }

  decimal Max { get; }
}
