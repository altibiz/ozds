using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Messaging.Entities.Abstractions;

namespace Ozds.Messaging.Entities.Base;

public abstract class Entity : IEntity
{
  public string CurrentState { get; set; } = default!;

  public uint RowVersion { get; set; }

  public Guid CorrelationId { get; set; } = Guid.Empty;
}

public static class EntityExtensions
{
  public static void ConfigureEntity(this EntityTypeBuilder entity)
  {
    entity
      .Property(nameof(IEntity.RowVersion))
      .HasColumnName("xmin")
      .HasColumnType("xid")
      .IsRowVersion();
  }
}
