using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
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

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    _ = optionsBuilder.UseNpgsql(
      "Server=localhost;Port=5432;User Id=ozds;Password=ozds;Database=ozds");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    _ = modelBuilder.HasPostgresExtension("timescaledb");

    CreateEnumPropertyTypes(modelBuilder, typeof(IdEntity));
    CreateEntitiesWithBase(
      modelBuilder,
      typeof(IdEntity),
      entity => { _ = entity.HasKey(nameof(IdEntity.Id)); }
    );

    CreateEnumPropertyTypes(modelBuilder, typeof(MeasurementEntity));
    CreateEntitiesWithBase(
      modelBuilder,
      typeof(MeasurementEntity),
      entity =>
      {
        _ = entity.HasKey(
          nameof(MeasurementEntity.Source),
          nameof(MeasurementEntity.Timestamp)
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
            new[] { modelBuilder, null, null, null }
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
