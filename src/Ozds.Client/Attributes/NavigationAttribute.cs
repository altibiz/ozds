using Ozds.Business.Models.Enums;

namespace Ozds.Client.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NavigationAttribute : Attribute
{
  public int Order { get; set; }

  public string? Title { get; set; }

  public bool IsVisible { get; set; } = true;

  public string? RouteValue { get; set; }

  public string? Icon { get; set; }

  public Type[] Parents { get; set; } = [];

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
  public RoleModel[] Allows { get; set; } = default!;
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
  public RoleModel[] Disallows { get; set; } = Array.Empty<RoleModel>();
}
