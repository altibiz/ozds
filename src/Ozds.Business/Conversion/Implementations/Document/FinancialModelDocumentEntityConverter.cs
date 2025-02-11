using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class FinancialModelDocumentEntityConverter
  : ConcreteModelDocumentEntityConverter<
      FinancialModel,
      FinancialEntity>
{
  public override void InitializeEntity(
    FinancialModel model,
    FinancialEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.IssuedOn = model.IssuedOn;
    entity.IssuedById = model.IssuedById;
    entity.FromDate = model.FromDate;
    entity.ToDate = model.ToDate;
    entity.Total_EUR = model.Total_EUR;
  }
}
