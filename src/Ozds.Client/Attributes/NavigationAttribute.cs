using System.Reflection;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Enums;

namespace Ozds.Client.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NavigationAttribute : Attribute
{
  public int Order { get; set; } = 1;

  public required string Title { get; set; }

  public string? Icon { get; set; }

  public Type? Parent { get; set; }

  public bool Developer { get; set; } = false;

  public required RoleModel[] Allows { get; set; }

  public static IEnumerable<NavigationDescriptor> GetNavigationDescriptors()
  {
    var descriptors = typeof(NavigationAttribute).Assembly
      .GetTypes()
      .Select(type => new
      {
        Type = type,
        Navigation = type.GetCustomAttributes<NavigationAttribute>(),
        Route = type.GetCustomAttributes<RouteAttribute>()
      })
      .Where(type => type.Navigation.Any() && type.Route.Any())
      .SelectMany(type => type.Navigation
        .SelectMany(navigation => type.Route
          .Select(route => new NavigationDescriptor
          {
            Type = type.Type,
            Navigation = navigation,
            Route = route
          })))
      .ToList();

    var lastLayer = descriptors
      .Where(x => x.Navigation.Parent == null)
      .ToList();
    var result = lastLayer.ToList();
    descriptors.RemoveAll(x => x.Navigation.Parent == null);
    while (descriptors.Count > 0)
    {
      var layer = new List<NavigationDescriptor>();
      foreach (var descriptor in descriptors.ToList())
      {
        if (lastLayer.Find(x => x.Type == descriptor.Navigation.Parent)
          is { } parent)
        {
          parent.Children.Add(descriptor);
          layer.Add(descriptor);
          descriptors.Remove(descriptor);
        }
      }
      lastLayer = layer;
    }

    return result;
  }

  public class NavigationDescriptor
  {
    public required Type Type { get; set; }

    public required NavigationAttribute Navigation { get; set; }

    public required RouteAttribute Route { get; set; }

    public List<NavigationDescriptor> Children { get; set; } = new();
  }
}
