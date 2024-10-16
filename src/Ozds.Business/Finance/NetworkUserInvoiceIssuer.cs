#pragma warning disable CS9113 // Parameter is unread.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable IDE0005

using System.Globalization;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries;
using Ozds.Messaging.Contracts;
using Ozds.Messaging.Sender.Abstractions;

namespace Ozds.Business.Finance;

// TODO: in one transaction

public class NetworkUserInvoiceIssuer(
  OzdsBillingQueries ozdsBillingQueries,
  INetworkUserInvoiceCalculator invoiceCalculator,
  NetworkUserInvoiceMutations invoiceMutations,
  ReadonlyMutations calculationMutations,
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
    // var basis = await ozdsBillingQueries
    //   .IssuingBasisForNetworkUser(networkUserId, dateFrom, dateTo);
    // var invoice = invoiceCalculator.Calculate(basis);
    // var invoiceId = await invoiceMutations.CreateId(invoice.Invoice);
    // await calculationMutations.SaveChangesAsync();

    // foreach (var calculation in invoice.Calculations)
    // {
    //   if (calculation is NetworkUserCalculationModel networkUserCalculation)
    //   {
    //     networkUserCalculation.NetworkUserInvoiceId = invoiceId;
    //   }
    //   calculationMutations.Create(calculation);
    //   await calculationMutations.SaveChangesAsync();
    // }

    // var culture = CultureInfo.CreateSpecificCulture("hr-HR");

    // await messageSender.AcknowledgeNetworkUserInvoice(
    //   new AcknowledgeNetworkUserInvoice(
    //     invoiceId,
    //     basis.NetworkUser.AltiBizSubProjectCode,
    //     basis.ToDate,
    //     [
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture, "Network usage during one tariff period"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.1.",
    //         1,
    //         invoice.Invoice.UsageActiveEnergyTotalImportT0Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture, "Network usage during multiple tariff periods"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.2.",
    //         1,
    //         invoice.Invoice.UsageActiveEnergyTotalImportT1Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture, "Network usage during off-peak tariff period"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.3.",
    //         1,
    //         invoice.Invoice.UsageActiveEnergyTotalImportT2Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture, "Engaged power during multiple tariff periods"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.4.",
    //         1,
    //         invoice.Invoice.UsageActivePowerTotalImportT1PeakFee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(culture, "Excessively taken reactive power"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.5.",
    //         1,
    //         invoice.Invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(culture, "Metering service fee"),
    //         localizer.ForCulture(culture, "Month"),
    //         "1.6.",
    //         1,
    //         invoice.Invoice.UsageMeterFee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture,
    //           "Electricity supply during multi-rate daily tariff period"),
    //         localizer.ForCulture(culture, "Month"),
    //         "2.1.",
    //         1,
    //         invoice.Invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture,
    //           "Electricity supply during off-peak daily tariff period"),
    //         localizer.ForCulture(culture, "Month"),
    //         "2.2.",
    //         1,
    //         invoice.Invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture,
    //           "Fee for encouraging production from renewable energy sources and cogeneration"),
    //         localizer.ForCulture(culture, "Month"),
    //         "2.3.",
    //         1,
    //         invoice.Invoice.SupplyRenewableEnergyFee_EUR
    //       ),
    //       new AcknowledgeNetworkUserInvoiceItem(
    //         localizer.ForCulture(
    //           culture,
    //           "Electricity supply during off-peak daily tariff period"),
    //         localizer.ForCulture(culture, "Month"),
    //         "2.4.",
    //         1,
    //         invoice.Invoice.SupplyBusinessUsageFee_EUR
    //       )
    //     ],
    //     invoice.Invoice.Total_EUR,
    //     basis.RegulatoryCatalogue.TaxRate_Percent,
    //     invoice.Invoice.Tax_EUR,
    //     invoice.Invoice.TotalWithTax_EUR
    //   )
    // );
  }
}
