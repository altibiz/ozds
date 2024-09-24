using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class
  EventModelEntityConverter : ModelEntityConverter<EventModel, EventEntity>
{
  protected override EventEntity ToEntity(EventModel model)
  {
    return model.ToEntity();
  }

  protected override EventModel ToModel(EventEntity entity)
  {
    return entity.ToModel();
  }
}

public static class EventModelEntityConverterExtensions
{
  public static EventModel ToModel(this EventEntity entity)
  {
    if (entity is SystemAuditEventEntity systemAuditEventEntity)
    {
      return systemAuditEventEntity.ToModel();
    }

    if (entity is SystemEventEntity systemEventEntity)
    {
      return systemEventEntity.ToModel();
    }

    if (entity is RepresentativeAuditEventEntity representativeAuditEventEntity)
    {
      return representativeAuditEventEntity.ToModel();
    }

    if (entity is RepresentativeEventEntity representativeEventEntity)
    {
      return representativeEventEntity.ToModel();
    }

    if (entity is MessengerEventEntity messengerEventEntity)
    {
      return messengerEventEntity.ToModel();
    }

    if (entity is AuditEventEntity auditEventEntity)
    {
      return auditEventEntity.ToModel();
    }

    throw new NotImplementedException();
  }

  public static EventEntity ToEntity(this EventModel model)
  {
    if (model is SystemAuditEventModel systemAuditEventModel)
    {
      return systemAuditEventModel.ToEntity();
    }

    if (model is SystemEventModel systemEventModel)
    {
      return systemEventModel.ToEntity();
    }

    if (model is RepresentativeAuditEventModel representativeAuditEventModel)
    {
      return representativeAuditEventModel.ToEntity();
    }

    if (model is RepresentativeEventModel representativeEventModel)
    {
      return representativeEventModel.ToEntity();
    }

    if (model is MessengerEventModel messengerEventModel)
    {
      return messengerEventModel.ToEntity();
    }

    if (model is AuditEventModel auditEventModel)
    {
      return auditEventModel.ToEntity();
    }

    throw new NotImplementedException();
  }
}
