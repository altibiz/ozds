using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("meters")]
public abstract class MeterEntity : IdEntity
{
  public virtual NetworkUserMeasurementLocationEntity? NetworkUserMeasurementLocation { get; set; } = default!;

  public virtual LocationMeasurementLocationEntity? LocationMeasurementLocation { get; set; } = default!;
}
