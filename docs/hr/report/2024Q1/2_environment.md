# Okruženje

<div style="display: none;">
  \page izvjesce-2024-q1-okruzenje Okruženje
</div>

OZDS je ASP.NET Core web aplikacija smještena na Azure App Service. Aplikacija
se povezuje s Postgresql bazom podataka s TimescaleDB ekstenzijom. Aplikacija
prima dolazne podatke s Raspberry Pi 4B uređaja putem HTTP POST zahtjeva.
Korisnici pristupaju aplikaciji putem web preglednika. Aplikacija se prikazuje
putem Blazor SSR.

## Razvoj

Alati koji se koriste za razvoj OZDS-a su:

- [dotnet](https://github.com/dotnet/core/blob/main/release-notes/8.0/8.0.1/8.0.1.md?WT.mc_id=dotnet-35129-website)
- [just](https://github.com/casey/just#packages)
- [docker](https://docs.docker.com/engine/install/)
- [node](https://nodejs.org/en/download)
- [dvc](https://dvc.org/)
- [git](https://git-scm.com/)
- [nushell](https://www.nushell.sh/)

Kod za poslužitelj nalazi se na [github-u](https://github.com/altibiz/ozds). Kod
se razvija u [vscode](https://code.visualstudio.com/).

Kod za Raspberry Pi 4B posrednike se nalazi na
[github-u](https://github.com/altibiz/pidgeon). Kod za te uređaje obično se
razvija spajanjem na njih preko SSH. Posrednici rade na linux distribuciji
zvanoj NixOS. Razlog za to je što NixOS olakšava reprodukciju istog okruženja na
svim uređajima.

## Implementacija

Implementacija se vrši putem github akcija. Radni proces implementacije
definiran je u `.github/workflows/deploy.yml`. Radni proces se pokreće prilikom
`git push` na `main`. Radni proces gradi OZDS web aplikaciju i prenosi je na
Azure App Service.

```plantuml
start

:Push na glavnu granu;

if (Je li izgradnja uspješna?) then (da)
  :Prenos artifakata na Azure App Service;
else (ne)
  :Obavijest o neuspjehu putem emaila;
endif

stop
```

## Arhitektura

Evo potpunog grafa implementacije za OZDS:

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
  node "Modbus TCP gateway 1" as gateway11
  node "Abb B2x 2" as abb12
  node "Schneider iEM3xxx 2" as schneider12
  node "Modbus TCP gateway 2" as gateway12
  node "Raspberry Pi 4B 1" as messenger1
}

package "ZDS 2" {
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

Ozds.Business ..> Ozds.Data : koristi
Ozds.Data --> dbPort : povezuje se

messengerPushEndpoint --> IotController : sluša
IotController ..> Ozds.Business : koristi

AppController <-- ozdsUiEndpoint : sluša
Ozds.Client <.. AppController : koristi
Ozds.Client ..> Ozds.Business : koristi

ozdsUiEndpoint <-- C1 : Google Chrome
ozdsUiEndpoint <-- C2 : Microsoft Edge
ozdsUiEndpoint <-- C3 : Safari
```
