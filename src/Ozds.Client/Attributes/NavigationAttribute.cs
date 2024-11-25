using Ozds.Business.Models.Enums;

namespace Ozds.Client.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NavigationAttribute : Attribute
{
  public int Order { get; set; }

  public required string Title { get; set; }

  public bool IsVisible { get; set; } = true;

  public string? RouteValue { get; set; }

  public string? Icon { get; set; }

  public Type[] Parents { get; set; } = [];

  public RoleModel[] Allows { get; set; } = Array.Empty<RoleModel>();

  public RoleModel[] Disallows { get; set; } = Array.Empty<RoleModel>();

  public string? ParentTitle { get; set; }
}
