# Scripts

<div style="display: none;">
  \page script Scripts
</div>

This is documentation for the various scripts used for development defined in
the [justfile](justfile).

Install [just](https://github.com/casey/just#packages),
[docker](https://docs.docker.com/engine/install/),
[node](https://nodejs.org/en/download), [dvc](https://dvc.org/),
[dotnet](https://github.com/dotnet/core/blob/main/release-notes/8.0/8.0.1/8.0.1.md?WT.mc_id=dotnet-35129-website)
and [nushell](https://www.nushell.sh/). Prepare `dvc` by asking a fellow
developer and run `just prepare` from the command line before running any other
script.

## `just prepare`

This script installs installs prettier if not available, dotnet packages and
tools and runs the PostgreSQL database using docker compose in the background.

## `just ci`

This script is only used for CI and is not intended for developers. It installs
dotnet tools and prettier if not available.

## `just dev`

This script starts a dotnet process that starts up `Ozds.Server` and watches for
changes in the solution providing hot reloading of the server.

## `just fake`

This script runs the [`Ozds.Fake`](scripts/Ozds.Fake) project that sends fake
measurements to the local server.

## `just format`

This script formats every file in the repository using
[`dotnet jb`](https://www.nuget.org/packages/JetBrains.ReSharper.GlobalTools)
tool for C# and Razor files and [prettier](https://prettier.io/) for everything
else and is used on CI to keep the OZDS repository formatted on every PR.

## `just lint`

This script lints the repository by checking formatting with
[prettier](https://prettier.io/) and using
[`dotnet jb`](https://www.nuget.org/packages/JetBrains.ReSharper.GlobalTools)
tool to check for warnings and errors in C# and Razor files.

## `just test {*args}`

This script tests C# code with the `dotnet test` command.

## `just migrate {name}`

This script generates a migration in the [`Ozds.Data`](src/Ozds.Data) project
using the [`dotnet ef`](https://www.nuget.org/packages/dotnet-ef) tool.

## `just publish`

This script compiles the solution for release inside the
[artifacts directory](artifacts) using the `dotnet publish` command.

## `just clean`

This script cleans Orchard Core App Data files, removes all docker containers
and volumes and starts them back up.

## `just purge`

This scripts does the same as `just clean` except that, in addition, it uses
`git clean` to remove any compiled artifacts and restores all dotnet tools and
packages.
