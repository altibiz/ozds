using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class PhysicalPersonModelEntityConverter
  : ConcreteModelEntityConverter<PhysicalPersonModel, PhysicalPersonEntity>
{
  public override void InitializeEntity(
    PhysicalPersonModel model,
    PhysicalPersonEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Name = model.Name;
    entity.Email = model.Email;
    entity.PhoneNumber = model.PhoneNumber;
  }

  public override void InitializeModel(
    PhysicalPersonEntity entity,
    PhysicalPersonModel model)
  {
    base.InitializeModel(entity, model);
    model.Name = entity.Name;
    model.Email = entity.Email;
    model.PhoneNumber = entity.PhoneNumber;
  }
}
