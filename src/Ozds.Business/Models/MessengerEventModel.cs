using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class MessengerEventModel : EventModel
{
  [Required]
  public required string MessengerId { get; set; }
}
