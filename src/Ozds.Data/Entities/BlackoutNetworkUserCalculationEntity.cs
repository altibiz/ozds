using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class BlackoutNetworkUserCalculationEntity : NetworkUserCalculationEntity
{
  protected long _usageNetworkUserCatalogueId;

  public string UsageNetworkUserCatalogueId
  {
    get { return _usageNetworkUserCatalogueId.ToString(); }
    set { _usageNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual NetworkUserCatalogueEntity UsageNetworkUserCatalogue { get; set; } =
    default!;

  public NetworkUserCatalogueEntity ArchivedUsageNetworkUserCatalogue { get; set; } =
    default!;
}

public class BlackoutNetworkUserCalculationEntityTypeConfiguration
  : EntityTypeConfiguration<BlackoutNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlackoutNetworkUserCalculationEntity> builder
  )
  {
    builder
      .HasOne(
        nameof(BlackoutNetworkUserCalculationEntity.UsageNetworkUserCatalogue)
      )
      .WithMany()
      .HasForeignKey("_usageNetworkUserCatalogueId");

    builder.Ignore(
      nameof(BlackoutNetworkUserCalculationEntity.UsageNetworkUserCatalogueId)
    );
    builder
      .Property("_usageNetworkUserCatalogueId")
      .HasColumnName("usage_network_user_catalogue_id");

    builder.ArchivedProperty(
      nameof(
        BlackoutNetworkUserCalculationEntity.ArchivedUsageNetworkUserCatalogue
      )
    );
  }
}
