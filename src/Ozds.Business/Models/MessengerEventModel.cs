using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class MessengerEventModel : EventModel
{
  [Required] public required string MessengerId { get; set; }
}
