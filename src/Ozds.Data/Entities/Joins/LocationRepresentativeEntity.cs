using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Joins;

public class LocationRepresentativeEntity : IEntity
{
  private long _locationId;

  public string LocationId
  {
    get { return _locationId.ToString(); }
    set
    {
      _locationId = value is { } notNullValue
        ? long.Parse(notNullValue)
        : default;
    }
  }

  public string RepresentativeId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;
}

public class
  LocationRepresentativeEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var entity = modelBuilder.Entity<LocationEntity>();

    entity
      .HasMany(nameof(LocationEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.Locations))
      .UsingEntity(
        typeof(LocationRepresentativeEntity),
        configureLeft: l => l
          .HasOne(nameof(LocationRepresentativeEntity.Location))
          .WithMany(nameof(LocationEntity.LocationRepresentatives))
          .HasForeignKey("_locationId"),
        configureRight: r => r
          .HasOne(nameof(LocationRepresentativeEntity.Representative))
          .WithMany(nameof(RepresentativeEntity.LocationRepresentatives))
          .HasForeignKey(nameof(LocationRepresentativeEntity.RepresentativeId)),
        configureJoinEntityType: entity =>
        {
          entity.ToTable("location_representatives");

          entity.Ignore(nameof(LocationRepresentativeEntity.LocationId));
          entity
            .Property("_locationId")
            .HasColumnName("location_id");
        }
      );
  }
}
