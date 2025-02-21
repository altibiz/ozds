using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class SchneideriEM3xxxMeterModelRecordConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelRecordConverter<
    SchneideriEM3xxxMeterModel,
    IdentifiableModel,
    SchneideriEM3xxxMeterRecord,
    IdentifiableRecord
  >(serviceProvider)
{
  private static readonly char[] Delimiter = { ',' };

  public override void InitializeRecord(
    SchneideriEM3xxxMeterModel model,
    SchneideriEM3xxxMeterRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.Id = model.Id;
    record.ConnectionPower_W = model.ConnectionPower_W;
    record.MessengerId = model.MessengerId!;
    record.MeasurementValidatorId = model.MeasurementValidatorId;
    record.Phases = string.Join(",", model.Phases.Select(p => p.ToString()));
  }

  public override void InitializeModel(
    SchneideriEM3xxxMeterRecord record,
    SchneideriEM3xxxMeterModel model
  )
  {
    base.InitializeModel(record, model);
    model.Id = record.Id;
    model.ConnectionPower_W = record.ConnectionPower_W;
    model.MessengerId = record.MessengerId;
    model.MeasurementValidatorId = record.MeasurementValidatorId;
    model.Phases = record
      .Phases.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries)
      .Select(
        static s =>
          Enum.TryParse<PhaseModel>(s.Trim(), out var phase)
            ? phase
            : throw new InvalidOperationException(
              "An error occurred due to an invalid operation."
            )
      )
      .ToHashSet();
  }
}
