namespace Ozds.Email.Sender.Abstractions;

public record EmailMessage(
  string Name,
  string Address,
  string Subject,
  string Content
);

public interface IEmailSender
{
  public void Send(EmailMessage message);

  public void SendBulk(IEnumerable<EmailMessage> messages);

  public Task SendAsync(EmailMessage message);

  public Task SendBulkAsync(IEnumerable<EmailMessage> messages);
}
