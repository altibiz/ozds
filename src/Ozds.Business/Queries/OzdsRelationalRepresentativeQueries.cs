using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Queries;

public partial class OzdsRelationalQueries
{
  public async Task<RepresentativeModel?> RepresentativeById(string id) =>
    await _context.Representatives.FirstOrDefaultAsync(entity =>
      entity.Id == id) is { } entity
      ? entity.ToModel()
      : null;

  public async Task<PaginatedList<RepresentativeModel>> OperatorRepresentatives(
    Expression<Func<RepresentativeEntity, bool>>? filter = null,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) =>
    await _context.Representatives
      .Where(entity => entity.IsOperatorRepresentative)
      .QueryPaged(
        RepresentativeModelExtensions.ToModel,
        filter,
        pageNumber,
        pageCount
      );

  public async Task<PaginatedList<RepresentativeModel>> NetworkUserRepresentatives(
    string id,
    Expression<Func<RepresentativeEntity, bool>>? filter = null,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) =>
    await _context.Representatives
      .Where(entity => entity.NetworkUsers.Any(networkUser => networkUser.Id == id))
      .QueryPaged(
        RepresentativeModelExtensions.ToModel,
        filter,
        pageNumber,
        pageCount
      );

  public async Task<PaginatedList<RepresentativeModel>> LocationRepresentatives(
    string id,
    Expression<Func<RepresentativeEntity, bool>>? filter = null,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) =>
    await _context.Representatives
      .Where(entity => entity.Locations.Any(networkUser => networkUser.Id == id))
      .QueryPaged(
        RepresentativeModelExtensions.ToModel,
        filter,
        pageNumber,
        pageCount
      );
}
