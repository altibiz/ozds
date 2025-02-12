namespace Ozds.Client.Conversion.Base;

public abstract class ConcreteModelRecordConverter<TModel, TRecord>
  : InitializingModelRecordConverter
  where TModel : notnull
  where TRecord : notnull
{
  public override Type RecordType
  {
    get { return typeof(TRecord); }
  }

  public override Type ModelType
  {
    get { return typeof(TModel); }
  }

  public virtual void InitializeRecord(TModel model, TRecord record)
  {
  }

  public virtual void InitializeModel(TRecord record, TModel model)
  {
  }

  public virtual TRecord ToRecord(TModel model)
  {
    var record = CreateRecord();
    InitializeRecord(model, record);
    return record;
  }

  public virtual TModel ToModel(TRecord record)
  {
    var model = CreateModel();
    InitializeModel(record, model);
    return model;
  }

  public override bool CanConvertToRecord(Type modelType)
  {
    return modelType.IsAssignableTo(typeof(TModel));
  }

  public override bool CanConvertToModel(Type recordType)
  {
    return recordType.IsAssignableTo(typeof(TRecord));
  }

  public override object BoxRecord()
  {
    return CreateRecord();
  }

  public override object BoxModel()
  {
    return CreateModel();
  }

  public override void InitializeModel(object record, object model)
  {
    InitializeModel((TRecord)record, (TModel)model);
  }

  public override void InitializeRecord(object model, object record)
  {
    InitializeRecord((TModel)model, (TRecord)record);
  }

  public virtual TRecord CreateRecord()
  {
    return Activator.CreateInstance<TRecord>();
  }

  public virtual TModel CreateModel()
  {
    return Activator.CreateInstance<TModel>();
  }

  public override object ToRecord(object model)
  {
    return ToRecord((TModel)model);
  }

  public override object ToModel(object record)
  {
    return ToModel((TRecord)record);
  }
}
