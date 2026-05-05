using System.Collections;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class MultiEnumPicker<T> : OzdsComponentBase
{
  [Parameter]
  public IEnumerable<T> Value { get; set; } = default!;

  [Parameter]
  public EventCallback<IEnumerable<T>> ValueChanged { get; set; }

  [Parameter]
  public string Title { get; set; } = default!;

  [Parameter]
  public Func<T, string>? Label { get; set; }

  [Parameter]
  public Expression<Func<T>> For { get; set; } = default!;

  [Parameter]
  public IEnumerable Values { get; set; } = Enum.GetValues(typeof(T));

  [Parameter]
  public bool Disabled { get; set; }
}
