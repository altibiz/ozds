using System.ComponentModel;
using System.Globalization;
using Ozds.Business.Conversion.Abstractions;

namespace Ozds.Business.Conversion.Base;

public abstract class ModelEntityConverter<TModel, TEntity> : TypeConverter, IModelEntityConverter
  where TModel : class
  where TEntity : class
{
  protected abstract TEntity ToEntity(TModel model);

  protected abstract TModel ToModel(TEntity entity);

  public bool CanConvertToEntity(Type modelType) => modelType == typeof(TModel);

  public bool CanConvertToModel(Type entityType) => entityType == typeof(TEntity);

  public object ToEntity(object model) => ToEntity(model as TModel ?? throw new ArgumentNullException(nameof(model)));

  public object ToModel(object entity) => ToModel(entity as TEntity ?? throw new ArgumentNullException(nameof(entity)));

  public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
    sourceType == typeof(TModel) || base.CanConvertFrom(context, sourceType);

  public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
    destinationType == typeof(TEntity) || base.CanConvertTo(context, destinationType);

  public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) =>
    value is TModel model ? ToEntity(model) : base.ConvertFrom(context, culture, value);

  public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) =>
    value is TEntity entity ? ToModel(entity) : base.ConvertTo(context, culture, value, destinationType);
}
