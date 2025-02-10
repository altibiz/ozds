using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class NetworkUserModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : ConcreteModelDocumentEntityConverter<
  NetworkUserModel,
  NetworkUserEntity>
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    NetworkUserModel model,
    NetworkUserEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Title = model.Title;
    entity.Id = model.Id;
    entity.LegalPerson = modelDocumentEntityConverter
      .ToEntity<LegalPersonEntity>(model.LegalPerson);
  }
}
