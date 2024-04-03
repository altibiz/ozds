using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

// TODO: settings for the messenger

namespace Ozds.Data.Entities;

public class MessengerEntity : AuditableEntity
{
  protected readonly string _stringId = default!;

  private long _locationId;

  public override string Id
  {
    get { return _stringId; }
    init { _stringId = value; }
  }

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    init { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;
  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;

  public virtual ICollection<MessengerEventEntity> Events { get; set; } =
    default!;
}

public class
  MessengerEntityTypeConfiguration : EntityTypeConfiguration<MessengerEntity>
{
  public override void Configure(EntityTypeBuilder<MessengerEntity> builder)
  {
    builder
      .HasOne(nameof(MessengerEntity.Location))
      .WithMany(nameof(LocationEntity.Messengers))
      .HasForeignKey("_locationId");

    builder
      .HasMany(nameof(MessengerEntity.Meters))
      .WithOne(nameof(MeterEntity.Messenger));

    builder
      .HasMany(nameof(MessengerEntity.Events))
      .WithOne(nameof(MessengerEventEntity.Messenger));

    builder.Ignore(nameof(MessengerEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");
  }
}
