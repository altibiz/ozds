using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class IdentifiableLink<T, TPage> : OzdsComponentBase
  where T : IIdentifiable
{
  [Parameter]
  public T Model { get; set; } = default!;

  private void OnClick()
  {
    var parameters = new { Model.Id };
    NavigateToPage<TPage>(parameters);
  }
}
