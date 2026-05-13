using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;

namespace Ozds.Client.Components.Fields;

public partial class SearchableSelectField<T> : IAsyncDisposable
{
  private const string ModulePath =
    "/js/components/searchable-select-field/searchable-select-field.js";

  private readonly string _instanceId = Guid.NewGuid().ToString("N");

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

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  private IJSObjectReference? _module;

  private bool _open;
  private string _search = string.Empty;
  private bool _caseSensitive = true;
  private int _highlightedIndex = 0;

  private MudTextField<string> _searchField = default!;
  private ElementReference _elementsField = default!;

  private IReadOnlyList<T>? _filteredItemsCache;
  private ICollection<T>? _previousItems;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      _module = await JS.InvokeAsync<IJSObjectReference>("import", ModulePath);
    }
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();
    // NOTE: this should be further addressed
    // in future useMemo clone implementation
    if (!ReferenceEquals(_previousItems, Items))
    {
      _previousItems = Items;
      InvalidateFilteredCache();
    }
  }

  private string GetItemId(int index) => $"ozds-ss-item-{_instanceId}-{index}";

  private void SetHighlightedIndex(int index)
  {
    var count = GetFilteredItems().Count;
    if (count == 0)
    {
      _highlightedIndex = 0;
      return;
    }

    _highlightedIndex = Math.Clamp(index, 0, count - 1);
  }

  private async Task SetHighlightedIndexWithScrollAlignment(int index)
  {
    SetHighlightedIndex(index);

    if (_module is not null && _highlightedIndex >= 0)
    {
      await _module.InvokeVoidAsync(
        "scrollItemIntoView",
        GetItemId(_highlightedIndex)
      );
    }
  }

  private void OnSearchChanged(string value)
  {
    _highlightedIndex = 0;
    _search = value;
    InvalidateFilteredCache();
  }

  private async Task HandleKeyDownSearch(KeyboardEventArgs e)
  {
    switch (e.Key)
    {
      case "Tab":
      case "ArrowDown":
        await SetHighlightedIndexWithScrollAlignment(0);
        await _elementsField.FocusAsync();
        break;
      case "Escape":
        Close();
        break;
    }
  }

  private async Task HandleKeyDownPopover(KeyboardEventArgs e)
  {
    switch (e.Key)
    {
      case "Tab":
        await _searchField.FocusAsync();
        break;
      case "Escape":
        Close();
        break;
      case "ArrowDown":
        await SetHighlightedIndexWithScrollAlignment(_highlightedIndex + 1);
        break;
      case "ArrowUp":
        await SetHighlightedIndexWithScrollAlignment(_highlightedIndex - 1);
        break;
      case "Home":
        await SetHighlightedIndexWithScrollAlignment(0);
        break;
      case "End":
        await SetHighlightedIndexWithScrollAlignment(int.MaxValue);
        break;
      case "Enter":
        var filtered = GetFilteredItems();
        if (
          filtered.Count > 0
          && _highlightedIndex >= 0
          && _highlightedIndex < filtered.Count
        )
        {
          await ToggleItem(filtered[_highlightedIndex]);
        }
        break;
    }
  }

  private void OnCaseSensitiveToggle()
  {
    _highlightedIndex = 0;
    _caseSensitive = !_caseSensitive;
    InvalidateFilteredCache();
  }

  private IReadOnlyList<T> GetSelectedValuesList()
  {
    return SelectedValues?.ToList() ?? new List<T>();
  }

  private bool HasSelection =>
    MultiSelection ? SelectedValues?.Any() == true : Value is not null;

  private IReadOnlyList<T> GetFilteredItems()
  {
    return _filteredItemsCache ??= ComputeFilteredItems();
  }

  private void InvalidateFilteredCache()
  {
    _filteredItemsCache = null;
  }

  private IReadOnlyList<T> ComputeFilteredItems()
  {
    if (string.IsNullOrWhiteSpace(_search))
    {
      return Items is IReadOnlyList<T> list ? list : Items.ToList();
    }

    var comparison = _caseSensitive
      ? StringComparison.Ordinal
      : StringComparison.OrdinalIgnoreCase;

    return Items
      .Where(item =>
      {
        var text = GetDisplayText(item);
        return text.Contains(_search, comparison);
      })
      .ToList();
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
      return GetSelectedValuesList()
        .Any(v => EqualityComparer<T>.Default.Equals(v, item));
    }

    return EqualityComparer<T?>.Default.Equals(Value, item);
  }

  private void ToggleOpen()
  {
    _open = !_open;
    _highlightedIndex = 0;
    if (_open)
    {
      _search = string.Empty;
      InvalidateFilteredCache();
    }
  }

  private void Close()
  {
    _open = false;
  }

  // NOTE: for current select sizes this closure callback style
  // should be acceptable
  // in future if need be, reconstruct this to use event.target
  // so it does not need to save closure per item
  private Task ToggleItem(T item, int elementIndex)
  {
    SetHighlightedIndex(elementIndex);
    return ToggleItem(item);
  }

  private async Task ToggleItem(T item)
  {
    if (MultiSelection)
    {
      var list = GetSelectedValuesList().ToList();

      var index = list.FindIndex(v =>
        EqualityComparer<T>.Default.Equals(v, item)
      );
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

  private string GetItemClass(bool selected, bool highlighted)
  {
    var classes = new List<string> { "ozds-searchable-select__item" };
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

    if (highlighted)
    {
      classes.Add("mud-primary-text mud-primary-hover");
    }

    return string.Join(" ", classes);
  }

  public async ValueTask DisposeAsync()
  {
    if (_module is null)
    {
      return;
    }

    // NOTE: this catch is here because the disconnected exception
    // does not signify an actual error in this case and can be safely ignored
    try
    {
      await _module.DisposeAsync();
    }
    catch (JSDisconnectedException)
    {
      // Ignore: Blazor circuit/browser already disconnected.
    }
  }
}
