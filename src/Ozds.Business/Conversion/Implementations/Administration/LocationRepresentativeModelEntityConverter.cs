using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class LocationRepresentativeEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  LocationRepresentativeModel,
  JoinModel,
  LocationRepresentativeEntity,
  JoinEntity>(serviceProvider)
{
  public override void InitializeEntity(
    LocationRepresentativeModel model,
    LocationRepresentativeEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.LocationId = model.LocationId;
    entity.RepresentativeId = model.RepresentativeId;
  }

  public override void InitializeModel(
    LocationRepresentativeEntity entity,
    LocationRepresentativeModel model)
  {
    base.InitializeModel(entity, model);
    model.LocationId = entity.LocationId;
    model.RepresentativeId = entity.RepresentativeId;
  }
}
