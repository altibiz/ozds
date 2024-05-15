using System.Globalization;
using MassTransit;
using Ozds.Business.Localization.Abstractions;
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
  ISendEndpointProvider endpointProvider,
  IOzdsLocalizer localizer
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

    var culture = CultureInfo.CreateSpecificCulture("hr-HR");

    var endpoint = await endpointProvider.GetSendEndpoint(
      new Uri(
        config["AcknowledgeNetworkUserInvoice"]
        ?? throw new InvalidOperationException(
          "AcknowledgeNetworkUserInvoice endpoint not found")));
    await endpoint.Send(
      new AcknowledgeNetworkUserInvoice(
        invoiceId,
        basis.NetworkUser.AltiBizSubProjectCode,
        basis.ToDate,
        [
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture, "Network usage during one tariff period"),
            localizer.ForCulture(culture, "Month"),
            "1.1.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture, "Network usage during multiple tariff periods"),
            localizer.ForCulture(culture, "Month"),
            "1.2.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture, "Network usage during off-peak tariff period"),
            localizer.ForCulture(culture, "Month"),
            "1.3.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture, "Engaged power during multiple tariff periods"),
            localizer.ForCulture(culture, "Month"),
            "1.4.",
            1,
            invoice.Invoice.UsageActivePowerTotalImportT1PeakFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(culture, "Excessively taken reactive power"),
            localizer.ForCulture(culture, "Month"),
            "1.5.",
            1,
            invoice.Invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(culture, "Metering service fee"),
            localizer.ForCulture(culture, "Month"),
            "1.6.",
            1,
            invoice.Invoice.UsageMeterFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture,
              "Electricity supply during multi-rate daily tariff period"),
            localizer.ForCulture(culture, "Month"),
            "2.1.",
            1,
            invoice.Invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.ForCulture(culture, "Month"),
            "2.2.",
            1,
            invoice.Invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture,
              "Fee for encouraging production from renewable energy sources and cogeneration"),
            localizer.ForCulture(culture, "Month"),
            "2.3.",
            1,
            invoice.Invoice.SupplyRenewableEnergyFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.ForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.ForCulture(culture, "Month"),
            "2.4.",
            1,
            invoice.Invoice.SupplyBusinessUsageFee_EUR
          )
        ],
        invoice.Invoice.Total_EUR,
        basis.RegulatoryCatalogue.TaxRate_Percent,
        invoice.Invoice.Tax_EUR,
        invoice.Invoice.TotalWithTax_EUR
      )
    );
  }
}
