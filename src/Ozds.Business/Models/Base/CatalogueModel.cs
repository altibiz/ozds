using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Base;

public abstract class CatalogueModel : TrackableModel, ICatalogue
{
  public virtual IEnumerable<ObisModel> Obis
  {
    get { return []; }
  }
}
