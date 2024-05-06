using Ozds.Business.Models.Enums;

namespace Ozds.Client.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NavigationAttribute : Attribute
{
  public string? Title { get; set; }

  public string? RouteValue { get; set; }

  public string? Icon { get; set; }

  public Type[] Parents { get; set; } = [];

  public RoleModel[] Allows { get; set; } = [];
}
