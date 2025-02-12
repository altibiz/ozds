using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Client.Import.Abstractions;

namespace Ozds.Client.Import.ModelsImport;

public class NetworkUserImporter : INetworkUserImporter
{
    private readonly ModelActivator _activator;
    public NetworkUserImporter(ModelActivator activator)
    {
        _activator = activator;
    }
    public Task<IEnumerable<NetworkUserModel>> MapNetworkUsersAsync(IEnumerable<NetworkUserCsvRecord> csvRecords)
    {
        var networkUsers = csvRecords.Select(record =>
        {
            var networkUser = _activator.Activate<NetworkUserModel>();
            networkUser.Title = record.Title;
            networkUser.LocationId = record.LocationId;
            networkUser.AltiBizSubProjectCode = record.AltiBizSubProjectCode;
            networkUser.LegalPerson.Name = record.LegalPersonName;
            networkUser.LegalPerson.SocialSecurityNumber = record.SocialSecurityNumber;
            networkUser.LegalPerson.Address = record.Address;
            networkUser.LegalPerson.PostalCode = record.PostalCode;
            networkUser.LegalPerson.City = record.City;
            networkUser.LegalPerson.Email = record.Email;
            networkUser.LegalPerson.PhoneNumber = record.PhoneNumber;
            return networkUser;
        }).ToList();
        return Task.FromResult<IEnumerable<NetworkUserModel>>(networkUsers);
    }

    public class NetworkUserCsvRecord
    {
        public string Title { get; set; } = string.Empty;
        public string LocationId { get; set; } = string.Empty;
        public string AltiBizSubProjectCode { get; set; } = string.Empty;
        public string LegalPersonName { get; set; } = string.Empty;
        public string SocialSecurityNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
