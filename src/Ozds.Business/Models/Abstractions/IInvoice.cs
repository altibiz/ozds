using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Abstractions;

public interface IInvoice : IValidatableObject, IModel
{
  DateTimeOffset IssuedOn { get; }

  string? IssuedById { get; }

  DateTimeOffset FromDate { get; }

  DateTimeOffset ToDate { get; }
}
