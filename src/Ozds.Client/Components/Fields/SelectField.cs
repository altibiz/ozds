using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ozds.Client.Components.Fields;

public partial class SelectField<T>
{
  [Parameter]
  public string Label { get; set; } = default!;

  [Parameter]
  public T Value { get; set; } = default!;

  [Parameter]
  public EventCallback<T> ValueChanged { get; set; }

  [Parameter]
  public IEnumerable<T> SelectedValues { get; set; } = Enumerable.Empty<T>();

  [Parameter]
  public EventCallback<IEnumerable<T>> SelectedValuesChanged { get; set; }

  [Parameter]
  public bool MultiSelection { get; set; }

  [Parameter]
  public Expression<Func<T>> For { get; set; } = default!;

  [Parameter]
  public Origin AnchorOrigin { get; set; } = Origin.BottomLeft;

  [Parameter(CaptureUnmatchedValues = true)]
  public IDictionary<string, object> AdditionalAttributes { get; set; } =
    new Dictionary<string, object>();

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;
}
