using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities;

namespace Ozds.Data;

public class OzdsDbContext : DbContext
{
  private static readonly MethodInfo HasPostgresEnumMethod =
    typeof(NpgsqlModelBuilderExtensions)
      .GetMethods()
      .First(
        method =>
          method.IsGenericMethod
          && method.Name == nameof(NpgsqlModelBuilderExtensions.HasPostgresEnum)
      )
    ?? throw new InvalidOperationException("HasPostgresEnum method not found");

  public DbSet<AbbMeasurementEntity> AbbMeasurements { get; set; } = default!;

  public DbSet<SchneiderMeasurementEntity> SchneiderMeasurements { get; set; } =
    default!;

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql(
      "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasPostgresExtension("timescaledb");

    CreateEntitiesWithBase(
      modelBuilder,
      typeof(HypertableEntity),
      entity =>
      {
        entity.HasKey(
          nameof(HypertableEntity.Source),
          nameof(HypertableEntity.Timestamp)
        );

        entity.HasIndex(
          nameof(HypertableEntity.Source),
          nameof(HypertableEntity.Timestamp)
        );

        entity.HasIndex(
          nameof(HypertableEntity.Source),
          nameof(HypertableEntity.Milliseconds)
        );
      }
    );
  }

  protected void CreateEnumPropertyTypes(ModelBuilder modelBuilder, Type @base)
  {
    foreach (
      var entityType in GetType().Assembly
        .GetTypes()
        .Where(
          type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(@base)
        )
    )
    {
      foreach (var property in entityType.GetProperties())
      {
        if (property.PropertyType.IsEnum)
        {
          var enumType = property.PropertyType;
          var genericMethod = HasPostgresEnumMethod.MakeGenericMethod(enumType);
          genericMethod.Invoke(
            null,
            [modelBuilder, null, null, null]
          );
        }
      }
    }
  }

  protected void CreateEntitiesWithBase(
    ModelBuilder builder,
    Type @base,
    Action<EntityTypeBuilder>? configuration = null
  )
  {
    foreach (
      var entityType in GetType().Assembly
        .GetTypes()
        .Where(
          type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(@base)
        )
    )
    {
      var entityParameter = Expression.Parameter(entityType);
      configuration?.Invoke(builder.Entity(entityType));
    }
  }
}
