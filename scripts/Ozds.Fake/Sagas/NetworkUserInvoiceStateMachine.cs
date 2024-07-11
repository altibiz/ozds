using MassTransit;
using Ozds.Messaging.Contracts;
using Ozds.Messaging.Contracts.Abstractions;
using Ozds.Messaging.Sagas;

// TODO: make queue names configurable

namespace Ozds.Fake.Sagas;

public class NetworkUserInvoiceStateMachine
  : MassTransitStateMachine<NetworkUserInvoiceState>
{
  public NetworkUserInvoiceStateMachine(IConfiguration configuration)
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
            new NetworkUserInvoiceState
            {
              CorrelationId = context.CorrelationId ?? NewId.NextGuid(),
              NetworkUserInvoiceId = context.Message.NetworkUserInvoiceId
            });
      });

    var config = configuration
      .GetSection("Ozds")
      .GetSection("Messaging")
      .GetSection("Endpoints");

    Initially(
      When(AcknowledgeNetworkUserInvoice)
        .Then(
          context => context.Saga.NetworkUserInvoiceId
            = context.Message.NetworkUserInvoiceId)
        .Send(
          new Uri(
            config["InitiateNetworkUserInvoice"]
            ?? throw new InvalidOperationException(
              "InitiateNetworkUserInvoice endpoint not found")),
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
                config["RegisterNetworkUserInvoice"]
                ?? throw new InvalidOperationException(
                  "RegisterNetworkUserInvoice endpoint not found")),
              context =>
                new RegisterNetworkUserInvoice(
                  context.Saga.NetworkUserInvoiceId,
                  context.Saga.BillId!))
            .TransitionTo(Registered),
          x => x
            .Send(
              new Uri(
                config["CancelNetworkUserInvoice"]
                ?? throw new InvalidOperationException(
                  "CancelNetworkUserInvoice endpoint not found")),
              context =>
                new CancelNetworkUserInvoice(
                  context.Saga.NetworkUserInvoiceId))
            .TransitionTo(Cancelled)));
  }

  public State Initiated { get; } = default!;

  public State Registered { get; } = default!;

  public State Cancelled { get; } = default!;

  public Event<IAcknowledgeNetworkUserInvoice> AcknowledgeNetworkUserInvoice
  {
    get;
  } = default!;
}
