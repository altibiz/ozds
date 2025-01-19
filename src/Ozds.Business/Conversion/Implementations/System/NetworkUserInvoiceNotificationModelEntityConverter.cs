using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class NetworkUserInvoiceNotificationEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NetworkUserInvoiceNotificationModel,
      NotificationModel,
      NetworkUserInvoiceNotificationEntity,
      NotificationEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserInvoiceNotificationModel model,
    NetworkUserInvoiceNotificationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.InvoiceId = model.InvoiceId;
  }

  public override void InitializeModel(
    NetworkUserInvoiceNotificationEntity entity,
    NetworkUserInvoiceNotificationModel model)
  {
    base.InitializeModel(entity, model);
    model.InvoiceId = entity.InvoiceId;
  }
}
