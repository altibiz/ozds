using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RepresentativeEventModel : EventModel
{
  [Required] public required string RepresentativeId { get; set; }
}
