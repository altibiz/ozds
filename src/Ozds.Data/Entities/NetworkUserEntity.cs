using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : AuditableEntity
{
  public virtual ICollection<RepresentativeEntity>
    Representatives
  { get; set; } = default!;

  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<NetworkUserMeasurementLocationEntity>
    MeasurementLocations
  { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceEntity> Invoices { get; set; } =
    default!;
}

public class NetworkUserEntityTypeConfiguration : EntityTypeConfiguration<NetworkUserEntity>
{
  public override void Configure(EntityTypeBuilder<NetworkUserEntity> builder)
  {
    builder
      .HasMany(e => e.Representatives)
      .WithMany(e => e.NetworkUsers);
  }
}
