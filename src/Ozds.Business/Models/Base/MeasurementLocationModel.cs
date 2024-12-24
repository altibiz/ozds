using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class MeasurementLocationModel :
  AuditableModel,
  IMeasurementLocation
{
  [Required]
  public required string MeterId { get; set; }

  [Required]
  public required string Kind { get; set; }
}
