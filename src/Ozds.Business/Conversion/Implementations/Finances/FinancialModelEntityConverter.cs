using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class FinancialModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    FinancialModel,
    IdentifiableModel,
    FinancialEntity,
    IdentifiableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    FinancialModel model,
    FinancialEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.IssuedOn = model.IssuedOn;
    entity.IssuedById = model.IssuedById;
    entity.FromDate = model.FromDate;
    entity.ToDate = model.ToDate;
    entity.Total_EUR = model.Total_EUR;
  }

  public override void InitializeModel(
    FinancialEntity entity,
    FinancialModel model)
  {
    base.InitializeModel(entity, model);
    model.IssuedOn = entity.IssuedOn;
    model.IssuedById = entity.IssuedById;
    model.FromDate = entity.FromDate;
    model.ToDate = entity.ToDate;
    model.Total_EUR = entity.Total_EUR;
  }
}
