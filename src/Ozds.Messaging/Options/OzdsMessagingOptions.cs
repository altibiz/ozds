namespace Ozds.Messaging.Options;

public record OzdsMessagingEndpointOptions(
  string AcknowledgeNetworkUserInvoice
);

public record OzdsMessagingSagaOptions(
  string NetworkUserInvoiceState
);

public record OzdsMessagingOptions(
  string ConnectionString,
  string PersistenceConnectionString,
  OzdsMessagingEndpointOptions Endpoints,
  OzdsMessagingSagaOptions Sagas
);
