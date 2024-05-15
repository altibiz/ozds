using MassTransit;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Sagas;

namespace Ozds.Business.Sagas;

public class NetworkUserInvoiceStateMachine
  : MassTransitStateMachine<NetworkUserInvoiceState>
{
  public NetworkUserInvoiceStateMachine()
  {
    InstanceState(x => x.CurrentState);

    Event(
      () => InitiateNetworkUserInvoice,
      x =>
      {
        x.CorrelateBy(
          (state, context) =>
            state.NetworkUserInvoiceId
            == context.Message.NetworkUserInvoiceId);
        x.SelectId(x => NewId.NextGuid());

        x.InsertOnInitial = true;

        x.SetSagaFactory(
          context =>
            new NetworkUserInvoiceState
            {
              CorrelationId = context.CorrelationId ?? NewId.NextGuid(),
              NetworkUserInvoiceId = context.Message.NetworkUserInvoiceId
            });
      });

    Event(
      () => CancelNetworkUserInvoice,
      x => x
        .CorrelateBy(
          (state, context) =>
            state.NetworkUserInvoiceId
            == context.Message.NetworkUserInvoiceId)
        .SelectId(
          x => NewId.NextGuid()));

    Event(
      () => RegisterNetworkUserInvoice,
      x => x
        .CorrelateBy(
          (state, context) =>
            state.NetworkUserInvoiceId
            == context.Message.NetworkUserInvoiceId)
        .SelectId(
          x => NewId.NextGuid()));

    Initially(
      When(InitiateNetworkUserInvoice)
        .TransitionTo(Initiated));

    DuringAny(
      When(CancelNetworkUserInvoice)
        .TransitionTo(Cancelled));

    During(
      Initiated,
      When(RegisterNetworkUserInvoice)
        .Then(
          context => context.Saga.BillId =
            context.Message.BillId)
        .Activity(x => x.OfType<NetworkUserInvoiceRegisteredActivity>())
        .TransitionTo(Registered));
  }

  public State Initiated { get; } = default!;

  public State Cancelled { get; } = default!;

  public State Registered { get; } = default!;

  public Event<IInitiateNetworkUserInvoice>
    InitiateNetworkUserInvoice { get; } = default!;

  public Event<ICancelNetworkUserInvoice> CancelNetworkUserInvoice { get; } =
    default!;

  public Event<IRegisterNetworkUserInvoice>
    RegisterNetworkUserInvoice { get; } = default!;
}
