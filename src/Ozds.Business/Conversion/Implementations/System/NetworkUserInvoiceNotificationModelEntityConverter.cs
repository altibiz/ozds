using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserInvoiceNotificationModelEntityConverter :
  ConcreteModelEntityConverter<
    NetworkUserInvoiceNotificationModel,
    NetworkUserInvoiceNotificationEntity>
{
  protected override NetworkUserInvoiceNotificationEntity ToEntity(
    NetworkUserInvoiceNotificationModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserInvoiceNotificationModel ToModel(
    NetworkUserInvoiceNotificationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NetworkUserInvoiceNotificationModelEntityConverterExtensions
{
  public static NetworkUserInvoiceNotificationEntity ToEntity(
    this NetworkUserInvoiceNotificationModel model)
  {
    return new NetworkUserInvoiceNotificationEntity
    {
      Id = model.Id,
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Timestamp = model.Timestamp,
      EventId = model.EventId,
      Topics = model.Topics.Select(x => x.ToEntity()).ToList(),
      InvoiceId = model.InvoiceId
    };
  }

  public static NetworkUserInvoiceNotificationModel ToModel(
    this NetworkUserInvoiceNotificationEntity entity)
  {
    return new NetworkUserInvoiceNotificationModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Summary = entity.Summary,
      Content = entity.Content,
      Timestamp = entity.Timestamp,
      EventId = entity.EventId,
      Topics = entity.Topics.Select(x => x.ToModel()).ToHashSet(),
      InvoiceId = entity.InvoiceId
    };
  }
}
