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
    object? parameters = null
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
    return navigationManager.BasedHref(uri);
  }

  public static string BasedHref(
    this NavigationManager navigationManager,
    string uri
  )
  {
    var @base = new Uri(navigationManager.BaseUri).AbsolutePath;
    return @base + uri.TrimStart('/');
  }
}
