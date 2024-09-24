using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Base;

public class OzdsOwningLayoutComponentBase : OzdsOwningComponentBase
{
  [Parameter]
  public RenderFragment? Body { get; set; }

  [DynamicDependency(
    DynamicallyAccessedMemberTypes.All,
    typeof(OzdsLayoutComponentBase))]
  public override Task SetParametersAsync(ParameterView parameters)
  {
    return base.SetParametersAsync(parameters);
  }
}
