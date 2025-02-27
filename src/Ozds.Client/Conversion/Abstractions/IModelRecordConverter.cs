namespace Ozds.Client.Conversion.Abstractions;

public interface IModelRecordConverter
{
  public Type RecordType { get; }

  public Type ModelType { get; }

  public bool CanConvertToRecord(Type modelType);

  public bool CanConvertToModel(Type recordType);

  public object ToRecord(object model);

  public object ToModel(object record);
}
