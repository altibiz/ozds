using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class InvoiceModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    InvoiceModel,
    FinancialModel,
    InvoiceEntity,
    FinancialEntity>(serviceProvider)
{
  public override void InitializeEntity(
    InvoiceModel model,
    InvoiceEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.InvoiceTaxRate_Percent = model.InvoiceTaxRate_Percent;
    entity.InvoiceTax_EUR = model.InvoiceTax_EUR;
    entity.InvoiceTotalWithTax_EUR = model.InvoiceTotalWithTax_EUR;
  }

  public override void InitializeModel(
    InvoiceEntity entity,
    InvoiceModel model
  )
  {
    base.InitializeModel(entity, model);
    model.InvoiceTaxRate_Percent = entity.InvoiceTaxRate_Percent;
    model.InvoiceTax_EUR = entity.InvoiceTax_EUR;
    model.InvoiceTotalWithTax_EUR = entity.InvoiceTotalWithTax_EUR;
  }
}
