using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Joins;

public class NetworkUserRepresentativeEntity : IEntity
{
  private long _networkUserId;

  public string NetworkUserId
  {
    get { return _networkUserId.ToString(); }
    set
    {
      _networkUserId = value is { } notNullValue
        ? long.Parse(notNullValue)
        : default;
    }
  }

  public string RepresentativeId { get; set; } = default!;

  public virtual NetworkUserEntity NetworkUser { get; set; } = default!;

  public virtual RepresentativeEntity Representative { get; set; } = default!;
}

public class
  NetworkUserRepresentativeEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var entity = modelBuilder.Entity<NotificationEntity>();

    entity
      .HasMany(nameof(NetworkUserEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.NetworkUsers))
      .UsingEntity(
        typeof(NetworkUserRepresentativeEntity),
        configureLeft: l => l
          .HasOne(nameof(NetworkUserRepresentativeEntity.NetworkUser))
          .WithMany(nameof(NetworkUserEntity.NetworkUserRepresentatives))
          .HasForeignKey("_networkUserId"),
        configureRight: r => r
          .HasOne(nameof(NetworkUserRepresentativeEntity.Representative))
          .WithMany(nameof(RepresentativeEntity.NetworkUserRepresentatives))
          .HasForeignKey(nameof(NetworkUserRepresentativeEntity.RepresentativeId)),
        configureJoinEntityType: entity =>
        {
          entity.ToTable("network_user_representatives");

          entity.Ignore(nameof(NetworkUserRepresentativeEntity.NetworkUserId));
          entity
            .Property("_networkUserId")
            .HasColumnName("network_user_id");
        }
      );
  }
}
