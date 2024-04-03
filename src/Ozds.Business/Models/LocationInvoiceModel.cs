using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class LocationInvoiceModel : InvoiceModel
{
  [Required] public required string LocationId { get; set; }
}
