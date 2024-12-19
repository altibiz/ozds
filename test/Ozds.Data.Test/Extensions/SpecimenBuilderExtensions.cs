using System.Linq.Expressions;
using AutoFixture.Dsl;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Test.Extensions;

public static class SpecimenBuilderExtensions
{
  public static async Task<T> CreateInDb<T>(
    this ISpecimenBuilder builder,
    DbContext context,
    CancellationToken cancellationToken = default
  )
  {
    var entity = builder.Create<T>()!;
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public static async Task<List<T>> CreateManyInDb<T>(
    this ISpecimenBuilder builder,
    DbContext context,
    int count = Constants.DefaultDbFuzzCount,
    CancellationToken cancellationToken = default
  )
  {
    var entities = builder.CreateMany<T>(count).ToList();
    context.AddRange(entities.OfType<object>());
    await context.SaveChangesAsync(cancellationToken);
    return entities;
  }

  public static async Task<T> CreateInDb<T>(
    this IPostprocessComposer<T> builder,
    DbContext context,
    CancellationToken cancellationToken = default
  )
  {
    var entity = builder.Create()!;
    context.Add(entity);
    await context.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public static async Task<List<T>> CreateManyInDb<T>(
    this IPostprocessComposer<T> builder,
    DbContext context,
    int count = Constants.DefaultDbFuzzCount,
    CancellationToken cancellationToken = default
  )
  {
    var entities = builder.CreateMany(count).ToList();
    context.AddRange(entities.OfType<object>());
    await context.SaveChangesAsync(cancellationToken);
    return entities;
  }

  public static IPostprocessComposer<T> IndexedWith<T, TProperty>(
    this IPostprocessComposer<T> builder,
    Expression<Func<T, TProperty>> propertyPicker,
    Func<int, TProperty> valueFactory
  )
  {
    var i = 0;
    return builder.With(propertyPicker, () => valueFactory(i++));
  }

  public static IPostprocessComposer<T> IndexedWith<T, TInput, TProperty>(
    this IPostprocessComposer<T> builder,
    Expression<Func<T, TProperty>> propertyPicker,
    Func<TInput, int, TProperty> valueFactory
  )
  {
    var i = 0;
    return builder.With<TProperty, TInput>(
      propertyPicker,
      (input) => valueFactory(input, i++));
  }
}
