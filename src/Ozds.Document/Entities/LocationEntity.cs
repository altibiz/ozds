namespace Ozds.Document.Entities;

public class LocationEntity
{
  public string Title { get; set; } = default!;

  public string Id { get; set; } = default!;

  public LegalPersonEntity LegalPerson { get; set; } = default!;

  public string AltiBizSubProjectCode { get; set; } = default!;
}
