using System.Collections;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Fields;

public partial class EnumPicker<T> : OzdsComponentBase
{
  [Parameter]
  public T Value { get; set; } = default!;

  [Parameter]
  public EventCallback<T> ValueChanged { get; set; } = default!;

  [Parameter]
  public string Title { get; set; } = default!;

  [Parameter]
  public Func<T, string>? Label { get; set; } = default!;

  [Parameter]
  public Expression<Func<T>> For { get; set; } = default!;

  [Parameter]
  public IEnumerable Values { get; set; } = typeof(T).GetNullableEnumValues();
}
