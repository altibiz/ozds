namespace Ozds.Report.Entities;

public class NetworkUserEntity : IdentifiableEntity
{
  public string LocationId { get; set; } = default!;

  public LegalPersonEntity LegalPerson { get; set; } = default!;

  public string AltiBizSubProjectCode { get; set; } = default!;
}
