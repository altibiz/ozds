using Ozds.Client.Conversion.Abstractions;

namespace Ozds.Client.Conversion.Base;

public abstract class InitializingModelRecordConverter : IModelRecordConverter
{
  public abstract Type RecordType { get; }

  public abstract Type ModelType { get; }

  public abstract bool CanConvertToRecord(Type modelType);

  public abstract bool CanConvertToModel(Type recordType);

  public virtual object ToRecord(object model)
  {
    var record = BoxRecord();
    InitializeRecord(model, record);
    return record;
  }

  public virtual object ToModel(object record)
  {
    var model = BoxModel();
    InitializeModel(record, model);
    return model;
  }

  public abstract object BoxRecord();

  public abstract object BoxModel();

  public virtual void InitializeRecord(object model, object record)
  {
  }

  public virtual void InitializeModel(object record, object model)
  {
  }
}
