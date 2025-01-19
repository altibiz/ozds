using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class PhaseModelEntityConverter
  : ConcreteModelEntityConverter<PhaseModel, PhaseEntity>
{
  public override PhaseEntity ToEntity(PhaseModel model)
  {
    return model switch
    {
      PhaseModel.L1 => PhaseEntity.L1,
      PhaseModel.L2 => PhaseEntity.L2,
      PhaseModel.L3 => PhaseEntity.L3,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override PhaseModel ToModel(PhaseEntity entity)
  {
    return entity switch
    {
      PhaseEntity.L1 => PhaseModel.L1,
      PhaseEntity.L2 => PhaseModel.L2,
      PhaseEntity.L3 => PhaseModel.L3,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}
