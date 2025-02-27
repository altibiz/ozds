using Ozds.Client.Records.Abstractions;

namespace Ozds.Client.Records;

public abstract class IdentifiableRecord : IRecord
{
  public string Title { get; set; } = default!;
}
