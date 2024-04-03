using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class NetworkUserMeasurementLocationModel : MeasurementLocationModel
{
  [Required] public required string NetworkUserId { get; set; }
}
