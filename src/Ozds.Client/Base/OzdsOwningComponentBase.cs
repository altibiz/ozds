using System.Globalization;
using Microsoft.AspNetCore.Components;
using Ozds.Business.Time;

namespace Ozds.Client.Base;

public abstract class OzdsOwningComponentBase : OwningComponentBase
{
  public static OzdsComponentLocalizer T
  {
    get { return new OzdsComponentLocalizer(); }
  }

}
