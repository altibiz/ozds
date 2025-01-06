using MassTransit;
using Microsoft.Extensions.Options;
using Ozds.Messaging.Contracts;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Entities;

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceStateMachine
  : MassTransitStateMachine<NetworkUserInvoiceStateEntity>
{
  public NetworkUserInvoiceStateMachine(IOptions<OzdsFakeOptions> options)
  {
    InstanceState(x => x.CurrentState);

    Event(
      () => AcknowledgeNetworkUserInvoice,
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
            new NetworkUserInvoiceStateEntity
            {
              CorrelationId = context.CorrelationId ?? NewId.NextGuid(),
              NetworkUserInvoiceId = context.Message.NetworkUserInvoiceId
            });
      });

    Initially(
      When(AcknowledgeNetworkUserInvoice)
        .Then(
          context => context.Saga.NetworkUserInvoiceId
            = context.Message.NetworkUserInvoiceId)
        .Send(
          new Uri(
            options.Value.Messaging.Endpoints.InitiateNetworkUserInvoice),
          context =>
            new InitiateNetworkUserInvoice(
              context.Saga.NetworkUserInvoiceId))
        .TransitionTo(Initiated));

    WhenEnter(
      Initiated,
      x => x
        .Activity(
          activity => activity
            .OfType<NetworkUserInvoiceRegistrationActivity>())
        .IfElse(
          context => context.Saga.BillId is not null,
          x => x
            .Send(
              new Uri(
                options.Value.Messaging.Endpoints.RegisterNetworkUserInvoice),
              context =>
                new RegisterNetworkUserInvoice(
                  context.Saga.NetworkUserInvoiceId,
                  context.Saga.BillId!))
            .TransitionTo(Registered),
          x => x
            .Send(
              new Uri(
                options.Value.Messaging.Endpoints.AbortNetworkUserInvoice),
              context =>
                new AbortNetworkUserInvoice(
                  context.Saga.NetworkUserInvoiceId,
                  context.Saga.AbortReason!))
            .TransitionTo(Aborted)));
  }

  public State Initiated { get; } = default!;

  public State Registered { get; } = default!;

  public State Aborted { get; } = default!;

  public Event<IAcknowledgeNetworkUserInvoice> AcknowledgeNetworkUserInvoice
  {
    get;
  } = default!;
}
