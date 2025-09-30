using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class MessengerEventEntity : EventEntity
{
  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;
}

public class
  MessengerEventEntityTypeConfiguration : EntityTypeConfiguration<
  MessengerEventEntity>
{
  public override void Configure(
    EntityTypeBuilder<MessengerEventEntity> builder)
  {
    builder
      .HasOne(nameof(MessengerEventEntity.Messenger))
      .WithMany(nameof(MessengerEntity.Events))
      .HasForeignKey(nameof(MessengerEventEntity.MessengerId));
  }
}
