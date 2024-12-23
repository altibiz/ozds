using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Test.Extensions;

public static class CastleProxyExtensions
{
  public static UnproxyBuilder<TProxy> Unproxy<TProxy>(
    this TProxy proxy)
    where TProxy : class, IEntity
  {
    return new UnproxyBuilder<TProxy>(proxy);
  }

  public static EnumerableUnproxyBuilder<TProxy>
    Unproxy<TProxy>(this IEnumerable<TProxy> proxies)
    where TProxy : class, IEntity
  {
    return new EnumerableUnproxyBuilder<TProxy>(proxies);
  }

  public class UnproxyBuilder<TProxy>(
    TProxy proxy
  )
    where TProxy : class, IEntity
  {
    public TProxy Into() => Into<TProxy>();

    public TEntity Into<TEntity>()
      where TEntity : class, TProxy
    {
      var proxyType = proxy.GetType();
      if (Castle.DynamicProxy.ProxyUtil.IsProxyType(proxyType))
      {
        try
        {
          var instanceField = proxyType.GetField(
            "__target",
            BindingFlags.NonPublic | BindingFlags.Instance);
          var fieldValue = instanceField!.GetValue(proxy);
          return (fieldValue as TEntity)!;
        }
        catch (RuntimeBinderException)
        {
          return (proxy as TEntity)!;
        }
      }
      return (proxy as TEntity)!;
    }
  }

  public class EnumerableUnproxyBuilder<TProxy>(
    IEnumerable<TProxy> proxies
  )
    where TProxy : class, IEntity
  {
    public IEnumerable<TProxy> Into() => Into<TProxy>();

    public IEnumerable<TEntity> Into<TEntity>()
      where TEntity : class, TProxy
    {
      if (!proxies.Any())
      {
        return Enumerable.Empty<TEntity>();
      }
      Type proxyType = proxies.First().GetType();
      if (Castle.DynamicProxy.ProxyUtil.IsProxyType(proxyType))
      {
        var unproxied = proxies
          .Select(proxy => proxy
            .Unproxy()
            .Into<TEntity>());
        return unproxied;
      }
      return proxies.Cast<TEntity>();
    }
  }
}
