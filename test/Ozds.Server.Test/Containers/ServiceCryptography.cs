using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ozds.Server.Test.Containers;

public static class ServiceCryptography
{
  public static string BcryptHash(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  public static (string, string) Rs256KeyPair(string subjectName)
  {
    using var rsa = RSA.Create(2048);

    var distinguishedName = new X500DistinguishedName($"CN={subjectName}");
    var request = new CertificateRequest(
      distinguishedName,
      rsa,
      HashAlgorithmName.SHA256,
      RSASignaturePadding.Pkcs1
    );

    request.CertificateExtensions.Add(
      new X509BasicConstraintsExtension(true, false, 0, true)
    );
    request.CertificateExtensions.Add(
      new X509KeyUsageExtension(
        X509KeyUsageFlags.DigitalSignature
          | X509KeyUsageFlags.KeyEncipherment
          | X509KeyUsageFlags.KeyCertSign,
        true
      )
    );
    request.CertificateExtensions.Add(
      new X509SubjectKeyIdentifierExtension(request.PublicKey, false)
    );

    var certificate = request.CreateSelfSigned(
      DateTimeOffset.UtcNow.AddDays(-1),
      DateTimeOffset.UtcNow.AddYears(1)
    );

    var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
    var privateKeyPem = new StringBuilder();
    privateKeyPem.AppendLine("-----BEGIN PRIVATE KEY-----");
    privateKeyPem.AppendLine(
      Convert.ToBase64String(
        privateKeyBytes,
        Base64FormattingOptions.InsertLineBreaks
      )
    );
    privateKeyPem.AppendLine("-----END PRIVATE KEY-----");

    var certificatePem = new StringBuilder();
    certificatePem.AppendLine("-----BEGIN CERTIFICATE-----");
    certificatePem.AppendLine(
      Convert.ToBase64String(
        certificate.Export(X509ContentType.Cert),
        Base64FormattingOptions.InsertLineBreaks
      )
    );
    certificatePem.AppendLine("-----END CERTIFICATE-----");

    return (privateKeyPem.ToString(), certificatePem.ToString());
  }

  public static string GlibcPbkdf2Hash(string password)
  {
    const int iterations = 310000;

    var salt = new byte[16];
    using (var rng = RandomNumberGenerator.Create())
    {
      rng.GetBytes(salt);
    }

    // https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0060 - replaced by
    var passwordHash = Rfc2898DeriveBytes.Pbkdf2(
      password,
      salt,
      iterations,
      HashAlgorithmName.SHA512,
      64
    );

    // NOTE: Authelia uses glibc base64
    // which uses '.' instead of '+' without padding...
    var saltBase64 = Convert
      .ToBase64String(salt)
      .Replace("+", ".")
      .TrimEnd('=');
    var hashBase64 = Convert
      .ToBase64String(passwordHash)
      .Replace("+", ".")
      .TrimEnd('=');

    var hash = $"$pbkdf2-sha512${iterations}${saltBase64}${hashBase64}";

    return hash;
  }
}
