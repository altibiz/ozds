namespace Ozds.Business.Models.Abstractions;

public interface INetworkUserInvoice : IInvoice
{
  string NetworkUserId { get; }
}
