using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataInvoiceQueries = Ozds.Data.Queries.InvoiceQueries;

namespace Ozds.Business.Queries;

public class InvoiceQueries(
  DataInvoiceQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<
    PaginatedList<NetworkUserInvoiceModel>
  > ReadByRepresentativeIdAndRole(
    string representativeId,
    RoleModel role,
    int pageNumber,
    CancellationToken cancellationToken,
    DateTimeOffset? fromDate = null,
    DateTimeOffset? toDate = null,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    var entities = await queries.ReadInvoicesByRepresentative(
      representativeId,
      role.ToEntity(),
      pageNumber,
      cancellationToken,
      fromDate,
      toDate,
      pageCount
    );

    return entities
      .Items.Select(modelEntityConverter.ToModel<NetworkUserInvoiceModel>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<CalculatedNetworkUserInvoiceModel?> ReadCalculatedById(
    string id,
    CancellationToken cancellationToken
  )
  {
    var entity = await queries.ReadCalculatedNetworkUserInvoice(
      id,
      cancellationToken
    );

    return entity is null
      ? default
      : new CalculatedNetworkUserInvoiceModel
      {
        Calculations = entity
          .Calculations.Select(
            modelEntityConverter.ToModel<NetworkUserCalculationModel>
          )
          .ToList(),
        Invoice = modelEntityConverter.ToModel<NetworkUserInvoiceModel>(
          entity.Invoice
        ),
      };
  }
}
