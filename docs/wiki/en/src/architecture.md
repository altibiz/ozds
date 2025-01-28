# Architecture

The architecture of OZDS is a distributed system with multiple locations. The
system consists of a server running the ASP.NET Core application, a PostgreSQL
database, and multiple locations with meters. The meters are connected to the
server via a Raspberry Pi acting as a messenger. The server receives data from
the meters and stores it in the database. The server also serves the web
application to clients.

The server hosts the ASP.NET Core application, which is divided into three main
parts:

- **Ozds.Data**: Data access layer
- **Ozds.Client**: Client application
- **Ozds.Business**: Business logic

Here is the full deployment graph for OZDS:

```plantuml
@startuml

cloud "Cloud" {
  node "Ozds.Server" {
    portin messengerPushEndpoint as "/iot/push"
    portout ozdsUiEndpoint as "/app"

    component Ozds.Data
    component Ozds.Client
    component Ozds.Business

    component AppController
    component IotController
  }

  database "Relational database" {
    portin 5432 as dbPort
  }
}

cloud "Location 1" {
  node "Meter 1" as meter11
  node "Meter 2" as meter12
  node "Raspberry Pi 4B 1" as messenger1
}

cloud "Location 2" {
  node "Meter 1" as meter21
  node "Meter 2" as meter22
  node "Raspberry Pi 4B 2" as messenger2
}

meter11 --> messenger1 : RS485
meter12 --> messenger1 : RS485

meter21 --> messenger2 : RS485
meter22 --> messenger2 : RS485

node "Client1" as C1
node "Client2" as C2
node "Client3" as C3

messenger1 --> messengerPushEndpoint : HTTP POST
messenger2 --> messengerPushEndpoint : HTTP POST

Ozds.Business ..> Ozds.Data : uses
Ozds.Data --> dbPort : connects to

messengerPushEndpoint --> IotController : listens to
IotController ..> Ozds.Business : uses

AppController <-- ozdsUiEndpoint : listens to
Ozds.Client <.. AppController : uses
Ozds.Client ..> Ozds.Business : uses

ozdsUiEndpoint <-- C1 : Google Chrome
ozdsUiEndpoint <-- C2 : Microsoft Edge
ozdsUiEndpoint <-- C3 : Safari

@enduml
```
