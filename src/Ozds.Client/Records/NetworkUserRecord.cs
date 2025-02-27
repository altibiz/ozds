namespace Ozds.Client.Records;

public class NetworkUserRecord : IdentifiableRecord
{
  public string LocationId { get; set; } = default!;

  public LegalPersonRecord LegalPerson { get; set; } = default!;

  public string AltiBizSubProjectCode { get; set; } = default!;
}
