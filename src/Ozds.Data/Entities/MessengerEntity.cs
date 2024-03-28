using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

// TODO: settings for the messenger

namespace Ozds.Data.Entities;

public class MessengerEntity : AuditableEntity
{
  public string LocationId { get; set; } = default!;
  public virtual LocationEntity Location { get; set; } = default!;
  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;
  public virtual ICollection<MessengerEventEntity> Events { get; set; } = default!;
}

public class MessengerEntityTypeConfiguration : EntityTypeConfiguration<MessengerEntity>
{
  public override void Configure(EntityTypeBuilder<MessengerEntity> builder)
  {
    builder
      .HasOne(nameof(MessengerEntity.Location))
      .WithMany(nameof(LocationEntity.Messengers))
      .HasForeignKey(nameof(MessengerEntity.LocationId));

    builder
      .HasMany(nameof(MessengerEntity.Meters))
      .WithOne(nameof(MeterEntity.Messenger));

    builder
      .HasMany(nameof(MessengerEntity.Events))
      .WithOne(nameof(MessengerEventEntity.Messenger));

    builder
      .Property(nameof(MessengerEntity.LocationId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
