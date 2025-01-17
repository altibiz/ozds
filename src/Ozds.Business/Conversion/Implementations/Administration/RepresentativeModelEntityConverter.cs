using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RepresentativeModelEntityConverter : ConcreteModelEntityConverter<
  RepresentativeModel, RepresentativeEntity>
{
  protected override RepresentativeEntity ToEntity(RepresentativeModel model)
  {
    return model.ToEntity();
  }

  protected override RepresentativeModel ToModel(RepresentativeEntity entity)
  {
    return entity.ToModel();
  }
}

public static class RepresentativeModelEntityConverterExtensions
{
  public static RepresentativeEntity ToEntity(this RepresentativeModel model)
  {
    return new RepresentativeEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      Role = model.Role.ToEntity(),
      PhysicalPerson = model.PhysicalPerson.ToEntity(),
      Topics = model.Topics.Select(x => x.ToEntity()).ToList()
    };
  }

  public static RepresentativeModel ToModel(this RepresentativeEntity entity)
  {
    return new RepresentativeModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      Role = entity.Role.ToModel(),
      PhysicalPerson = entity.PhysicalPerson.ToModel(),
      Topics = entity.Topics.Select(x => x.ToModel()).ToList()
    };
  }
}
