using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity, ICustomIdentifiableEntity
{
  public RoleEntity Role { get; set; }

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } =
    default!;

  public virtual ICollection<NetworkUserRepresentativeEntity>
    NetworkUserRepresentatives { get; set; } = default!;

  public virtual ICollection<LocationEntity> Locations { get; set; } = default!;

  public virtual ICollection<LocationRepresentativeEntity>
    LocationRepresentatives { get; set; } = default!;

  public virtual ICollection<RepresentativeEventEntity> Events { get; set; } =
    default!;

  public virtual ICollection<RepresentativeAuditEventEntity> AuditEvents
  {
    get;
    set;
  } =
    default!;

  public virtual ICollection<NotificationRecipientEntity>
    NotificationRecipients { get; set; } = default!;

  public virtual ICollection<NotificationEntity> Notifications { get; set; } =
    default!;

  public virtual ICollection<ResolvableNotificationEntity>
    ResolvableNotifications { get; set; } = default!;

  public PhysicalPersonEntity PhysicalPerson { get; set; } = default!;

  public List<TopicEntity> Topics { get; set; } = default!;
}

public class
  RepresentativeEntityTypeConfiguration : EntityTypeConfiguration<
  RepresentativeEntity>
{
  public override void Configure(
    EntityTypeBuilder<RepresentativeEntity> builder)
  {
    builder.ComplexProperty(nameof(RepresentativeEntity.PhysicalPerson));
  }
}
