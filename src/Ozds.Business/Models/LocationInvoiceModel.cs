using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class LocationInvoiceModel : InvoiceModel
{
  [Required] public required string LocationId { get; set; }
}
