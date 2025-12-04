using Ozds.Business.Conversion;
using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Queries;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Composite;
using DataInvoiceQueries = Ozds.Data.Queries.InvoiceQueries;
using DataNetworkUserInvoiceMutations =
  Ozds.Data.Mutations.NetworkUserInvoiceMutations;

namespace Ozds.Business.Mutations;

public class NetworkUserInvoiceMutations(
  IServiceScopeFactory factory,
  DataNetworkUserInvoiceMutations mutations,
  DataInvoiceQueries queries,
  INetworkUserInvoiceCalculator invoiceCalculator,
  LocalizationQueries localizationQueries,
  ModelEntityConverter modelEntityConverter,
  RepresentativeQueries representativeQueries
) : IMutations
{
  public async Task UpdateBillId(
    string id,
    string registrationId,
    CancellationToken cancellationToken
  )
  {
    await mutations.UpdateBillId(id, registrationId, cancellationToken);
  }

  public async Task<CalculatedNetworkUserInvoiceModel>
    Preview(
      string networkUserId,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      CancellationToken cancellationToken
    )
  {
    await using var scope = factory.CreateAsyncScope();
    var billingQueries = scope.ServiceProvider
      .GetRequiredService<BillingQueries>();
    var basis = await billingQueries
      .ReadInvoiceBasisByNetworkUser(
        networkUserId,
        dateFrom,
        dateTo,
        cancellationToken
      );
    var invoice = invoiceCalculator.Calculate(basis);
    var culture = localizationQueries.CroatianCulture;
    var previewText = localizationQueries.Translate(
      culture,
      localizationQueries.Translate(
        localizationQueries.CroatianCulture,
        "This invoice is a preview.")
    );
    invoice.Invoice.Remark = $@"
      <div>
        <p><strong>{previewText}</strong></p>
        <p>{invoice.Invoice.Remark}</p>
      </div>
    ";
    return invoice;
  }

  public async Task<CalculatedNetworkUserInvoiceModel>
    Create(
      string networkUserId,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      CancellationToken cancellationToken
    )
  {
    await using var scope = factory.CreateAsyncScope();
    var billingQueries = scope.ServiceProvider
      .GetRequiredService<BillingQueries>();
    var basis = await billingQueries
      .ReadInvoiceBasisByNetworkUser(
        networkUserId,
        dateFrom,
        dateTo,
        cancellationToken
      );
    var invoice = invoiceCalculator.Calculate(basis);
    invoice = await CreateCalculatedInvoice(invoice, cancellationToken);
    return invoice;
  }

  private async Task<CalculatedNetworkUserInvoiceModel> CreateCalculatedInvoice(
    CalculatedNetworkUserInvoiceModel invoice,
    CancellationToken cancellationToken)
  {
    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(cancellationToken);

    var entity = new CalculatedNetworkUserInvoiceEntity
    {
      Calculations = invoice.Calculations
        .Select(modelEntityConverter.ToEntity<NetworkUserCalculationEntity>)
        .ToList(),
      Invoice = modelEntityConverter
        .ToEntity<NetworkUserInvoiceEntity>(invoice.Invoice)
    };

    foreach (var calculation in entity.Calculations)
    {
      calculation.AuditingRepresentativeId = representativeId;
    }

    entity.Invoice.AuditingRepresentativeId = representativeId;

    var created = await mutations.CreateCalculatedInvoice(
      entity,
      cancellationToken
    );

    if (!created)
    {
      entity = await queries.ReadCalculatedNetworkUserInvoice(
        invoice.Invoice.NetworkUserId,
        invoice.Invoice.FromDate,
        invoice.Invoice.ToDate,
        cancellationToken
      );

      if (entity is null)
      {
        throw new InvalidOperationException(
          "Could not find calculated invoice.");
      }
    }

    var model = new CalculatedNetworkUserInvoiceModel
    {
      Invoice = modelEntityConverter.ToModel<NetworkUserInvoiceModel>(
        entity.Invoice
      ),
      Calculations = entity.Calculations
        .Select(modelEntityConverter.ToModel<NetworkUserCalculationModel>)
        .ToList()
    };

    return model;
  }
}
