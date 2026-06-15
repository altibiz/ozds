using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class DateTimePickerField : OzdsComponentBase
{

  private DateTime? date;

  private TimeSpan time;

  private DateTimeOffset? lastValue;

  private bool open;

  [Parameter]
  public DateTimeOffset? Value { get; set; }

  [Parameter]
  public EventCallback<DateTimeOffset?> ValueChanged { get; set; }

  [Parameter]
  public string? Label { get; set; }

  [Parameter]
  public string? TimeLabel { get; set; }

  // Shown in the field when no value is selected. Defaults to the display
  // format so the user sees the expected pattern (e.g. "dd.MM.yyyy HH:mm:ss").
  [Parameter]
  public string? Placeholder { get; set; }

  // Custom .NET date/time format used both to render the selected value and,
  // unless overridden, as the placeholder pattern.
  [Parameter]
  public string Format { get; set; } = "dd.MM.yyyy HH:mm:ss";

  [Parameter]
  public DateTimeOffset? MinDate { get; set; }

  [Parameter]
  public DateTimeOffset? MaxDate { get; set; }

  [Parameter]
  public bool Clearable { get; set; } = true;

  [Parameter]
  public bool Disabled { get; set; }

  [Parameter]
  public Variant Variant { get; set; } = Variant.Text;

  [Parameter]
  public Margin Margin { get; set; } = Margin.None;

  [Parameter]
  public string? Class { get; set; }

  [Parameter]
  public string? Style { get; set; }

  private bool HasValue
  {
    get { return date is not null; }
  }

  private string PlaceholderText
  {
    get { return Placeholder ?? Format; }
  }

  // The date and time are kept in the display time zone, so they can be
  // formatted directly without another round trip through UTC.
  private string DisplayText
  {
    get
    {
      return date is { } value
        ? (value.Date + time).ToString(Format, CultureInfo.InvariantCulture)
        : string.Empty;
    }
  }

  private int Hour
  {
    get { return time.Hours; }
  }

  private int Minute
  {
    get { return time.Minutes; }
  }

  private int Second
  {
    get { return time.Seconds; }
  }

  private string FieldClass
  {
    get
    {
      var modifier = open ? "ozds-datetime-picker__field--open" : string.Empty;
      return $"ozds-datetime-picker__field {modifier}".Trim();
    }
  }

  protected override void OnParametersSet()
  {
    // Only re-read the bound value when it changes from the outside so that
    // our own edits (which may set just the date or just the time) are not
    // clobbered by the round trip through ValueChanged.
    if (Value != lastValue)
    {
      lastValue = Value;
      Decompose(Value);
    }
  }

  private void ToggleOpen()
  {
    if (Disabled)
    {
      return;
    }

    open = !open;
  }

  private void Close()
  {
    open = false;
  }

  private async Task OnDateChanged(DateTime? newDate)
  {
    date = newDate?.Date;
    await Emit();
  }

  private async Task OnHourChanged(int value)
  {
    time = new TimeSpan(Math.Clamp(value, 0, 23), time.Minutes, time.Seconds);
    await Emit();
  }

  private async Task OnMinuteChanged(int value)
  {
    time = new TimeSpan(time.Hours, Math.Clamp(value, 0, 59), time.Seconds);
    await Emit();
  }

  private async Task OnSecondChanged(int value)
  {
    time = new TimeSpan(time.Hours, time.Minutes, Math.Clamp(value, 0, 59));
    await Emit();
  }

  private async Task SetNow()
  {
    var now = TimeZoneInfo.ConvertTime(DateTimeOffset.Now, GetTimeZone());
    date = now.Date;
    time = new TimeSpan(now.Hour, now.Minute, now.Second);
    await Emit();
  }

  private async Task Clear()
  {
    date = null;
    time = TimeSpan.Zero;
    await Emit();
  }

  private async Task Emit()
  {
    var value = Compose();
    lastValue = value;
    await ValueChanged.InvokeAsync(value);
  }


  // Splits a stored (UTC) instant into the wall-clock date and time of the
  // display time zone for the pickers.
  private void Decompose(DateTimeOffset? value)
  {
    if (value is not { } offset)
    {
      date = null;
      time = TimeSpan.Zero;
      return;
    }

    var local = TimeZoneInfo.ConvertTimeFromUtc(
      offset.UtcDateTime,
      GetTimeZone()
    );
    date = local.Date;
    time = new TimeSpan(local.Hour, local.Minute, local.Second);
  }

  // Recombines the pickers into a single instant. A null date means no value.
  // The wall-clock value is interpreted in the display time zone and
  // normalized to UTC for storage.
  private DateTimeOffset? Compose()
  {
    if (date is not { } value)
    {
      return null;
    }

    var local = DateTime.SpecifyKind(
      value.Date + time,
      DateTimeKind.Unspecified
    );
    var timeZone = GetTimeZone();
    var offset = timeZone.GetUtcOffset(local);
    return new DateTimeOffset(local, offset).ToUniversalTime();
  }

  private DateTime? ToLocalDate(DateTimeOffset? value)
  {
    if (value is not { } offset)
    {
      return null;
    }

    var local = TimeZoneInfo.ConvertTimeFromUtc(
      offset.UtcDateTime,
      GetTimeZone()
    );
    return local.Date;
  }
}
