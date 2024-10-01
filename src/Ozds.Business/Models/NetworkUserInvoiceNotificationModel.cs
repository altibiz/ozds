using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class NetworkUserInvoiceNotificationModel : NotificationModel
{
  [Required]
  public required string InvoiceId { get; set; } = default!;
}
