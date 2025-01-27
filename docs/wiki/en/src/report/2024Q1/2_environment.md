# Environment

OZDS is an ASP.NET Core web application hosted on Azure App Service. The
application connects to a Postgresql database with TimescaleDB extension. The
application listens for incoming data from Raspberry Pi 4B devices via HTTP POST
requests. Users access the application via a web browser. The application is
rendered via Blazor SSR.

## Development

Tools used to develop OZDS are:

- [dotnet](https://github.com/dotnet/core/blob/main/release-notes/8.0/8.0.1/8.0.1.md?WT.mc_id=dotnet-35129-website)
- [just](https://github.com/casey/just#packages)
- [docker](https://docs.docker.com/engine/install/)
- [node](https://nodejs.org/en/download)
- [dvc](https://dvc.org/)
- [git](https://git-scm.com/)
- [nushell](https://www.nushell.sh/)

Code for the server is hosted on [github](https://github.com/altibiz/ozds). The
code is developed in [vscode](https://code.visualstudio.com/).

Code for the Raspberry Pi 4B messengers is also hosted on
[github](https://github.com/altibiz/ozds). Code for these devices is usually
developed by connecting to them via SSH. Messengers are running on a linux
distribution called NixOS. The reasoning behind this is that NixOS makes it is
easy to reproduce the same environment on all devices.

## Deployment

Deployment is done via github actions. The deployment workflow is defined in
`.github/workflows/deploy.yml`. The workflow is triggered on push to the `main`
branch. The workflow builds the OZDS web application and uploads it to an Azure
App Service.

```plantuml
@startuml

start

:Push to main branch;

if (Build successful?) then (yes)
  :Upload artifacts to Azure App Service;
else (no)
  :Notify failure via email;
endif

stop

@enduml
```

## Architecture

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

cloud "Azure Cloud" {
  package "ASP.NET Core Ozds.Server" {
    portin messengerPushEndpoint
    portout ozdsUiEndpoint

    component Ozds.Data
    component Ozds.Client
    component Ozds.Business

    component AppController
    component IotController
  }

  database "Postgresql + TimescaleDB Database" {
    port dbPort
  }
}

package "Location 1" {
  node "Abb B2x 1" as abb11
  node "Schneider iEM3xxx 1" as schneider11
  node "Modbus TCP gateway 1" as gateway11
  node "Abb B2x 2" as abb12
  node "Schneider iEM3xxx 2" as schneider12
  node "Modbus TCP gateway 2" as gateway12
  node "Raspberry Pi 4B 1" as messenger1
}

package "Location 2" {
  node "Abb B2x 1" as abb21
  node "Schneider iEM3xxx 1" as schneider21
  node "Modbus TCP gateway 1" as gateway21
  node "Abb B2x 2" as abb22
  node "Schneider iEM3xxx 2" as schneider22
  node "Modbus TCP gateway 2" as gateway22
  node "Raspberry Pi 4B 2" as messenger2
}

abb11 --> gateway11 : Modbus RTU
schneider11 --> gateway11 : Modbus RTU
abb12 --> gateway12 : Modbus RTU
schneider12 --> gateway12 : Modbus RTU
gateway11 --> messenger1 : Modbus TCP
gateway12 --> messenger1 : Modbus TCP

abb21 --> gateway21 : Modbus RTU
schneider21 --> gateway21 : Modbus RTU
abb22 --> gateway22 : Modbus RTU
schneider22 --> gateway22 : Modbus RTU
gateway21 --> messenger2 : Modbus TCP
gateway22 --> messenger2 : Modbus TCP

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
