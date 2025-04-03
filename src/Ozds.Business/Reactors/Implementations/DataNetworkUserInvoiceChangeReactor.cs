using System.Globalization;
using Ozds.Business.Caching;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
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
  ILocalizer localizer,
  IMessageSender messageSender,
  ILogger<DataNetworkUserInvoiceChangeHandler> logger
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not NetworkUserInvoiceModel invoice)
      {
        continue;
      }

      if (entry.State is not DataModelChangedState.Added)
      {
        continue;
      }

      logger.LogInformation("Acknowledging invoice: {Id}", invoice.Id);

      var culture = CultureInfo.CreateSpecificCulture("hr-HR");
      await messageSender.AcknowledgeNetworkUserInvoice(
        new AcknowledgeNetworkUserInvoice(
          invoice.Id,
          invoice.ArchivedNetworkUser.AltiBizSubProjectCode,
          invoice.ToDate,
          [
            new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during one tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.1.",
            1,
            invoice.UsageActiveEnergyTotalImportT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during multiple tariff periods"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.2.",
            1,
            invoice.UsageActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Network usage during off-peak tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.3.",
            1,
            invoice.UsageActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Engaged power during multiple tariff periods"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.4.",
            1,
            invoice.UsageActivePowerTotalImportT1PeakFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture, "Excessively taken reactive power"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.5.",
            1,
            invoice.UsageReactiveEnergyTotalRampedT0Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(culture, "Metering service fee"),
            localizer.TranslateForCulture(culture, "Month"),
            "1.6.",
            1,
            invoice.UsageMeterFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during multi-rate daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.1.",
            1,
            invoice.SupplyActiveEnergyTotalImportT1Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.2.",
            1,
            invoice.SupplyActiveEnergyTotalImportT2Fee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Fee for encouraging production from renewable energy sources and cogeneration"),
            localizer.TranslateForCulture(culture, "Month"),
            "2.3.",
            1,
            invoice.SupplyRenewableEnergyFee_EUR
          ),
          new AcknowledgeNetworkUserInvoiceItem(
            localizer.TranslateForCulture(
              culture,
              "Electricity supply during off-peak daily tariff period"),
            localizer.TranslateForCulture(culture, "Month"),
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
        ),
        cancellationToken
      );
    }
  }
}
