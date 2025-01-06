# Billing

As is shown in the [billing workflow](../workflow/billing.md), billing is an
orchestrated process that involves the cloud server, the database, and the
Altibiz ERP server. With this in mind, we can identify the following points of
failure:

- **Server -> Database**: The server sends data to the database. If the database
  is down or there is no connection to the database or there are query problems,
  the server will not be able to store the data.

- **Server**: The server itself could stop working or have a bug that causes it
  to stop from communicating to other services.

- **Server -> Altibiz ERP**: The cloud server sends billing data to the Altibiz
  ERP server. If the Altibiz ERP server is down or there is no connection to the
  Altibiz ERP server or there are data problems, the cloud server will not be
  able to store the response. This point of failure will be expanded on when
  proper integration with the Altibiz ERP server is implemented.

## Failures

Here is a list of failures that can occur in the billing process divided into
areas:

- **Server -> Database**:

  - Database is not connected to the network
  - Database throws an exception (software bug)

- **Server**:

  - Server is not connected to the network
  - Server throws an exception (software bug)

- **Server -> Altibiz ERP**:

  - Altibiz ERP server is not connected to the network
  - Altibiz ERP server is sending incorrect data
  - Altibiz ERP server throws an exception (software bug)

Note that each failure could come up in different states of the billing process.

## Testing

To test resiliency in the billing process, we can simulate failures in the
following ways:

- Database is not connected to the network: shut down the database and start it
  back up after some time. The server should start working again and all data
  should be stored correctly. If any operation is complete the server should
  continue those operations.

- Database throws an exception: create a bug in the server that causes the
  database to stop processing incoming queries. The server should not stop
  working and create an alarm.

- Server is not connected to the network: shut down the server and start it back
  up after some time. The server should start working again and all data should
  be stored correctly, and an alarm should be created. If any operation is
  complete the server should continue those operations.

- Server throws an exception: create a bug in the server that causes it to stop
  processing incoming queries. The server should not stop working and create an
  alarm.

- Altibiz ERP server is not connected to the network: shut down the fake altibiz
  server and start it back up after some time. The server should start working
  again and all data should be stored correctly, and an alarm should be created.

- Altibiz ERP server throws an exception
