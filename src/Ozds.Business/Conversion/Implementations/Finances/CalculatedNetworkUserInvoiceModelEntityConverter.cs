using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data.Entities.Composite;

namespace Ozds.Business.Conversion.Composite;

public class CalculatedNetworkUserInvoiceModelEntityConverter :
  ConcreteModelEntityConverter<
    CalculatedNetworkUserInvoiceModel,
    CalculatedNetworkUserInvoiceEntity>
{
  protected override CalculatedNetworkUserInvoiceEntity ToEntity(
    CalculatedNetworkUserInvoiceModel model
  )
  {
    return model.ToEntity();
  }

  protected override CalculatedNetworkUserInvoiceModel ToModel(
    CalculatedNetworkUserInvoiceEntity entity
  )
  {
    return entity.ToModel();
  }
}

public static class CalculatedNetworkUserInvoiceModelEntityConverterExtensions
{
  public static CalculatedNetworkUserInvoiceModel ToModel(
    this CalculatedNetworkUserInvoiceEntity entity
  )
  {
    return new CalculatedNetworkUserInvoiceModel(
      entity.Calculations
        .Select(calculation => calculation.ToModel())
        .ToList(),
      entity.Invoice.ToModel()
    );
  }

  public static CalculatedNetworkUserInvoiceEntity ToEntity(
    this CalculatedNetworkUserInvoiceModel model
  )
  {
    return new CalculatedNetworkUserInvoiceEntity(
      model.Calculations
        .Select(calculation => calculation.ToEntity())
        .ToList(),
      model.Invoice.ToEntity()
    );
  }
}
