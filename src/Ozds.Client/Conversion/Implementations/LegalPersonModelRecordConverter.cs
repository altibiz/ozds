using Ozds.Business.Models.Complex;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Business.Conversion.Implementations.Administration;

public class LegalPersonModelRecordConverter
  : ConcreteModelRecordConverter<LegalPersonModel, LegalPersonRecord>
{
  public override void InitializeRecord(
    LegalPersonModel model,
    LegalPersonRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.Name = model.Name;
    record.Email = model.Email;
    record.PhoneNumber = model.PhoneNumber;
    record.Address = model.Address;
    record.City = model.City;
    record.PostalCode = model.PostalCode;
    record.SocialSecurityNumber = model.SocialSecurityNumber;
  }

  public override void InitializeModel(
    LegalPersonRecord record,
    LegalPersonModel model
  )
  {
    base.InitializeModel(record, model);
    model.Name = record.Name;
    model.Email = record.Email;
    model.PhoneNumber = record.PhoneNumber;
    model.Address = record.Address;
    model.City = record.City;
    model.PostalCode = record.PostalCode;
    model.SocialSecurityNumber = record.SocialSecurityNumber;
  }
}
