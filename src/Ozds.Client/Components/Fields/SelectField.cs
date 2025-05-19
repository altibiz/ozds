using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Ozds.Client.Components.Fields
{
  public partial class SelectField<T>
  {
    private MudSelect<T> _inner;

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    private void HandlePointerDown(PointerEventArgs e)
    {
      _inner.OpenMenu();
    }
  }
}
