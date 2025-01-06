namespace Ozds.Fake;

public record OzdsFakeOptions(
  OzdsFakeMessagingOptions Messaging
);

public record OzdsFakeMessagingOptions(
  OzdsFakeMessagingEndpointsOptions Endpoints,
  OzdsFakeMessagingSagasOptions Sagas
);

public record OzdsFakeMessagingEndpointsOptions(
  string InitiateNetworkUserInvoice,
  string AbortNetworkUserInvoice,
  string RegisterNetworkUserInvoice
);

public record OzdsFakeMessagingSagasOptions(
  string NetworkUserInvoiceState
);
