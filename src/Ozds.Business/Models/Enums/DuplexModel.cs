using Ozds.Business.Math;

namespace Ozds.Business.Models.Enums;

public enum DuplexModel
{
  Any,
  Net,
  Import,
  Export
}

public static class DuplexModelExtensions
{
  public static PhasicMeasure<decimal> GetMeasure(
    this DuplexMeasure<decimal> phasic,
    DuplexModel? duplex,
    MeasureModel? measure
  )
  {
    return duplex switch
    {
      DuplexModel.Any => phasic.DuplexAny(),
      DuplexModel.Net => phasic.DuplexNet(),
      DuplexModel.Import => phasic.DuplexImport(),
      DuplexModel.Export => phasic.DuplexExport(),
      _ => measure switch
      {
        MeasureModel.Voltage
          or MeasureModel.Current => phasic.DuplexAny(),
        MeasureModel.ActivePower
          or MeasureModel.ReactivePower
          or MeasureModel.ApparentPower => phasic.DuplexNet(),
        MeasureModel.ActiveEnergy
          or MeasureModel.ReactiveEnergy
          or MeasureModel.ApparentEnergy => phasic.DuplexImport(),
        _ => phasic.DuplexAny()
      }
    };
  }
}
