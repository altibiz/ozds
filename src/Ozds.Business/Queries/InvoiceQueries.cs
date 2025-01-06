using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Queries.Abstractions;
using DataInvoiceQueries = Ozds.Data.Queries.InvoiceQueries;

namespace Ozds.Business.Queries;

public class InvoiceQueries(
  DataInvoiceQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<NetworkUserInvoiceModel>>
    ReadInvoicesByRepresentative(
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
      pageCount);

    return entities.Items
      .Select(modelEntityConverter.ToModel<NetworkUserInvoiceModel>)
      .ToPaginatedList(entities.TotalCount);
  }
}
