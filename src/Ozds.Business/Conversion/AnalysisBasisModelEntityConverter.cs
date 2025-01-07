using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data.Entities.Composite;

namespace Ozds.Business.Conversion;

public class AnalysisBasisModelEntityConverter : ModelEntityConverter<
  AnalysisBasisModel, AnalysisBasisEntity>
{
  protected override AnalysisBasisEntity ToEntity(
    AnalysisBasisModel model
  )
  {
    return model.ToEntity();
  }

  protected override AnalysisBasisModel ToModel(
    AnalysisBasisEntity entity
  )
  {
    return entity.ToModel();
  }
}

public static class AnalysisBasisModelEntityConverterExtensions
{
  public static AnalysisBasisModel ToModel(
    this AnalysisBasisEntity entity
  )
  {
    return new AnalysisBasisModel(
      entity.Representative.ToModel(),
      entity.FromDate,
      entity.ToDate,
      entity.Location.ToModel(),
      entity.NetworkUser is { }
        ? entity.NetworkUser.ToModel()
        : null,
      entity.MeasurementLocation.ToModel(),
      entity.Meter.ToModel(),
      entity.Calculations
        .Select(calculation => calculation.ToModel())
        .ToList(),
      entity.Invoices
        .Select(invoice => invoice.ToModel())
        .ToList(),
      entity.LastMeasurement.ToModel(),
      entity.MonthlyAggregates
        .Select(aggregate => aggregate.ToModel())
        .ToList()
    );
  }

  public static AnalysisBasisEntity ToEntity(
    this AnalysisBasisModel model
  )
  {
    return new AnalysisBasisEntity(
      model.Representative.ToEntity(),
      model.FromDate,
      model.ToDate,
      model.Location.ToEntity(),
      model.NetworkUser is { }
        ? model.NetworkUser.ToEntity()
        : null,
      model.MeasurementLocation.ToEntity(),
      model.Meter.ToEntity(),
      model.Calculations
        .Select(calculation => calculation.ToEntity())
        .ToList(),
      model.Invoices
        .Select(invoice => invoice.ToEntity())
        .ToList(),
      model.LastMeasurement.ToEntity(),
      model.MonthlyAggregates
        .Select(aggregate => aggregate.ToEntity())
        .ToList()
    );
  }
}
