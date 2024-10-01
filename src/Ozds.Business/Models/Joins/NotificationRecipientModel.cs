using System.ComponentModel.DataAnnotations;

namespace Ozds.Business.Models.Joins;

public class NotificationRecipientModel
{
  [Required]
  public required string NotificationId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;

  public DateTimeOffset? SeenOn { get; set; } = default!;
}
