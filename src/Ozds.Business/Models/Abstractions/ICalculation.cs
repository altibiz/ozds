using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public interface ICalculation : IValidatableObject, IReadonly, IIdentifiable
{
  public DateTimeOffset IssuedOn { get; }
  public string? IssuedById { get; }
  public DateTimeOffset FromDate { get; }
  public DateTimeOffset ToDate { get; }
  public string MeterId { get; }
  public IMeter ArchivedMeter { get; }
  public decimal Total_EUR { get; }
}
