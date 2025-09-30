using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface ICatalogue : ITrackableIdentifiable
{
  public IEnumerable<ObisModel> Obis { get; }

  public IEnumerable<string> ObisCodes
  {
    get { return Obis.Select(x => x.ToCode()); }
  }
}
