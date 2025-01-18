using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Messaging.Entities;
using Ozds.Messaging.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserInvoiceStateEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NetworkUserInvoiceStateModel,
      StateModel,
      NetworkUserInvoiceStateEntity,
      StateEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserInvoiceStateModel model,
    NetworkUserInvoiceStateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.NetworkUserInvoiceId = model.NetworkUserInvoiceId;
    entity.BillId = model.BillId;
    entity.AbortReason = model.AbortReason;
  }

  public override void InitializeModel(
    NetworkUserInvoiceStateEntity entity,
    NetworkUserInvoiceStateModel model
  )
  {
    base.InitializeModel(entity, model);
    model.NetworkUserInvoiceId = entity.NetworkUserInvoiceId;
    model.BillId = entity.BillId;
    model.AbortReason = entity.AbortReason;
  }
}
