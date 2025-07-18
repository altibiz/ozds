using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class MeterNotificationModel : ResolvableNotificationModel
{
  [Required]
  public required string MeterId { get; set; } = default!;
}
