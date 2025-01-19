using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class CatalogueEntity : AuditableEntity, ICatalogueEntity
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
