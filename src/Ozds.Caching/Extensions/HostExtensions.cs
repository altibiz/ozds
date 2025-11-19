using Altibiz.DependencyInjection.Extensions;
using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Cache.Implementations;
using Ozds.Caching.Mutations.Abstractions;
using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Options;
using Ozds.Caching.Profiles;
using Ozds.Caching.Queries.Abstractions;
using Ozds.Caching.Reactors.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsCaching(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddCache();
    builder.AddObservers();
    builder.AddReactors();
    builder.AddQueries();
    builder.AddMutations();
    builder.AddProfiles();
    builder.AddReflection();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsCachingOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddCache(
    this IHostApplicationBuilder builder
  )
  {
    var connectionString = ConfigureOzdsCachingOptions
      .ConnectionString(builder.Configuration);
    if (connectionString.StartsWith("memory://"))
    {
      builder.Services.AddMemoryCache();
      builder.Services.AddTransient(
        typeof(ICache),
        typeof(InMemoryCache<object>));
      builder.Services.AddTransient(
        typeof(ICache<>),
        typeof(InMemoryCache<>));
      builder.Services.AddTransient(
        typeof(IConfigurableCache),
        typeof(InMemoryCache<object>));
      builder.Services.AddTransient(
        typeof(IConfigurableCache<>),
        typeof(InMemoryCache<>));
      builder.Services.AddTransient(
        typeof(IPolicyCache),
        typeof(InMemoryCache<object>));
    }
    else
    {
      throw new InvalidOperationException(
        "Only memory cache is supported");
    }

    builder.Services.AddSingleton(
      typeof(ICacheFactory),
      typeof(CacheFactory));
    builder.Services.AddSingleton(
      typeof(IPolicyCacheFactory),
      typeof(CacheFactory));

    return builder;
  }

  private static IHostApplicationBuilder AddObservers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IPublisher));
    builder.Services.AddSingletonAssignableTo(typeof(ISubscriber));
    return builder;
  }

  private static IHostApplicationBuilder AddReactors(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IReactor));
    builder.Services.AddScopedAssignableTo(typeof(IReactorHandler));
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo<IQueries>();
    return builder;
  }

  private static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo<IMutations>();
    return builder;
  }

  private static IHostApplicationBuilder AddProfiles(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingleton<ProfileBuilder>();
    builder.Services.AddSingleton(
      services =>
        services.GetRequiredService<ProfileBuilder>()
          .Build(typeof(HostExtensions).Assembly));
    return builder;
  }

  private static IHostApplicationBuilder AddReflection(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingleton<EntityReflector>();
    return builder;
  }
}
