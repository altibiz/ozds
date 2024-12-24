using Ozds.Business.Conversion;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using DataBillingQueries = Ozds.Data.Queries.BillingQueries;

// TODO: location invoices

namespace Ozds.Business.Queries;

public class BillingQueries(
  DataBillingQueries queries
) : IQueries
{
  public async Task<NetworkUserInvoiceIssuingBasisModel>
    IssuingBasisForNetworkUser(
      string networkUserId,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      CancellationToken cancellationToken
    )
  {
    var entity = await queries.ReadIssuingBasisForNetworkUser(
      networkUserId,
      fromDate,
      toDate,
      cancellationToken
    );

    return new(
      entity.Location.ToModel(),
      entity.NetworkUser.ToModel(),
      entity.RegulatoryCatalogue.ToModel(),
      fromDate,
      toDate,
      entity.NetworkUserCalculationBases
        .Select(x => new NetworkUserCalculationBasisModel(
          x.FromDate,
          x.ToDate,
          x.Aggregates.Select(y => y.ToModel()).ToList(),
          x.Location.ToModel(),
          x.NetworkUser.ToModel(),
          x.MeasurementLocation.ToModel(),
          x.UsageNetworkUserCatalogue.ToModel(),
          x.SupplyRegulatoryCatalogue.ToModel(),
          x.Meter.ToModel()
        ))
        .ToList()
    );
  }
}
