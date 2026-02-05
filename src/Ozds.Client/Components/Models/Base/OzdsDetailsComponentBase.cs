using System.Linq.Expressions;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsDetailsComponentBase<TModel>
  : OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Details; }
  }

  protected MemberExpression Label<T>(Expression<Func<TModel, T?>> next)
  {
    return next.LabelExpression();
  }
}
