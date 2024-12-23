namespace Ozds.Data.Options;

public record OzdsDataOptions(
  string ConnectionString,
  bool UseProxies = true
);
