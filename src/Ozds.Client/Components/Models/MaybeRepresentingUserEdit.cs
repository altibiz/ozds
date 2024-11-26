using Microsoft.AspNetCore.Components;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Models;

public partial class MaybeRepresentingUserEdit : OzdsComponentBase
{
  [Inject]
  private AgnosticModelActivator AgnosticModelActivator { get; set; }
    = default!;

  private void OnCreateRepresentative()
  {
    Model = Model with
    {
      Representative = AgnosticModelActivator.Activate<RepresentativeModel>()
    };
  }
}
