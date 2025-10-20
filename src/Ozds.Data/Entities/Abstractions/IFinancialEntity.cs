namespace Ozds.Data.Entities.Abstractions;

public interface IFinancialEntity : IReadonlyEntity, IIdentifiableEntity
{
  public string? AuditingRepresentativeId { get; set; }

  public DateTimeOffset IssuedOn { get; set; }

  public string? IssuedById { get; set; }

  public DateTimeOffset FromDate { get; }

  public DateTimeOffset ToDate { get; }

  public decimal Total_EUR { get; }

  public decimal TaxRate_Percent { get; }

  public decimal Tax_EUR { get; }

  public decimal TotalWithTax_EUR { get; }

  public string Remark { get; }
}
