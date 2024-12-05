using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class CatalogueEntity : AuditableEntity, ICatalogueEntity
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
    if (entity == typeof(CatalogueEntity))
    {
      return;
    }

    _ = modelBuilder.Entity(entity);
  }
}
