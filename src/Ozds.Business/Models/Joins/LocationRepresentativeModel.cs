using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class LocationRepresentativeModel : AuditableJoinModel
{
  public override string LeftId
  {
    get { return LocationId; }
    set { LocationId = value; }
  }

  public override Type LeftType
  {
    get { return typeof(LocationModel); }
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
  public required string LocationId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;
}
