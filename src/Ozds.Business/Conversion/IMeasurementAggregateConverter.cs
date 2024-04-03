using System.ComponentModel;
using System.Globalization;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Conversion;

public interface IMeasurementAggregateConverter
{
  public Type AggregateType { get; }

  public Type MeasurementType { get; }

  IMeasurement ToMeasurement(IAggregate aggregate);

  IAggregate ToAggregate(IMeasurement measurement);
}

public abstract class MeasurementAggregateConverter<TAggregate, TMeasurement> : TypeConverter, IMeasurementAggregateConverter
  where TAggregate : class, IAggregate
  where TMeasurement : class, IMeasurement
{
  public Type AggregateType => typeof(TAggregate);

  public Type MeasurementType => typeof(TMeasurement);

  public IMeasurement ToMeasurement(IAggregate aggregate) => ToMeasurement(aggregate as TAggregate ?? throw new ArgumentNullException(nameof(aggregate)));

  public IAggregate ToAggregate(IMeasurement measurement) => ToAggregate(measurement as TMeasurement ?? throw new ArgumentNullException(nameof(measurement)));

  public abstract TMeasurement ToMeasurement(TAggregate aggregate);

  public abstract TAggregate ToAggregate(TMeasurement measurement);

  public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
    sourceType == typeof(TAggregate) || base.CanConvertFrom(context, sourceType);

  public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
    destinationType == typeof(TMeasurement) || base.CanConvertTo(context, destinationType);

  public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) =>
    value is TAggregate aggregate ? ToMeasurement(aggregate) : base.ConvertFrom(context, culture, value);

  public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) =>
    value is TMeasurement measurement ? ToAggregate(measurement) : base.ConvertTo(context, culture, value, destinationType);
}
