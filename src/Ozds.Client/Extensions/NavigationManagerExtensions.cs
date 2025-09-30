using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;

namespace Ozds.Client.Extensions;

public static class NavigationManagerExtensions
{
  public static string PageHref(
    this NavigationManager navigationManager,
    TemplateBinderFactory templateBinderFactory,
    Type type,
    object? parameters = null,
    object? queryParameters = null
  )
  {
    var attribute = type.GetCustomAttribute<RouteAttribute>()
      ?? throw new InvalidOperationException(
        $"{type} is not decorated with {nameof(RouteAttribute)}");
    var route = attribute.Template;
    var template = TemplateParser.Parse(route);
    var pattern = RoutePatternFactory.Parse(route);
    var values = new RouteValueDictionary(parameters);
    var binder = templateBinderFactory
      .Create(template, new RouteValueDictionary(pattern.Defaults));
    var uri = binder.BindValues(values)
      ?? throw new InvalidOperationException(
        $"{type} has no route template");
    var query = Query(queryParameters);
    return navigationManager.BasedHref(uri) + query;
  }

  public static string BasedHref(
    this NavigationManager navigationManager,
    string uri
  )
  {
    var @base = new Uri(navigationManager.BaseUri).AbsolutePath;
    return @base + uri.TrimStart('/');
  }

  private static string Query(object? queryParameters = null)
  {
    var queryString = string.Empty;

    if (queryParameters is null)
    {
      return queryString;
    }

    if (queryParameters is IDictionary<string, object> dictionary)
    {
      foreach (var ((key, value), index) in dictionary.Select((x, i) => (x, i)))
      {
        if (value is null)
        {
          continue;
        }

        var valueString = value.ToString();
        if (string.IsNullOrWhiteSpace(valueString))
        {
          continue;
        }

        var separator = index == 0 ? "?" : "&";
        queryString +=
          separator
          + key
          + "="
          + Uri.EscapeDataString(valueString);
      }
    }
    else
    {
      foreach (var (queryParameter, index) in queryParameters
        .GetType()
        .GetProperties()
        .Select((x, i) => (x, i)))
      {
        var value = queryParameter.GetValue(queryParameters);
        if (value is null)
        {
          continue;
        }

        var valueString = value.ToString();
        if (string.IsNullOrWhiteSpace(valueString))
        {
          continue;
        }

        var separator = index == 0 ? "?" : "&";
        queryString +=
          separator
          + queryParameter.Name
          + "="
          + Uri.EscapeDataString(valueString);
      }
    }

    return queryString;
  }
}
