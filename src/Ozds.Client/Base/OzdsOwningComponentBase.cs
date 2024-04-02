using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Base;

public abstract class OzdsOwningComponentBase : OwningComponentBase
{
  public static OzdsComponentLocalizer T
  {
    get { return new OzdsComponentLocalizer(); }
  }
}
