using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class AbbB2xMeterModelRecordConverter(IServiceProvider serviceProvider)
  : InheritingModelRecordConverter<
    AbbB2xMeterModel,
    IdentifiableModel,
    AbbB2xMeterRecord,
    IdentifiableRecord
  >(serviceProvider)
{
  public override void InitializeRecord(
    AbbB2xMeterModel model,
    AbbB2xMeterRecord record
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
    AbbB2xMeterRecord record,
    AbbB2xMeterModel model
  )
  {
    base.InitializeModel(record, model);
    model.Id = record.Id;
    model.ConnectionPower_W = record.ConnectionPower_W;
    model.MessengerId = record.MessengerId;
    model.MeasurementValidatorId = record.MeasurementValidatorId;
    model.Phases = record
      .Phases.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
      .Select(static s =>
        Enum.TryParse<PhaseModel>(s.Trim(), out var phase)
          ? phase
          : throw new InvalidOperationException(
            "An error occurred due to an invalid operation."
          )
      )
      .ToHashSet();
  }
}
