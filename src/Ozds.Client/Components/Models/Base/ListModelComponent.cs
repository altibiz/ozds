using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class ListModelComponent : ModelComponent
{
  [Parameter]
  public IEnumerable<object> Models { get; set; } = default!;

  protected override Type CreateModelType()
  {
    return Models
      .Select(x => x.GetType())
      .DefaultIfEmpty(null)
      .Aggregate((acc, next) =>
        acc is null
          ? null
          : next!.IsAssignableFrom(acc)
            ? next
            : acc)
      ?? throw new InvalidOperationException(
        $"No model type found for {nameof(Models)}.");
  }
}
