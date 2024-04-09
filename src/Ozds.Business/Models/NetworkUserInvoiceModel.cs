using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserInvoiceModel : InvoiceModel
{
  [Required] public required string NetworkUserId { get; set; }

  [Required] public required LocationModel ArchivedLocation { get; set; } = default!;

  [Required] public required NetworkUserModel ArchivedNetworkUser { get; set; } = default!;
}
