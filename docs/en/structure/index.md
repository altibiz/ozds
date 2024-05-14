# OZDS project structure

<div style="display: none;">
  \page structure Structure

  <div>\subpage structure-data</div>
  <div>\subpage structure-business</div>
  <div>\subpage structure-client</div>
  <div>\subpage structure-fake</div>
  <div>\subpage structure-server</div>
  <div>\subpage structure-test</div>
</div>

The C# solution of the OZDS repository contains several projects each serving a
different purpose. Here is an outline of every project

## Ozds.Data

This project contains the database schema and queries and nothing else. The
`OzdsDbContext` class contains all database tables and the `OzdsDbClient`
contains all the queries needed for the OZDS server. All the entities (tables)
are situated in the `Ozds.Data.Entities` namespace.

## Ozds.Business

This project is the layer that sits between the `Ozds.Server` and `Ozds.Data`.
Any logic that has to sit between the server actions and queries goes here and
is organized in services that are added to the DI container. Every function
defined in this project is tested by the unit tests in the `Ozds.Business.Test`
project.

## Ozds.Server

This project is the startup project and contains all API controllers and the
entry point for the `Ozds.Client` project. The complexity in this project should
come only from the UI and any backend logic should be kept in the
`Ozds.Business` project.

## Ozds.Client

This project contains all the UI Razor files that will present data to the end
users. The complexity in this project should be kept at a minimum and any
extraneous logic should be kept in the `Ozds.Business` project.

## Ozds.Business.Test

This project tests everything in the `Ozds.Business` project and mimics its
structure.

## Ozds.Fake

This project is treated as a script that generates fake measurements for the
server. The complexity in this project should be kept at a minimum and this
project should use the `Ozds.Server` project to mimic the measurement structure.
