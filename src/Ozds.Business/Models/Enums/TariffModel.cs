using Ozds.Business.Math;

namespace Ozds.Business.Models.Enums;

public enum TariffModel
{
  T0,
  T1,
  T2
}

public static class TariffModelExtensions
{
  public static DuplexMeasure<decimal> GetMeasure(
    this TariffMeasure<decimal> measurement,
    TariffModel? tariff,
#pragma warning disable IDE0060 // Remove unused parameter
    MeasureModel? measure
#pragma warning restore IDE0060 // Remove unused parameter
  )
  {
    return tariff switch
    {
      TariffModel.T0 => measurement.TariffUnary(),
      TariffModel.T1 => measurement.TariffBinary().T1,
      TariffModel.T2 => measurement.TariffBinary().T2,
      _ => measurement.TariffUnary()
    };
  }
}
