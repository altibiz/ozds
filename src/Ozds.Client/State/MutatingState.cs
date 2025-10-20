namespace Ozds.Client.State;

public record MutatingState(Type Type, object ObjectModel, bool Created)
{
  public MutatingState(object model, bool created)
    : this(model.GetType(), model, created)
  {
  }
}

public record MutatingState<T>(T Model, bool Created)
  : MutatingState(typeof(T), Model, Created)
  where T : notnull;
