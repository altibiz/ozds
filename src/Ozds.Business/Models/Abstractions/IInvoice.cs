namespace Ozds.Business.Models.Abstractions;

public interface IInvoice : IIdentifiable, IReadonly
{
  DateTimeOffset IssuedOn { get; }

  string? IssuedById { get; }

  DateTimeOffset FromDate { get; }

  DateTimeOffset ToDate { get; }

  decimal Total_EUR { get; }

  decimal TaxRate_Percent { get; }

  decimal Tax_EUR { get; }

  decimal TotalWithTax_EUR { get; }
}
