using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class SchneideriEM3xxxMeterModelReportEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelReportEntityConverter<
    SchneideriEM3xxxMeterModel,
    IdentifiableModel,
    SchneideriEM3xxxMeterEntity,
    IdentifiableEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    SchneideriEM3xxxMeterModel model,
    SchneideriEM3xxxMeterEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Id = model.Id;
    entity.ConnectionPower_W = model.ConnectionPower_W;
    entity.MessengerId = model.MessengerId!;
    entity.MeasurementValidatorId = model.MeasurementValidatorId;
    entity.Phases = string.Join(
      ReportConversionConstants.ListDelimiter,
      model.Phases.Select(p => p.ToString())
    );
  }

  public override void InitializeModel(
    SchneideriEM3xxxMeterEntity entity,
    SchneideriEM3xxxMeterModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Id = entity.Id;
    model.ConnectionPower_W = entity.ConnectionPower_W;
    model.MessengerId = entity.MessengerId;
    model.MeasurementValidatorId = entity.MeasurementValidatorId;
    model.Phases = entity
      .Phases.Split(
        ReportConversionConstants.ListDelimiter,
        StringSplitOptions.RemoveEmptyEntries
      )
      .Select(
        static s =>
          Enum.TryParse<PhaseModel>(s.Trim(), out var phase)
            ? phase
            : throw new InvalidOperationException(
              "An error occurred while trying to parse a csv input as a PhaseModel."
            )
      )
      .ToHashSet();
  }
}
