using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class NetworkUserCatalogueModelDocumentEntityConverter
  : ConcreteModelDocumentEntityConverter<
    NetworkUserCatalogueModel,
    NetworkUserCatalogueEntity>
{
  public override void InitializeEntity(
    NetworkUserCatalogueModel model,
    NetworkUserCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterFeePrice_EUR = model.MeterFeePrice_EUR;
  }
}
