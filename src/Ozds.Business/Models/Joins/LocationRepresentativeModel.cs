using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Joins;

public class LocationRepresentativeModel : JoinModel
{
  [Required]
  public required string LocationId { get; set; } = default!;

  [Required]
  public required string RepresentativeId { get; set; } = default!;
}
