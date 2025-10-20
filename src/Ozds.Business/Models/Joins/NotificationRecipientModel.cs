using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class NotificationRecipientModel : JoinModel
{
  public override string LeftId
  {
    get { return NotificationId; }
    set { NotificationId = value; }
  }

  public override Type LeftType
  {
    get { return typeof(NotificationModel); }
  }

  public override string RightId
  {
    get { return RepresentativeId; }
    set { RepresentativeId = value; }
  }

  public override Type RightType
  {
    get { return typeof(RepresentativeModel); }
  }

  [Required]
  public required string NotificationId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;

  public DateTimeOffset? SeenOn { get; set; } = default!;
}
