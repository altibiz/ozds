using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Columns<TPrefix> : ListModelComponent<TPrefix, TPrefix>
{
  [Parameter]
  public EventCallback<
    IEnumerable<Func<object, object?>>
  > SearchMappersChanged { get; set; }

  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    var parameters = base.CreateParameters();
    parameters[nameof(SearchMappersChanged)] = SearchMappersChanged;
    return parameters;
  }
}
