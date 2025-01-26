namespace Ozds.Business.Models.Abstractions;

public interface INetworkUserCatalogue : ICatalogue
{
  public decimal MeterFeePrice_EUR { get; }
}
