using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Abstractions;

public interface ICalculationEntity : IFinancialEntity
{
  public string MeterId { get; }

  public MeterEntity ArchivedMeter { get; }
}
