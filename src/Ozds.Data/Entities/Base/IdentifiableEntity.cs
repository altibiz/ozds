using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  [Key] public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}

public class IdentifiableEntityTypeConfiguration : EntityTypeConfiguration<IdentifiableEntity>
{
  public override void Configure(EntityTypeBuilder<IdentifiableEntity> builder)
  {
    builder.UseTpcMappingStrategy();
  }
}

