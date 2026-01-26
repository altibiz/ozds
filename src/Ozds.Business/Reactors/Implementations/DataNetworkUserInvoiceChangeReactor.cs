using Ozds.Business.Models;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Messaging.Contracts;
using Ozds.Messaging.Sender.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class DataNetworkUserInvoiceChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataNetworkUserInvoiceChangeHandler>(serviceProvider)
{
}

public class DataNetworkUserInvoiceChangeHandler(
  LocalizationQueries localizationQueries,
  IMessageSender messageSender,
  ILogger<DataNetworkUserInvoiceChangeHandler> logger,
  IHostEnvironment hostEnvironment
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    // NOTE: skip for now in production
    if (hostEnvironment.IsProduction())
    {
      return;
    }

    var culture = localizationQueries.CroatianCulture;
    var invoices = eventArgs.Models
      .Where(x => x.State is DataModelChangedState.Added)
      .Where(x => x.Model is NetworkUserInvoiceModel)
      .Select(x => x.Model)
      .OfType<NetworkUserInvoiceModel>()
      .Select(invoice =>
        new AcknowledgeNetworkUserInvoice(
          invoice.Id,
          invoice.ArchivedNetworkUser.AltiBizSubProjectCode,
          invoice.ToDate,
          [
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture, "Network usage during one tariff period"),
              localizationQueries.Translate(culture, "Month"),
              "1.1.",
              1,
              invoice.UsageActiveEnergyTotalImportT0Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture, "Network usage during multiple tariff periods"),
              localizationQueries.Translate(culture, "Month"),
              "1.2.",
              1,
              invoice.UsageActiveEnergyTotalImportT1Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture, "Network usage during off-peak tariff period"),
              localizationQueries.Translate(culture, "Month"),
              "1.3.",
              1,
              invoice.UsageActiveEnergyTotalImportT2Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture, "Engaged power during multiple tariff periods"),
              localizationQueries.Translate(culture, "Month"),
              "1.4.",
              1,
              invoice.UsageActivePowerTotalImportT1PeakFee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture, "Excessively taken reactive power"),
              localizationQueries.Translate(culture, "Month"),
              "1.5.",
              1,
              invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(culture, "Metering service fee"),
              localizationQueries.Translate(culture, "Month"),
              "1.6.",
              1,
              invoice.UsageMeterFee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture,
                "Electricity supply during multi-rate daily tariff period"),
              localizationQueries.Translate(culture, "Month"),
              "2.1.",
              1,
              invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture,
                "Electricity supply during off-peak daily tariff period"),
              localizationQueries.Translate(culture, "Month"),
              "2.2.",
              1,
              invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture,
                "Fee for encouraging production from renewable energy sources and cogeneration"),
              localizationQueries.Translate(culture, "Month"),
              "2.3.",
              1,
              invoice.SupplyRenewableEnergyFee_EUR
            ),
            new AcknowledgeNetworkUserInvoiceItem(
              localizationQueries.Translate(
                culture,
                "Electricity supply during off-peak daily tariff period"),
              localizationQueries.Translate(culture, "Month"),
              "2.4.",
              1,
              invoice.SupplyBusinessUsageFee_EUR
            )
          ],
          invoice.Total_EUR,
          invoice.ArchivedRegulatoryCatalogue.TaxRate_Percent,
          invoice.Tax_EUR,
          invoice.TotalWithTax_EUR,
          invoice.ArchivedNetworkUser.AutomaticallyApproveInvoices
        ))
      .ToList();

    if (invoices.Count == 0)
    {
      return;
    }

    logger.LogInformation(
      "Acknowledging invoices: {Count}",
      invoices.Count);

    await messageSender.AcknowledgeNetworkUserInvoices(
      invoices,
      cancellationToken
    );
  }
}
