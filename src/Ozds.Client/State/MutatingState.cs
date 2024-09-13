namespace Ozds.Client.State;

public record MutatingState<T>(T Model, bool Created);
