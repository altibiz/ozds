using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Ozds.Caching.Policies;
using Ozds.Caching.Policies.Abstractions;

namespace Ozds.Caching.Configuration;

public class CacheConfigurationBuilder
{
  private readonly JsonSerializerOptions jsonSerializerOptions = new();

  private readonly List<IPolicy> policies = new();

  private CacheEntryConfiguration entry = new();

  public static CacheConfigurationBuilder Default
  {
    get { return new CacheConfigurationBuilder(); }
  }

  public CacheConfigurationBuilder WithDependencyTracking(
    Action<DependencyTrackingPolicyBuilder>? configure = null
  )
  {
    var builder = new DependencyTrackingPolicyBuilder();
    if (configure is not null)
    {
      configure(builder);
    }

    var policy = builder.Build();
    policies.Add(policy);
    return this;
  }

  public CacheConfigurationBuilder WithEntry(
    Action<CacheEntryConfigurationBuilder>? configure = null
  )
  {
    var builder = new CacheEntryConfigurationBuilder();
    if (configure is not null)
    {
      configure(builder);
    }

    entry = builder.Build();
    return this;
  }

  public CacheConfigurationBuilder WithReverseDependencyEviction(
    Action<ReverseDependencyEvictionPolicyBuilder>? configure = null
  )
  {
    var builder = new ReverseDependencyEvictionPolicyBuilder();
    if (configure is not null)
    {
      configure(builder);
    }

    var policy = builder.Build();
    policies.Add(policy);
    return this;
  }

  public CacheConfigurationBuilder WithIndirectReverseDependencyEvictionPolicy(
    IndirectReverseDependencyEvictionPolicyKeyResolver keyResolver,
    Type type
  )
  {
    var builder = new IndirectReverseDependencyEvictionPolicyBuilder(
      keyResolver,
      type
    );
    var policy = builder.Build();
    policies.Add(policy);
    return this;
  }

  public CacheConfigurationBuilder WithPolymorphicTypeHierarchy(
    Type root,
    Assembly? assembly = null,
    string? @namespace = null
  )
  {
    var assemblies = assembly is null
      ? AppDomain.CurrentDomain.GetAssemblies().ToList()
      : [assembly];

    IEnumerable<Type> WhereNamespace(IEnumerable<Type> types)
    {
      return @namespace is not null
        ? types.Where(type =>
          type.Namespace is not null && type.Namespace.StartsWith(@namespace)
        )
        : types;
    }

    var types = assemblies.SelectMany(type => WhereNamespace(type.GetTypes()));

    var baseTypes = types
      .Where(type =>
        types.Any(concreteType =>
          concreteType != type && concreteType.IsAssignableTo(type)
        )
      )
      .ToList();

    var subtypes = baseTypes.ToDictionary(
      type => type,
      root =>
        types
          .Where(type => !type.IsAbstract)
          .Where(type => type.IsAssignableTo(root))
    );

    void Modifier(JsonTypeInfo typeInfo)
    {
      if (!subtypes.TryGetValue(typeInfo.Type, out var typeSubtypes))
      {
        return;
      }

      typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
      {
        TypeDiscriminatorPropertyName = "$type",
        IgnoreUnrecognizedTypeDiscriminators = false,
        UnknownDerivedTypeHandling =
          JsonUnknownDerivedTypeHandling.FailSerialization,
      };

      foreach (var subtype in typeSubtypes)
      {
        typeInfo.PolymorphismOptions.DerivedTypes.Add(
          new JsonDerivedType(subtype, subtype.FullName ?? subtype.Name)
        );
      }
    }

    if (!jsonSerializerOptions.TypeInfoResolverChain.IsReadOnly)
    {
      jsonSerializerOptions.TypeInfoResolverChain.Add(
        new DefaultJsonTypeInfoResolver().WithAddedModifier(Modifier)
      );
    }
    else if (jsonSerializerOptions.TypeInfoResolver is not null)
    {
      jsonSerializerOptions.TypeInfoResolver =
        jsonSerializerOptions.TypeInfoResolver.WithAddedModifier(Modifier);
    }
    else
    {
      jsonSerializerOptions.TypeInfoResolver =
        new DefaultJsonTypeInfoResolver().WithAddedModifier(Modifier);
    }

    return this;
  }

  public CacheConfiguration Build()
  {
    return new CacheConfiguration
    {
      Policies = policies.ToList(),
      JsonSerializerOptions = jsonSerializerOptions,
      Entry = entry,
    };
  }
}

public static class CacheConfigurationExtensions
{
  public static CacheConfiguration Merge(
    this CacheConfiguration configuration,
    CacheConfiguration right
  )
  {
    var jsonSerializerOptions = new JsonSerializerOptions
    {
      TypeInfoResolver = right.JsonSerializerOptions.TypeInfoResolver
        is { } rightResolver
        ? configuration.JsonSerializerOptions.TypeInfoResolver
          is { } leftResolver
          ? new MergedTypeInfoResolver(leftResolver, rightResolver)
          : rightResolver
        : configuration.JsonSerializerOptions.TypeInfoResolver
          is { } leftResolver2
          ? leftResolver2
          : null,
    };

    foreach (
      var resolver in configuration.JsonSerializerOptions.TypeInfoResolverChain
    )
    {
      jsonSerializerOptions.TypeInfoResolverChain.Add(resolver);
    }

    foreach (var resolver in right.JsonSerializerOptions.TypeInfoResolverChain)
    {
      jsonSerializerOptions.TypeInfoResolverChain.Add(resolver);
    }

    var entry = new CacheEntryConfiguration
    {
      HardTtl =
        configuration.Entry.HardTtl < right.Entry.HardTtl
          ? configuration.Entry.HardTtl
          : right.Entry.HardTtl,
      SoftTtl =
        configuration.Entry.SoftTtl < right.Entry.SoftTtl
          ? configuration.Entry.SoftTtl
          : right.Entry.SoftTtl,
    };

    return new CacheConfiguration
    {
      Policies = configuration.Policies.Concat(right.Policies).ToList(),
      JsonSerializerOptions = jsonSerializerOptions,
      Entry = entry,
    };
  }

  public static CacheConfiguration Merge(
    this IEnumerable<CacheConfiguration> enumerable
  )
  {
    var result = enumerable
      .DefaultIfEmpty(CacheConfiguration.Default)
      .Aggregate((acc, next) => acc.Merge(next));

    return result;
  }

  private sealed class MergedTypeInfoResolver(
    IJsonTypeInfoResolver left,
    IJsonTypeInfoResolver right
  ) : IJsonTypeInfoResolver
  {
    public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options)
    {
      return right.GetTypeInfo(type, options)
        ?? left.GetTypeInfo(type, options);
    }
  }
}
