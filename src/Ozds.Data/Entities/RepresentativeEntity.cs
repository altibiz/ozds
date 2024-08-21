using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity
{
  protected readonly string _stringId = default!;

  public override string Id
  {
    get { return _stringId; }
    init { _stringId = value; }
  }

  public RoleEntity Role { get; set; }

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } =
    default!;

  public virtual ICollection<LocationEntity> Locations { get; set; } = default!;

  public virtual ICollection<RepresentativeEventEntity> Events { get; set; } =
    default!;

  public virtual ICollection<RepresentativeAuditEventEntity> AuditEvents
  {
    get;
    set;
  } =
    default!;

  public virtual ICollection<NotificationEntity> Notifications { get; set; } = default!;

  public PhysicalPersonEntity PhysicalPerson { get; set; } = default!;
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
