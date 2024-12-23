#pragma warning disable CS9113 // Parameter is unread.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable IDE0005

using System.Globalization;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Agnostic;
using Ozds.Messaging.Contracts;
using Ozds.Messaging.Sender.Abstractions;

namespace Ozds.Business.Finance;

// TODO: in one transaction
// TODO: to mutations

public class NetworkUserInvoiceIssuer(
  IServiceScopeFactory factory,
  BillingQueries ozdsBillingQueries,
  INetworkUserInvoiceCalculator invoiceCalculator,
  ReadonlyQueries queries,
  ILocalizer localizer,
  IMessageSender messageSender
) : INetworkUserInvoiceIssuer
{
  public async Task IssueNetworkUserInvoiceAsync(
    string networkUserId,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo
  )
  {
    NetworkUserInvoiceIssuingBasisModel? basis = null;
    CalculatedNetworkUserInvoiceModel? invoice = null;
    {
      await using var scope = factory.CreateAsyncScope();
      var billingQueries = scope.ServiceProvider
        .GetRequiredService<BillingQueries>();
      var readonlyMutations = scope.ServiceProvider
        .GetRequiredService<ReadonlyMutations>();
      basis = await billingQueries
        .IssuingBasisForNetworkUser(
          networkUserId,
          dateFrom,
          dateTo,
          CancellationToken.None
        );
      invoice = invoiceCalculator.Calculate(basis);
      invoice.Invoice.Id = await readonlyMutations.Create(invoice.Invoice, CancellationToken.None);
      foreach (var calculation in invoice.Calculations)
      {
        if (calculation is NetworkUserCalculationModel networkUserCalculation)
        {
          networkUserCalculation.NetworkUserInvoiceId = invoice.Invoice.Id;
        }
        await readonlyMutations.Create(calculation, CancellationToken.None);
      }
    }
    var culture = CultureInfo.CreateSpecificCulture("hr-HR");

    await messageSender.AcknowledgeNetworkUserInvoice(
      new AcknowledgeNetworkUserInvoice(
        invoice.Invoice.Id,
        basis.NetworkUser.AltiBizSubProjectCode,
        basis.ToDate,
        [
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during one tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.1.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during multiple tariff periods"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.2.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during off-peak tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.3.",
            1,
            invoice.Invoice.UsageActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Engaged power during multiple tariff periods"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.4.",
            1,
            invoice.Invoice.UsageActivePowerTotalImportT1PeakFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(culture, "Excessively taken reactive power"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.5.",
            1,
            invoice.Invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(culture, "Metering service fee"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.6.",
            1,
            invoice.Invoice.UsageMeterFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during multi-rate daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.1.",
            1,
            invoice.Invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.2.",
            1,
            invoice.Invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Fee for encouraging production from renewable energy sources and cogeneration"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.3.",
            1,
            invoice.Invoice.SupplyRenewableEnergyFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
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
