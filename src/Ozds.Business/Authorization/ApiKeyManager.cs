using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Ozds.Business.Models;
using Ozds.Business.Options;

namespace Ozds.Business.Authorization;

public class ApiKeyManager(IOptions<OzdsBusinessOptions> options)
{
  public const string ApiKeySeparator = ":";

  private readonly byte[] secret = Encoding.UTF8.GetBytes(
    options.Value.Authorization.HmacSecret
  );

  public string Generate()
  {
    var bytes = RandomNumberGenerator.GetBytes(32);
    var apiKey = Convert
      .ToBase64String(bytes)
      .TrimEnd('=')
      .Replace('+', '-')
      .Replace('/', '_');
    return apiKey;
  }

  public string Hash(string message)
  {
    var hash = ComputeHmacBase64(message, secret);
    return hash;
  }

  public string Tokenize(ApiKeyModel model)
  {
    return model.Id + ApiKeySeparator + model.Value;
  }

  public (string Id, string Value) Split(string token)
  {
    var parts = token.Split(ApiKeySeparator, 2);
    return (parts[0], parts[1]);
  }

  public bool Verify(string message, string hash)
  {
    var expected = ComputeHmacBase64(message, secret);
    return ConstantTimeEquals(hash, expected);
  }

  private static string ComputeHmacBase64(string message, byte[] key)
  {
    var data = Encoding.UTF8.GetBytes(message);
    using var hmac = new HMACSHA256(key);
    var hash = hmac.ComputeHash(data);
    return Convert.ToBase64String(hash);
  }

  private static bool ConstantTimeEquals(string lhs, string rhs)
  {
    if (lhs is null || rhs is null)
    {
      return false;
    }

    try
    {
      var lhsBase64 = Convert.FromBase64String(lhs);
      var rhsBase64 = Convert.FromBase64String(rhs);
      return CryptographicOperations.FixedTimeEquals(lhsBase64, rhsBase64);
    }
    catch
    {
      return false;
    }
  }
}
