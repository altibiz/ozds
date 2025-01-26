using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models;

namespace Ozds.Client.Components.Fields;

public partial class IdentifiableLink : OzdsComponentBase
{
  [Parameter]
  public IIdentifiable Model { get; set; } = default!;

  [CascadingParameter]
  public ModelComponentProvider Provider { get; set; } = default!;
}
