using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class NetworkUserMeasurementLocationModelRecordConverter(
  IServiceProvider serviceProvider)
  : InheritingModelRecordConverter<
    NetworkUserMeasurementLocationModel,
    IdentifiableModel,
    NetworkUserMeasurementLocationRecord,
    IdentifiableRecord
  >(serviceProvider)
{
  public override void InitializeRecord(
    NetworkUserMeasurementLocationModel model,
    NetworkUserMeasurementLocationRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.MeterId = model.MeterId;
    record.NetworkUserId = model.NetworkUserId;
    record.NetworkUserCatalogueId = model.NetworkUserCatalogueId;
  }

  public override void InitializeModel(
    NetworkUserMeasurementLocationRecord record,
    NetworkUserMeasurementLocationModel model
  )
  {
    base.InitializeModel(record, model);
    model.MeterId = record.MeterId;
    model.NetworkUserId = record.NetworkUserId;
    model.NetworkUserCatalogueId = record.NetworkUserCatalogueId;
  }
}
