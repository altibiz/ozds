using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Extensions;

public interface IModelConfiguration
{
  void Configure(ModelBuilder modelBuilder);
}

public abstract class EntityTypeConfiguration<T> : IModelConfiguration
  where T : class
{
  public void Configure(ModelBuilder modelBuilder)
  {
    Configure(modelBuilder.Entity<T>());
  }

  public abstract void Configure(EntityTypeBuilder<T> builder);
}

public abstract class
  ConcreteHierarchyEntityTypeConfiguration<T> : IModelConfiguration
  where T : class
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var configure = GetType()
                      .GetMethods()
                      .FirstOrDefault(m =>
                        m.Name == nameof(Configure) && m.IsGenericMethod)
                    ?? throw new InvalidOperationException("Method not found");
    var entity = modelBuilder.GetType().GetMethods()
                   .FirstOrDefault(m =>
                     m.Name == nameof(ModelBuilder.Entity) && m.IsGenericMethod)
                 ?? throw new InvalidOperationException("Method not found");
    _ = typeof(T).Assembly
      .GetTypes()
      .Where(type =>
        !type.IsAbstract &&
        !type.IsGenericType &&
        type.IsClass &&
        (type.IsSubclassOf(typeof(T)) || type == typeof(T)))
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
        return modelBuilder;
      });
  }

  public abstract void Configure<U>(EntityTypeBuilder<U> builder)
    where U : class, T;
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
