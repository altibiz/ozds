using Microsoft.AspNetCore.Components;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Models;

public partial class MaybeRepresentingUserEdit : OzdsComponentBase
{
  [Inject]
  private ModelActivator ModelActivator { get; set; }
    = default!;

  [Parameter]
  public MaybeRepresentingUserModel Model { get; set; } = default!;

  private void OnCreateRepresentative()
  {
    Model.Representative = ModelActivator.Activate<RepresentativeModel>();
  }
}
