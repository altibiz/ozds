# Deployment

<div style="display: none;">
  \page workflow-deployment Deployment
</div>

Deployment is done via github actions. The deployment workflow is defined in
`.github/workflows/deploy.yml`. The workflow is triggered on push to the `main`
branch. The workflow builds the OZDS web application and uploads it to an Azure
App Service.

```plantuml
start

:Push to main branch;

if (Build successful?) then (yes)
  :Upload artifacts to Azure App Service;
else (no)
  :Notify failure via email;
endif

stop
```

Here is the full deployment graph for OZDS:

```plantuml
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

package "ZDS 1" {
  node "Abb B2x 1" as abb11
  node "Schneider iEM3xxx 1" as schneider11
  node "Modbus TCP/IP gateway 1" as gateway11
  node "Abb B2x 2" as abb12
  node "Schneider iEM3xxx 2" as schneider12
  node "Modbus TCP/IP gateway 2" as gateway12
  node "Raspberry Pi 4B 1" as messenger1
}

package "ZDS 2" {
  node "Abb B2x 1" as abb21
  node "Schneider iEM3xxx 1" as schneider21
  node "Modbus TCP/IP gateway 1" as gateway21
  node "Abb B2x 2" as abb22
  node "Schneider iEM3xxx 2" as schneider22
  node "Modbus TCP/IP gateway 2" as gateway22
  node "Raspberry Pi 4B 2" as messenger2
}

abb11 --> gateway11 : RS485
schneider11 --> gateway11 : RS485
abb12 --> gateway12 : RS485
schneider12 --> gateway12 : RS485
gateway11 --> messenger1 : Modbus TCP/IP
gateway12 --> messenger1 : Modbus TCP/IP

abb21 --> gateway21 : RS485
schneider21 --> gateway21 : RS485
abb22 --> gateway22 : RS485
schneider22 --> gateway22 : RS485
gateway21 --> messenger2 : Modbus TCP/IP
gateway22 --> messenger2 : Modbus TCP/IP

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
```
