using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ozds.Client.Components.Fields;

public partial class SearchableSelectField<T>
{
  [Parameter]
  public string? Label { get; set; }

  [Parameter]
  public string? Placeholder { get; set; }

  [Parameter]
  public T? Value { get; set; }

  [Parameter]
  public EventCallback<T?> ValueChanged { get; set; }

  [Parameter]
  public IEnumerable<T>? SelectedValues { get; set; }

  [Parameter]
  public EventCallback<IEnumerable<T>> SelectedValuesChanged { get; set; }

  [Parameter]
  public bool MultiSelection { get; set; }

  [Parameter]
  public Expression<Func<T>>? For { get; set; }

  [Parameter]
  public string? Text { get; set; }

  [Parameter]
  public Origin AnchorOrigin { get; set; } = Origin.BottomCenter;

  [Parameter]
  public Origin TransformOrigin { get; set; } = Origin.TopCenter;

  [Parameter]
  public ICollection<T> Items { get; set; } = Array.Empty<T>();

  [Parameter]
  public Func<T?, string?>? ToStringFunc { get; set; }

  [Parameter]
  public RenderFragment<T>? ItemTemplate { get; set; }

  [Parameter]
  public Variant Variant { get; set; } = Variant.Text;

  [Parameter]
  public Margin Margin { get; set; } = Margin.None;

  [Parameter]
  public bool Dense { get; set; } = true;

  [Parameter]
  public bool Clearable { get; set; }

  [Parameter]
  public string? Class { get; set; }

  [Parameter]
  public string? Style { get; set; }

  [Parameter]
  public string? PopoverClass { get; set; }

  [Parameter]
  public string? ItemClass { get; set; }

  [Parameter]
  public string? SelectedItemClass { get; set; }

  [Parameter]
  public string MaxPopoverHeight { get; set; } = "320px";

  [Parameter(CaptureUnmatchedValues = true)]
  public IDictionary<string, object>? AdditionalAttributes { get; set; }

  private bool _open;
  private string _search = string.Empty;
  private bool _caseSensitive = true;

  private void OnCaseSensitiveToggle()
  {
    _caseSensitive = !_caseSensitive;
  }

  private IReadOnlyList<T> GetSelectedValuesList()
  {
    return SelectedValues?.ToList() ?? new List<T>();
  }

  private bool HasSelection =>
    MultiSelection ? SelectedValues?.Any() == true : Value is not null;

  private IReadOnlyList<T> GetFilteredItems()
  {
    if (string.IsNullOrWhiteSpace(_search))
    {
      return Items is IReadOnlyList<T> list ? list : Items.ToList();
    }

    var comparison = _caseSensitive
      ? StringComparison.Ordinal
      : StringComparison.OrdinalIgnoreCase;

    return Items.Where(item =>
    {
      var text = GetDisplayText(item);
      return text.Contains(_search, comparison);
    }).ToList();
  }

  private string GetDisplayText(T? item)
  {
    if (item is null)
    {
      return string.Empty;
    }

    if (ToStringFunc is not null)
    {
      return ToStringFunc(item) ?? string.Empty;
    }

    return item.ToString() ?? string.Empty;
  }

  private bool IsSelected(T item)
  {
    if (MultiSelection)
    {
      return GetSelectedValuesList().Any(v =>
        EqualityComparer<T>.Default.Equals(v, item));
    }

    return EqualityComparer<T?>.Default.Equals(Value, item);
  }

  private void ToggleOpen()
  {
    _open = !_open;
    if (_open)
    {
      _search = string.Empty;
    }
  }

  private void Close()
  {
    _open = false;
  }

  private async Task ToggleItem(T item)
  {
    if (MultiSelection)
    {
      var list = GetSelectedValuesList().ToList();
      var index = list.FindIndex(v =>
        EqualityComparer<T>.Default.Equals(v, item));
      if (index >= 0)
      {
        list.RemoveAt(index);
      }
      else
      {
        list.Add(item);
      }

      if (SelectedValuesChanged.HasDelegate)
      {
        await SelectedValuesChanged.InvokeAsync(list);
      }
    }
    else
    {
      if (ValueChanged.HasDelegate)
      {
        await ValueChanged.InvokeAsync(item);
      }

      Close();
    }
  }

  private async Task ClearAll()
  {
    if (MultiSelection)
    {
      if (SelectedValuesChanged.HasDelegate)
      {
        await SelectedValuesChanged.InvokeAsync(Array.Empty<T>());
      }
    }
    else
    {
      if (ValueChanged.HasDelegate)
      {
        await ValueChanged.InvokeAsync(default);
      }
    }
  }

  private string GetItemClass(bool selected)
  {
    var classes = new List<string>
    {
      "ozds-searchable-select__item",
    };
    if (!string.IsNullOrEmpty(ItemClass))
    {
      classes.Add(ItemClass);
    }

    if (selected)
    {
      classes.Add("ozds-searchable-select__item--selected");
      if (!string.IsNullOrEmpty(SelectedItemClass))
      {
        classes.Add(SelectedItemClass);
      }
    }

    return string.Join(" ", classes);
  }
}
