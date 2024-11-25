using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Dialogs;

public partial class DateRangeDialog : OzdsComponentBase
{
  [CascadingParameter]
  public MudDialogInstance MudDialog { get; set; } = default!;

  [Parameter]
  public DateTimeOffset? MinDate { get; set; }

  [Parameter]
  public DateTimeOffset? MaxDate { get; set; }

  private DateTime? startDate;

  private DateTime? endDate;

  public DateTime? StartDate
  {
    get { return startDate; }
    set
    {
      startDate = value;
      if (endDate.HasValue && startDate.HasValue && startDate > endDate)
      {
        endDate = startDate;
      }
    }
  }

  public DateTime? EndDate
  {
    get { return endDate; }
    set
    {
      endDate = value;
      if (startDate.HasValue && endDate.HasValue && endDate < startDate)
      {
        startDate = endDate;
      }
    }
  }

  private void Submit()
  {
    if (startDate.HasValue && endDate.HasValue)
    {
      MudDialog.Close(DialogResult
        .Ok((
          startDate.Value.ToUniversalTime(),
          endDate.Value.ToUniversalTime())));
    }
  }

  private void Cancel()
  {
    MudDialog.Cancel();
  }
}
