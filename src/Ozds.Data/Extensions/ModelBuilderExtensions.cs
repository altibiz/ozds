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
  public abstract void Configure(EntityTypeBuilder<T> builder);

  public void Configure(ModelBuilder modelBuilder) =>
    Configure(modelBuilder.Entity<T>());
}

public abstract class InheritedEntityTypeConfiguration<T> : IModelConfiguration
  where T : class
{
  public abstract void Configure<U>(EntityTypeBuilder<U> builder) where U : class, T;

  public void Configure(ModelBuilder modelBuilder)
  {
    _ = typeof(T).Assembly
      .GetTypes()
      .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(T)))
      .Aggregate(modelBuilder, (modelBuilder, type) =>
      {
        var method = GetType().GetMethod(nameof(Configure), 1, new[] { typeof(EntityTypeBuilder<>).MakeGenericType(type) });
        method?.Invoke(this, new object[] { modelBuilder.Entity(type) });
        return modelBuilder;
      });
  }
}

public static class ModelBuilderExtensions
{
  public static ModelBuilder ApplyModelConfigurationsFromAssembly(
    this ModelBuilder modelBuilder,
    Assembly assembly
  ) =>
    assembly
      .GetTypes()
      .Where(type => !type.IsAbstract && type
        .GetInterfaces()
        .Any(@interface => @interface == typeof(IModelConfiguration)))
      .Aggregate(
        modelBuilder,
        (modelBuilder, type) =>
        {
          (Activator.CreateInstance(type) as IModelConfiguration)?.Configure(modelBuilder);
          return modelBuilder;
        }
      );
}
