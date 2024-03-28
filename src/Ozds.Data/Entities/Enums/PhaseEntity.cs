using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum PhaseEntity
{
  L1,
  L2,
  L3
}

public class PhaseEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<PhaseEntity>();
  }
}
