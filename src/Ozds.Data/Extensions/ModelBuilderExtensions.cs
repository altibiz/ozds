using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Extensions;

public interface IModelConfiguration
{
  void Configure(ModelBuilder modelBuilder);
}

public abstract class EntityTypeConfiguration<TEntity> : IModelConfiguration
  where TEntity : class
{
  public void Configure(ModelBuilder modelBuilder)
  {
    Configure(modelBuilder.Entity<TEntity>());
  }

  public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}

public abstract class
  EntityTypeHierarchyConfiguration<TBase> : IModelConfiguration
  where TBase : class
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var configure = GetType()
                      .GetMethods()
                      .FirstOrDefault(m => m.Name == nameof(Configure))
                    ?? throw new InvalidOperationException("Method not found");
    _ = typeof(TBase).Assembly
      .GetTypes()
      .Where(type =>
        !type.IsGenericType &&
        type.IsClass &&
        typeof(TBase).IsAssignableFrom(type))
      .OrderBy(type =>
      {
        var level = 0;
        for (
          var currentType = type.BaseType;
          currentType != null;
          currentType = currentType.BaseType)
        {
          level++;
        }

        return level;
      })
      .Aggregate(modelBuilder, (modelBuilder, type) =>
      {
        configure
          .Invoke(
            this,
            new object[]
            {
              modelBuilder,
              type
            });
        return modelBuilder;
      });
  }

  public virtual void Configure(ModelBuilder modelBuilder, Type entity)
  {
  }
}

public static class ModelBuilderExtensions
{
  public static ModelBuilder ApplyModelConfigurationsFromAssembly(
    this ModelBuilder modelBuilder,
    Assembly assembly
  )
  {
    return assembly
      .GetTypes()
      .Where(type => !type.IsAbstract &&
                     typeof(IModelConfiguration).IsAssignableFrom(type))
      .Aggregate(
        modelBuilder,
        (modelBuilder, type) =>
        {
          var config = Activator.CreateInstance(type) as IModelConfiguration;
          config?.Configure(modelBuilder);
          return modelBuilder;
        }
      );
  }
}
