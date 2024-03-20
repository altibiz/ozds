using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

public abstract class AggregateEntity : MeasurementEntity
{
  public long AggregateCount { get; set; }

  [NotMapped]
  public abstract TimeSpan Interval { get; }
}
