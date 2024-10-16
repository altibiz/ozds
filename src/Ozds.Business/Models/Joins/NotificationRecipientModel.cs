using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class NotificationRecipientModel : JoinModel
{
  [Required]
  public required string NotificationId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;

  public DateTimeOffset? SeenOn { get; set; } = default!;
}
