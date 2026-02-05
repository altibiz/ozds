using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class TariffModelEntityConverter
  : ConcreteModelEntityConverter<TariffModel, TariffEntity>
{
  public override TariffEntity ToEntity(TariffModel model)
  {
    return model switch
    {
      TariffModel.T0 => TariffEntity.T0,
      TariffModel.T1 => TariffEntity.T1,
      TariffModel.T2 => TariffEntity.T2,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null),
    };
  }

  public override TariffModel ToModel(TariffEntity entity)
  {
    return entity switch
    {
      TariffEntity.T0 => TariffModel.T0,
      TariffEntity.T1 => TariffModel.T1,
      TariffEntity.T2 => TariffModel.T2,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null),
    };
  }
}
