using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IMeasurement, IValidatableObject, IReadonly
{
  public IntervalModel Interval { get; }

  public long Count { get; }

  public SpanningMeasure<float> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<float> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<float> ApparentEnergySpan_VAh { get; }
}
