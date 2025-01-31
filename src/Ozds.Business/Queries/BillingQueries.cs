using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries.Abstractions;
using DataBillingQueries = Ozds.Data.Queries.BillingQueries;

namespace Ozds.Business.Queries;

public class BillingQueries(
  DataBillingQueries queries,
  ModelEntityConverter modelEntityConverter
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

    return new NetworkUserInvoiceIssuingBasisModel
    {
      Location = modelEntityConverter.ToModel<LocationModel>(entity.Location),
      NetworkUser = modelEntityConverter.ToModel<NetworkUserModel>(
        entity.NetworkUser),
      RegulatoryCatalogue = modelEntityConverter
        .ToModel<RegulatoryCatalogueModel>(entity.RegulatoryCatalogue),
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      NetworkUserCalculationBases = entity.NetworkUserCalculationBases
        .Select(
          x => new NetworkUserCalculationBasisModel
          {
            FromDate = x.FromDate,
            ToDate = x.ToDate,
            Aggregates = x.Aggregates
              .Select(y => modelEntityConverter.ToModel<AggregateModel>(y))
              .ToList(),
            Location = modelEntityConverter.ToModel<LocationModel>(x.Location),
            NetworkUser = modelEntityConverter.ToModel<NetworkUserModel>(
              x.NetworkUser),
            MeasurementLocation = modelEntityConverter
              .ToModel<NetworkUserMeasurementLocationModel>(
                x.MeasurementLocation),
            UsageNetworkUserCatalogue = modelEntityConverter
              .ToModel<NetworkUserCatalogueModel>(x.UsageNetworkUserCatalogue),
            SupplyRegulatoryCatalogue = modelEntityConverter
              .ToModel<RegulatoryCatalogueModel>(x.SupplyRegulatoryCatalogue),
            Meter = modelEntityConverter.ToModel<MeterModel>(x.Meter)
          })
        .ToList()
    };
  }
}
