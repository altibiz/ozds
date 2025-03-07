# Wiki

This is the wiki for the OZDS project.

## Development

Here are some answers to questions developers might have when developing OZDS.

### How do I prepare OZDS for development?

Install [just](https://github.com/casey/just#packages),
[docker](https://docs.docker.com/engine/install/),
[node](https://nodejs.org/en/download), [dvc](https://dvc.org/),
[dotnet](https://github.com/dotnet/core/blob/main/release-notes/8.0/8.0.1/8.0.1.md?WT.mc_id=dotnet-35129-website),
[nushell](https://www.nushell.sh/) and
[powershell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell-on-windows).
Prepare `dvc` by asking a fellow developer for the configuration and following
their instructions. Finally, run `just prepare` from the command line.

### How do I start developing OZDS?

After [preparing OZDS for development](#how-do-i-prepare-ozds-for-development),
open OZDS in your editor of choice (we recommend
[Visual Studio Code](https://code.visualstudio.com/)) and run `just dev` on the
command line. We also recommend seeding the database with measurements
continuously by running `just fake` on another command line. After the server
starts, navigate to [localhost](http://localhost:5000) in your browser of
choice. Hot reload is enabled by default so there is no need of rerunning any
commands on changes.

### How do I debug OZDS?

Start [developing OZDS](#how-do-i-start-developing-ozds) except running
`just dev` on the command line and instead run the appropriate configuration in
your editor of choice. For Visual Studio Code, this will be the `debug`
configuration and for Visual Studio this will be the `Ozds.Server` startup
project.

### I have changed the database scheme of OZDS - how do I create a migration?

Run `just migrate {{name-of-migration}}` from the command line.

### How do I run tests on OZDS?

Most likely, your editor of choice will discover tests and you can run them from
there, but if you prefer, you can run `just test` on the command line. Please
refer to the
[dotnet test documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test)
for more details on the various options you can pass. The most common use case
is to limit the tests ran to a specific name and you can do that with
`just test --filter "FullyQualifiedName~{{your-test-name}}"`.

### Something has gone wrong with my development OZDS database - how do I reset it?

Run `just clean` from the command line.

### I am getting cryptic errors - what is the panic button?

Run `just purge` from the command line.
