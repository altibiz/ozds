namespace Ozds.Data.Entities.Abstractions;

public interface ICalculationEntity : IIdentifiableEntity, IReadonlyEntity
{
  public DateTimeOffset IssuedOn { get; }

  public string? IssuedById { get; }

  public DateTimeOffset FromDate { get; }

  public DateTimeOffset ToDate { get; }

  public string MeterId { get; }

  public decimal Total_EUR { get; }
}
