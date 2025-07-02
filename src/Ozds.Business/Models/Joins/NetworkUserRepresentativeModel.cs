using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class NetworkUserRepresentativeModel : JoinModel
{
  [Required]
  public required string NetworkUserId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;
}
