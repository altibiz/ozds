using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementLocationModel
  : TrackableModel,
    IMeasurementLocation
{
  [Required]
  public required string MeterId { get; set; }
}
