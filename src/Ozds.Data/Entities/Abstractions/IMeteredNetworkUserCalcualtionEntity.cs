namespace Ozds.Data.Entities.Abstractions;

public interface IMeteredNetworkUserCalculationEntity
  : INetworkUserCalculationEntity
{
  public decimal UsageFeeTotal_EUR { get; }

  public decimal SupplyFeeTotal_EUR { get; }
}
