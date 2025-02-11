using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace Ozds.Document.Components;

public class DocumentLayoutBase : DocumentBase
{
  [Parameter]
  public RenderFragment? Body { get; set; }

  [DynamicDependency(
    DynamicallyAccessedMemberTypes.All,
    typeof(DocumentLayoutBase))]
  public override Task SetParametersAsync(ParameterView parameters)
  {
    return base.SetParametersAsync(parameters);
  }
}
