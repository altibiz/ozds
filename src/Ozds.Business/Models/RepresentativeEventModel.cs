using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RepresentativeEventModel : EventModel
{
  [Required]
  public required string RepresentativeId { get; set; }
}
