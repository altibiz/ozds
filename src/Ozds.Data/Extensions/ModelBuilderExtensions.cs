using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public interface IModelConfiguration
{
  void Configure(ModelBuilder modelBuilder);
}

public static class ModelBuilderExtensions
{
  public static ModelBuilder ApplyModelConfigurationsFromAssembly(
    this ModelBuilder modelBuilder,
    Assembly assembly
  ) =>
    assembly
      .GetTypes()
      .Where(type => type
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
