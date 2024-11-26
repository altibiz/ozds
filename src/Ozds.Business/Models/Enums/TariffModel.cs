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
    TariffModel? tariff
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
