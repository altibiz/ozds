using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Enums;

public enum TariffEntity
{
  T0,
  T1,
  T2
}

public class TariffEntityTypeConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresEnum<TariffEntity>();
  }
}

public static class TariffEntityExtensions
{
  public static string ColumnPart(
    this TariffEntity tariff
  ) => tariff switch
  {
    TariffEntity.T0 => "t0",
    TariffEntity.T1 => "t1",
    TariffEntity.T2 => "t2",
    _ => throw new ArgumentOutOfRangeException(nameof(tariff), tariff, null)
  };
}
