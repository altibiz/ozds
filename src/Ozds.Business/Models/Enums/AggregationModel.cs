using Ozds.Business.Math;

namespace Ozds.Business.Models.Enums;

public enum AggregationModel
{
  Min,
  Max,
  Avg
}

public static class AggregationModelExtensions
{
  public static string ToTitle(this AggregationModel phase)
  {
    return phase switch
    {
      AggregationModel.Min => "Min",
      AggregationModel.Max => "Max",
      AggregationModel.Avg => "Avg",
      _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
    };
  }

  public static PhasicMeasure<decimal> GetMeasure(
    this PhasicMeasure<decimal> phasic,
    AggregationModel? aggregation,
    MeasureModel? measure
  )
  {
    return aggregation switch
    {
      AggregationModel.Min => phasic.AggregateMin(),
      AggregationModel.Max => phasic.AggregateMax(),
      AggregationModel.Avg => phasic.AggregateAvg(),
      _ => measure switch
      {
        MeasureModel.Voltage
          or MeasureModel.Current
          or MeasureModel.ActivePower
          or MeasureModel.ReactivePower
          or MeasureModel.ApparentPower => phasic.AggregateAvg(),
        MeasureModel.ActiveEnergy
          or MeasureModel.ReactiveEnergy
          or MeasureModel.ApparentEnergy => phasic.AggregateMax(),
        _ => phasic
      }
    };
  }
}
