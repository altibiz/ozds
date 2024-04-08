using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class LocationMeasurementLocationModel : MeasurementLocationModel
{
  [Required] public required string LocationId { get; set; }
}
