using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  [Key] public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}

public class IdentifiableEntityTypeConfiguration : IEntityTypeConfiguration<IdentifiableEntity>
{
  public void Configure(EntityTypeBuilder<IdentifiableEntity> builder)
  {
    builder.UseTptMappingStrategy();
  }
}

