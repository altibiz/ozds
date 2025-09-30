namespace Ozds.Sdk.Contracts.V1;

public partial interface IOzdsApiV1Client
{
  public string? BaseUrl { get; set; }

  public string? ApiKey { get; set; }
}
