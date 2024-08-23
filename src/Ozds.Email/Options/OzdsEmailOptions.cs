namespace Ozds.Email.Options;

public record OzdsEmailFromOptions(
  string Name,
  string Address
);

public record OzdsEmailSmtpOptions(
  string Host,
  int Port,
  string Username,
  string Password,
  bool Ssl
);

public record OzdsEmailOptions(
  OzdsEmailSmtpOptions Smtp,
  OzdsEmailFromOptions From
);
