using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface ICatalogue : IAuditable
{
  public IEnumerable<ObisModel> Obis { get; }
}
