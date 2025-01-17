using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class PeriodModelEntityConverter : ConcreteModelEntityConverter<PeriodModel,
  PeriodEntity>
{
  protected override PeriodEntity ToEntity(PeriodModel model)
  {
    return model.ToEntity();
  }

  protected override PeriodModel ToModel(PeriodEntity entity)
  {
    return entity.ToModel();
  }
}

public static class PeriodModelEntityConverterExtensions
{
  public static PeriodModel ToModel(this PeriodEntity entity)
  {
    return new PeriodModel
    {
      Duration = entity.Duration.ToModel(),
      Multiplier = entity.Multiplier
    };
  }

  public static PeriodEntity ToEntity(this PeriodModel model)
  {
    return new PeriodEntity
    {
      Duration = model.Duration.ToEntity(),
      Multiplier = model.Multiplier
    };
  }
}
