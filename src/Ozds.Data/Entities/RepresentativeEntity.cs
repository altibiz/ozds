using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RepresentativeEntity : AuditableEntity
{
  private readonly string _representativeId = default!;

  public required override string Id { get => _representativeId; init => _representativeId = value; }

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

  public string Name { get; set; } = default!;

  public string SocialSecurityNumber { get; set; } = default!;

  public string Address { get; set; } = default!;

  public string PostalCode { get; set; } = default!;

  public string City { get; set; } = default!;

  public string Email { get; set; } = default!;

  public string PhoneNumber { get; set; } = default!;
}

public class
  RepresentativeEntityTypeConfiguration : EntityTypeConfiguration<
  RepresentativeEntity>
{
  public override void Configure(
    EntityTypeBuilder<RepresentativeEntity> builder)
  {
    builder.HasKey("_representativeId");
    builder.Ignore(nameof(RepresentativeEntity.Id));
    builder
      .Property("_representativeId")
      .ValueGeneratedNever()
      .HasColumnName("id")
      .HasColumnType("text");
  }
}
