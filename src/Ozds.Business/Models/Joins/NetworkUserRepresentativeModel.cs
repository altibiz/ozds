using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class NetworkUserRepresentativeModel : AuditableJoinModel
{
  public override string LeftId
  {
    get { return NetworkUserId; }
    set { NetworkUserId = value; }
  }

  public override Type LeftType
  {
    get { return typeof(NetworkUserModel); }
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
  public required string NetworkUserId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;
}
