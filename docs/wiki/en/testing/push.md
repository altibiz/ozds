# Push

According to our [architecture](../architecture.md), from the standpoint of the
cloud server, there are multiple points from which errors can occur. Starting
from the locations and going to the database, we can expect to see errors in
these areas:

- **Raspberry PI -> Server**: The Raspberry PI is responsible for sending data
  from the meters to the server. If the Raspberry PI loses connection to the
  server, the server will not receive any data. The Raspberry PI could also send
  incorrect data or not be authorized to send data.

- **Server**: The server itself could stop working or have a bug that causes it
  to stop receiving data from the Raspberry PI.

- **Server -> Database**: The server sends data to the database. If the database
  is down or there is no connection to the database or there are query problems,
  the server will not be able to store the data.

## Failures

Here is a list of failures that can occur in the push process divided into
areas:

- **Raspberry PI -> Server**:

  - Raspberry PI is not sending data
  - Raspberry PI is sending incorrect data

- **Server**:

  - Server is not connected to the network
  - Server throws an exception (software bug)

- **Server -> Database**:
  - Database is not connected to the network
  - Database throws an exception (software bug)

## Testing

To test resiliency in the push process, we can simulate failures in the
following ways:

- Raspberry PI not sending data: shut down the fake push script and see how the
  server behaves. The server should not stop working and create an alarm after
  some time.

- Raspberry PI sending incorrect data: create CSV files with incorrect data from
  CSV files with correct data and fake push those measurements to the server.
  The server should not stop working and should create an alarm.

- Server is not connected to the network: shut down the server and start it back
  up after some time. The server should start working again, not a single
  measurement should be lost (fake script should retry same as application), and
  an alarm should be created.

- Server throws an exception: create a bug in the server that causes it to stop
  processing incoming measurements. The server should not stop working and
  create an alarm.

- Database is not connected to the network: shut down the database and start it
  back up after some time. The server should start working again, not a single
  measurement should be lost (fake script should retry same as application), and
  an alarm should be created.

- Database throws an exception: create a bug in the server that causes the
  database to stop processing incoming measurements. The server should not stop
  working and create an alarm.
