# [Ozds.Server](src/Ozds.Server)

This project holds the entry point to the `Ozds.Client` project and all
necessary API controllers. The controllers reside in the `Controller` namespace.
Every controller action is authorized and should only wrap inputs and outputs of
particular functions inside `Ozds.Business`.

## `Ozds.Server.Controllers`

There are a couple of marker interfaces that are used to implement the UI and
API:

- App: this is the entrypoint to the UI of the application and it serves pages
  from the `Ozds.Client` project. It is used to serve the `index.html` file and
  all other static files that are needed to render the UI.

- IOT: entrypoint to the `push` and `poll` actions which are needed to push
  measurements to the database and poll configuration for messengers.
