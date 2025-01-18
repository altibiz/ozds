using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculatedNetworkUserInvoiceModelEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelEntityConverter<
      CalculatedNetworkUserInvoiceModel,
      CalculatedNetworkUserInvoiceEntity>
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    CalculatedNetworkUserInvoiceModel model,
    CalculatedNetworkUserInvoiceEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Calculations = model.Calculations
      .Select(modelEntityConverter.ToEntity<NetworkUserCalculationEntity>)
      .ToList();
    entity.Invoice = modelEntityConverter.ToEntity<NetworkUserInvoiceEntity>(
      model.Invoice
    );
  }

  public override void InitializeModel(
    CalculatedNetworkUserInvoiceEntity entity,
    CalculatedNetworkUserInvoiceModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Calculations = entity.Calculations
      .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
      .ToList();
    model.Invoice = modelEntityConverter.ToModel<NetworkUserInvoiceModel>(
      entity.Invoice
    );
  }
}
