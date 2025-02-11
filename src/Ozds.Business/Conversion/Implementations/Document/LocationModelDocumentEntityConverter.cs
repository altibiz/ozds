using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class LocationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelDocumentEntityConverter<
  LocationModel,
  LocationEntity>
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    LocationModel model,
    LocationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Title = model.Title;
    entity.Id = model.Id;
    entity.LegalPerson = modelDocumentEntityConverter
      .ToEntity<LegalPersonEntity>(model.LegalPerson);
    entity.AltiBizSubProjectCode = model.AltiBizSubProjectCode;
  }
}
