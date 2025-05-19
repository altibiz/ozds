using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ozds.Client.Components.Fields
{
  public partial class SelectField<T>
  {
    private MudSelect<T> _inner;

    [Parameter] public string Label { get; set; }
    [Parameter] public T Value { get; set; }
    [Parameter] public EventCallback<T> ValueChanged { get; set; }
    [Parameter] public IEnumerable<T> SelectedValues { get; set; }
    [Parameter] public EventCallback<IEnumerable<T>> SelectedValuesChanged { get; set; }
    [Parameter] public bool MultiSelection { get; set; }
    [Parameter] public Expression<Func<T>> For { get; set; }
    [Parameter] public Origin AnchorOrigin { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object> AdditionalAttributes { get; set; }

    [Parameter] public RenderFragment ChildContent { get; set; }
  }
}
