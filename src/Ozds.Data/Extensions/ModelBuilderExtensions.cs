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

public abstract class EntityTypeHierarchyConfiguration<TBase> : IModelConfiguration
  where TBase : class
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var configure = GetType()
                      .GetMethods()
                      .FirstOrDefault(m =>
                        m.Name == nameof(Configure) && m.IsGenericMethod)
                    ?? throw new InvalidOperationException("Method not found");
    var configureConcrete = GetType()
                      .GetMethods()
                      .FirstOrDefault(m =>
                        m.Name == nameof(ConfigureConcrete) && m.IsGenericMethod)
                    ?? throw new InvalidOperationException("Method not found");
    var configureAbstract = GetType()
                      .GetMethods()
                      .FirstOrDefault(m =>
                        m.Name == nameof(ConfigureAbstract) && m.IsGenericMethod)
                    ?? throw new InvalidOperationException("Method not found");
    var entity = modelBuilder.GetType().GetMethods()
                   .FirstOrDefault(m =>
                     m.Name == nameof(ModelBuilder.Entity) && m.IsGenericMethod)
                 ?? throw new InvalidOperationException("Method not found");
    _ = typeof(TBase).Assembly
      .GetTypes()
      .Where(type =>
        !type.IsGenericType &&
        type.IsClass &&
        (type.IsSubclassOf(typeof(TBase)) || type == typeof(TBase)))
      .Aggregate(modelBuilder, (modelBuilder, type) =>
      {
        configure
          .MakeGenericMethod(type)
          .Invoke(
            this,
            new[]
            {
              entity.MakeGenericMethod(type).Invoke(modelBuilder, null)
              ?? throw new InvalidOperationException("Entity not found")
            });
        if (type.IsAbstract)
        {
          configureAbstract
            .MakeGenericMethod(type)
            .Invoke(
              this,
              new[]
              {
                entity.MakeGenericMethod(type).Invoke(modelBuilder, null)
                ?? throw new InvalidOperationException("Entity not found")
              });
        }
        else
        {
          configureConcrete
            .MakeGenericMethod(type)
            .Invoke(
              this,
              new[]
              {
                entity.MakeGenericMethod(type).Invoke(modelBuilder, null)
                ?? throw new InvalidOperationException("Entity not found")
              });
        }
        return modelBuilder;
      });
  }
  public virtual void Configure<TEntity>(
    EntityTypeBuilder<TEntity> builder
  ) where TEntity : class, TBase
  {
  }

  public virtual void ConfigureAbstract<TAbstract>(
    EntityTypeBuilder<TAbstract> builder
  ) where TAbstract : class, TBase
  {
  }

  public virtual void ConfigureConcrete<TConcrete>(EntityTypeBuilder<TConcrete> builder)
    where TConcrete : class, TBase
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
      .Where(type => !type.IsAbstract && type
        .GetInterfaces()
        .Any(@interface => @interface == typeof(IModelConfiguration)))
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
