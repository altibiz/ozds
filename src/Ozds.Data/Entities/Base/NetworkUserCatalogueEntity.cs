using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class NetworkUserCatalogueEntity : AuditableEntity
{
  public virtual ICollection<MeasurementLocationEntity>
    NetworkUserMeasurementLocations
  { get; set; } = default!;
}

public class NetworkUserCatalogueEntity<TNetworkUserCalculation>
  : NetworkUserCatalogueEntity
  where TNetworkUserCalculation : NetworkUserCalculationEntity
{
  public virtual ICollection<LocationEntity> Locations { get; set; } = default!;

  public virtual ICollection<TNetworkUserCalculation>
    NetworkUserCalculations
  { get; set; } =
    default!;
}

public class
  NetworkUserCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  NetworkUserCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<NetworkUserCatalogueEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("network_user_catalogues")
      .HasDiscriminator<string>("kind");
  }
}
