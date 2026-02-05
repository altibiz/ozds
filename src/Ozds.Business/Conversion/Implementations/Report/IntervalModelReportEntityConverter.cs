using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class IntervalModelReportEntityConverter
  : ConcreteModelReportEntityConverter<IntervalModel, IntervalEntity>
{
  public override IntervalEntity ToEntity(IntervalModel model)
  {
    return model switch
    {
      IntervalModel.Month => IntervalEntity.Month,
      IntervalModel.Day => IntervalEntity.Day,
      IntervalModel.QuarterHour => IntervalEntity.QuarterHour,
      _ => throw new NotImplementedException(),
    };
  }

  public override IntervalModel ToModel(IntervalEntity entity)
  {
    return entity switch
    {
      IntervalEntity.Month => IntervalModel.Month,
      IntervalEntity.Day => IntervalModel.Day,
      IntervalEntity.QuarterHour => IntervalModel.QuarterHour,
      _ => throw new NotImplementedException(),
    };
  }
}
