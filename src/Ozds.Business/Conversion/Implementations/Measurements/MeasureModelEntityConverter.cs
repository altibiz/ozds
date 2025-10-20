using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MeasureModelEntityConverter
  : ConcreteModelEntityConverter<MeasureModel, MeasureEntity>
{
  public override MeasureEntity ToEntity(MeasureModel model)
  {
    return model switch
    {
      MeasureModel.Current => MeasureEntity.Current,
      MeasureModel.Voltage => MeasureEntity.Voltage,
      MeasureModel.ActivePower => MeasureEntity.ActivePower,
      MeasureModel.ReactivePower => MeasureEntity.ReactivePower,
      MeasureModel.ApparentPower => MeasureEntity.ApparentPower,
      MeasureModel.ActiveEnergy => MeasureEntity.ActiveEnergy,
      MeasureModel.ReactiveEnergy => MeasureEntity.ReactiveEnergy,
      MeasureModel.ApparentEnergy => MeasureEntity.ApparentEnergy,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override MeasureModel ToModel(MeasureEntity entity)
  {
    return entity switch
    {
      MeasureEntity.Current => MeasureModel.Current,
      MeasureEntity.Voltage => MeasureModel.Voltage,
      MeasureEntity.ActivePower => MeasureModel.ActivePower,
      MeasureEntity.ReactivePower => MeasureModel.ReactivePower,
      MeasureEntity.ApparentPower => MeasureModel.ApparentPower,
      MeasureEntity.ActiveEnergy => MeasureModel.ActiveEnergy,
      MeasureEntity.ReactiveEnergy => MeasureModel.ReactiveEnergy,
      MeasureEntity.ApparentEnergy => MeasureModel.ApparentEnergy,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}
