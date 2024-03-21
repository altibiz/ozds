using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("meters")]
public abstract class MeterEntity : IdEntity
{
  public virtual NetworkUserMeasurementLocationEntity MeasurementLocation { get; set; } = default!;
}
