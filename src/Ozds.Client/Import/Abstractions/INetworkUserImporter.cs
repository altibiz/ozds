using Ozds.Business.Models;
using static Ozds.Client.Import.ModelsImport.NetworkUserImporter;

namespace Ozds.Client.Import.Abstractions;

public interface INetworkUserImporter
{
  Task<IEnumerable<NetworkUserModel>> MapNetworkUsersAsync(IEnumerable<NetworkUserCsvRecord> csvRecords);
}
