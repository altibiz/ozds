using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using DataLocationQueries = Ozds.Data.Queries.LocationQueries;

namespace Ozds.Business.Queries;

public class LocationQueries(
  DataLocationQueries dataLocationQueries,
  AgnosticModelEntityConverter modelEntityConverter
)
{
  public async Task<List<LocationModel>> LocationsByRepresentativeId(
    string representativeId,
    CancellationToken cancellationToken
  )
  {
    var entities = await dataLocationQueries.LocationsByRepresentativeId(
      representativeId,
      cancellationToken
    );

    var models = entities
      .Select(modelEntityConverter.ToModel<LocationModel>)
      .ToList();

    return models;
  }
}
