using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

public abstract class CatalogueEntity : TrackableEntity, ICatalogueEntity
{
}

public class
  CatalogueEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    CatalogueEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    _ = modelBuilder.Entity(entity);
  }
}
