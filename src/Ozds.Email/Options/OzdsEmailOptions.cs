namespace Ozds.Email.Options;

public class OzdsEmailFromOptions
{
  public string Name { get; init; } = string.Empty;

  public string Address { get; init; } = string.Empty;
}

public class OzdsEmailSmtpOptions
{
  public string Host { get; init; } = string.Empty;

  public int Port { get; init; } = 25;

  public string Username { get; init; } = string.Empty;

  public string Password { get; init; } = string.Empty;

  public bool Ssl { get; init; } = false;
}

public class OzdsEmailOptions
{
  public string Host { get; init; } = string.Empty;

  public OzdsEmailSmtpOptions Smtp { get; init; } = new();

  public OzdsEmailFromOptions From { get; init; } = new();
}
