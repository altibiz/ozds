using MassTransit;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries;
using Ozds.Messaging.Contracts;

namespace Ozds.Business.Finance;

public class NetworkUserInvoiceIssuer(
  IConfiguration configuration,
  OzdsBillingQueries ozdsBillingQueries,
  NetworkUserInvoiceCalculator invoiceCalculator,
  OzdsInvoiceMutations invoiceMutations,
  OzdsCalculationMutations calculationMutations,
  ISendEndpointProvider endpointProvider
)
{
  public async Task IssueNetworkUserInvoiceAsync(
    string networkUserId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  )
  {
    var basis = await ozdsBillingQueries
      .IssuingBasisForNetworkUser(networkUserId, dateFrom, dateTo);
    var invoice = invoiceCalculator.Calculate(basis);
    var invoiceId = await invoiceMutations.CreateId(invoice.Invoice);

    foreach (var calculation in invoice.Calculations)
    {
      if (calculation is NetworkUserCalculationModel networkUserCalculation)
      {
        networkUserCalculation.NetworkUserInvoiceId = invoiceId;
      }

      calculationMutations.Create(calculation);
    }

    await calculationMutations.SaveChangesAsync();

    var config = configuration
      .GetSection("Ozds")
      .GetSection("Messaging")
      .GetSection("Endpoints");

    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(config["AcknowledgeNetworkUserInvoice"]
              ?? throw new InvalidOperationException(
                "AcknowledgeNetworkUserInvoice endpoint not found")));
    await endpoint.Send(new AcknowledgeNetworkUserInvoice(invoiceId));
  }
}
