namespace Ozds.Business.Models.Abstractions;

public interface ICalculation : IFinancial
{
  public string MeterId { get; }

  public IMeter ArchivedMeter { get; }
}
