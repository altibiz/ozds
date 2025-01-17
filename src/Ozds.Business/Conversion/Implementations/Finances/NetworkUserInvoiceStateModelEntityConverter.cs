using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Messaging.Entities;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserInvoiceStateModelEntityConverter :
  ConcreteModelEntityConverter<
    NetworkUserInvoiceStateModel,
    NetworkUserInvoiceStateEntity>
{
  protected override NetworkUserInvoiceStateEntity ToEntity(
    NetworkUserInvoiceStateModel model
  )
  {
    return model.ToEntity();
  }

  protected override NetworkUserInvoiceStateModel ToModel(
    NetworkUserInvoiceStateEntity entity
  )
  {
    return entity.ToModel();
  }
}

public static class NetworkUserInvoiceStateModelEntityConverterExtensions
{
  public static NetworkUserInvoiceStateModel ToModel(
    this NetworkUserInvoiceStateEntity entity
  )
  {
    return new()
    {
      CurrentState = entity.CurrentState,
      NetworkUserInvoiceId = entity.NetworkUserInvoiceId,
      BillId = entity.BillId,
      AbortReason = entity.AbortReason
    };
  }

  public static NetworkUserInvoiceStateEntity ToEntity(
    this NetworkUserInvoiceStateModel model
  )
  {
    return new()
    {
      CurrentState = model.CurrentState,
      NetworkUserInvoiceId = model.NetworkUserInvoiceId,
      BillId = model.BillId,
      AbortReason = model.AbortReason
    };
  }
}
