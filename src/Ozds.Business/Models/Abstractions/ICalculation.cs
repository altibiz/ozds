namespace Ozds.Business.Models.Abstractions;

public interface ICalculation : IFinancial
{
  public DateTimeOffset RequestedFromDate { get; }

  public DateTimeOffset RequestedToDate { get; }

  public DateTimeOffset MeteredFromDate { get; }

  public DateTimeOffset MeteredToDate { get; }
}
