using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public class NotificationModel : IdentifiableModel, INotification
{
  [Required]
  public required string Summary { get; set; } = default!;

  [Required]
  public required string Content { get; set; } = default!;

  [Required]
  public required DateTimeOffset Timestamp { get; set; }

  public string? EventId { get; set; } = default!;

  [Required]
  public required TopicModel Topic { get; set; } = default!;
}
