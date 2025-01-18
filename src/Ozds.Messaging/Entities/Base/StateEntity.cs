using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Messaging.Entities.Abstractions;

namespace Ozds.Messaging.Entities.Base;

public abstract class StateEntity : Entity, IStateEntity
{
  public string CurrentState { get; set; } = default!;

  public uint RowVersion { get; set; }

  public Guid CorrelationId { get; set; } = Guid.Empty;
}

public static class StateEntityExtensions
{
  public static void ConfigureEntity(this EntityTypeBuilder entity)
  {
    entity
      .Property(nameof(StateEntity.RowVersion))
      .HasColumnName("xmin")
      .HasColumnType("xid")
      .IsRowVersion();
  }
}
