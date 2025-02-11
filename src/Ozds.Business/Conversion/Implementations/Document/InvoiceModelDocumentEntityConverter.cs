using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class InvoiceModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
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
    entity.Id = model.Id;
    entity.InvoiceTaxRate_Percent = model.InvoiceTaxRate_Percent;
    entity.InvoiceTax_EUR = model.InvoiceTax_EUR;
    entity.InvoiceTotalWithTax_EUR = model.InvoiceTotalWithTax_EUR;
  }
}
