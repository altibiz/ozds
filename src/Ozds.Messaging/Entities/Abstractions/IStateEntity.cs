using MassTransit;

namespace Ozds.Messaging.Entities.Abstractions;

public interface IStateEntity : IEntity, SagaStateMachineInstance
{
  public string CurrentState { get; }

  public uint RowVersion { get; }
}
