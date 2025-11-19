using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class DuplexModelCachingEntityConverter
  : ConcreteModelCachingEntityConverter<DuplexModel, DuplexEntity>
{
  public override DuplexEntity ToEntity(DuplexModel model)
  {
    return model switch
    {
      DuplexModel.Any => DuplexEntity.Any,
      DuplexModel.Net => DuplexEntity.Net,
      DuplexModel.Import => DuplexEntity.Import,
      DuplexModel.Export => DuplexEntity.Export,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override DuplexModel ToModel(DuplexEntity entity)
  {
    return entity switch
    {
      DuplexEntity.Any => DuplexModel.Any,
      DuplexEntity.Net => DuplexModel.Net,
      DuplexEntity.Import => DuplexModel.Import,
      DuplexEntity.Export => DuplexModel.Export,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}
