using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MessengerEntity : AuditableEntity, ICustomIdentifiableEntity
{
  private long _locationId;

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    set { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;
  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;

  public virtual ICollection<MessengerEventEntity> Events { get; set; } =
    default!;

  public virtual ICollection<MessengerNotificationEntity>
    InactivityNotifications { get; set; } = default!;

  public PeriodEntity MaxInactivityPeriod { get; set; } = default!;

  public PeriodEntity PushDelayPeriod { get; set; } = default!;

  public string Kind { get; set; } = default!;
}

public class
  MessengerEntityTypeConfiguration : EntityTypeHierarchyConfiguration<
  MessengerEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("messengers")
      .HasDiscriminator<string>(nameof(MessengerEntity.Kind));

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

    builder
      .HasMany(nameof(MessengerEntity.InactivityNotifications))
      .WithOne(nameof(MessengerNotificationEntity.Messenger));

    builder.Ignore(nameof(MessengerEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");

    builder.ComplexProperty(nameof(MessengerEntity.MaxInactivityPeriod));
    builder.ComplexProperty(nameof(MessengerEntity.PushDelayPeriod));
  }
}
