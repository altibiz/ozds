using Ozds.Client.Conversion.Abstractions;

namespace Ozds.Client.Conversion.Base;

public abstract class InheritingModelRecordConverter<
  TModel,
  TSuperModel,
  TRecord,
  TSuperRecord>(IServiceProvider serviceProvider)
  : ConcreteModelRecordConverter<TModel, TRecord>
  where TModel : notnull, TSuperModel
  where TRecord : notnull, TSuperRecord
  where TSuperModel : notnull
  where TSuperRecord : notnull
{
  private InitializingModelRecordConverter? _baseRecordConverter;

  private InitializingModelRecordConverter? _baseModelConverter;

  public override void InitializeRecord(TModel model, TRecord record)
  {
    _baseRecordConverter ??=
      serviceProvider
          .GetServices<IModelRecordConverter>()
          .FirstOrDefault(x => x.RecordType == typeof(TSuperRecord))
        as InitializingModelRecordConverter
      ?? throw new InvalidOperationException(
        $"No model record converter found for type {typeof(TSuperRecord)}");

    base.InitializeRecord(model, record);
    _baseRecordConverter.InitializeRecord(model, record);
  }

  public override void InitializeModel(TRecord record, TModel model)
  {
    _baseModelConverter ??=
      serviceProvider
          .GetServices<IModelRecordConverter>()
          .FirstOrDefault(x => x.ModelType == typeof(TSuperModel))
        as InitializingModelRecordConverter
      ?? throw new InvalidOperationException(
        $"No model record converter found for type {typeof(TSuperModel)}");

    base.InitializeModel(record, model);
    _baseModelConverter.InitializeModel(record, model);
  }
}
