using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserInvoiceStateModel : StateModel
{
  [Required]
  public required string NetworkUserInvoiceId { get; set; }

  public string? BillId { get; set; }

  public string? AbortReason { get; set; }
}
