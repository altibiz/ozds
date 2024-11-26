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
    DuplexModel? duplex
  )
  {
    return duplex switch
    {
      DuplexModel.Any => phasic.DuplexAny(),
      DuplexModel.Net => phasic.DuplexNet(),
      DuplexModel.Import => phasic.DuplexImport(),
      DuplexModel.Export => phasic.DuplexExport(),
      _ => phasic.DuplexImport()
    };
  }
}
