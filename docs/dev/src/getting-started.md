# Getting Started with OZDS

Welcome to OZDS! We're excited to have you join the team. 🎉

## What is OZDS?

OZDS is an implementation of the new electricity market act in Croatia,
specifically for operators of closed distribution systems (OZDS in Croatian).
It's a platform for electrical data acquisition, storage, analysis, reporting,
and billing for:

- **Operators** - our primary clients who manage the entire network
- **Closed distribution systems** - businesses with lots of smaller businesses
  inside (think shopping malls, business parks)
- **Network users** - the individual businesses within those systems

Think of it like this: operators use our software to manage electrical billing
for complex business locations where one main business administers many smaller
tenants.

## Quick Setup

### Prerequisites

**If you have Nix:** Just hop in the directory, allow direnv to do its thing,
and you're ready!

**If you don't have Nix:** You'll need to install:

- `just` (command runner)
- `nushell` (shell)
- `docker` (containers)
- `git` (version control)
- `dvc` (large file version control)
- `dotnet` (backend)
- `node` (for tooling)

Yes, that's a lot of tooling, but trust us - every tool has its place and it
pays off hugely!

### Getting Started

1. **Clone the repo and navigate to it**
2. **Let direnv do its thing** (if using Nix)
3. **Run the setup:**

   ```bash
   just prepare
   ```

### What does `just prepare` do?

It's doing quite a bit behind the scenes:

- Initial build of the project
- Installs prettier (if not already installed)
- Pulls large files with `dvc pull` (dev database dumps, assets, etc.)
- Runs Docker containers (might take a while if pulling images)
- Waits until containers are ready
- Pulls an LLM model (ollama) for future features but also translation in the
  future when local models get better

### Starting Development

Once `just prepare` completes successfully (exit code 0), start the development
server:

```bash
just dev
```

This starts the .NET project with hot reload. Navigate to the dev page in your
browser and you're ready to develop!

## Architecture Overview

We try to follow 12-factor app principles as best we can. The main projects are:

- **`Ozds.Business`** - Core business logic and rules
- **`Ozds.Client`** - Frontend UI components
- **`Ozds.Server`** - Startup project

The rest of the projects are integrations with either currently external
services or planned external service.

### User Roles Explained

- **Operator representatives** - People from our client companies who manage
  everything
- **Location representatives** - People from closed distribution systems (the
  "landlords")
- **Network user representatives** - People from individual businesses (the
  "tenants")

We separate physical people (who log in) from legal entities (businesses)
because of Croatian trade law requirements.

## Development Workflow

### Making Your First Change

Want to test that everything works? Try editing something in the `Ozds.Client`
project - maybe change a button color or some text in a page component. The hot
reload should pick up your changes automatically!

### Don't Worry About Breaking Things

We have lots of safety nets:

- Linters catch style and obvious logic issues
- Tests catch critical logic problems
- Thorough code review hopefully catches everything else

**Why thorough code review?** We're dealing with billing and measurements - any
mistake here costs real money for us or our clients. Every line gets reviewed,
not because we're being picky, but because financial accuracy is critical.

If you still manage to break things after all that its not your fault but ours
for not having better safety nets.

### Hot Reload

When you make changes, the system will automatically reload. Sometimes it might
need to do a "rude edit" and recompile everything instead of hot reloading -
you'll just have to test and see what triggers this but it is not a big deal
since recompilation usually lasts less than ten seconds.

## Development Environment

### What's Running Locally?

When you run `just dev` after `just prepare`:

- Main program (UI + API)
- Database (via Docker Compose)

With `just fake <command>` you can fake:

- Sending measurements to the main program
- Our ERP system that processes billing information

All `just fake` commands accept a `--help` argument which will give you all the
details for using that command.

### Staging vs Production

We have a staging environment but we're in a "half-production" state right now.
We're rolling out slowly with some clients receiving bills and others not, while
dealing with construction delays and the occasional yanked wire! 🙄

## Integrations

### Automatic Translation

Never worry about localization while developing! Our system automatically scans
the codebase with regex and reflection, sends new strings to DeepSeek R1 API
(for now) for Croatian translation, and updates the translation files. Focus on
features, fix translations later when clients complain! 😄

### IoT

We work with ABB and Schneider electrical meters. Since these meters aren't very
"smart," we use Raspberry Pis running our "pidgeon" program to relay
measurements from meters to our cloud server via REST JSON. This program can be
faked via the `Ozds.Fake` project or the `just fake push` command which calls
it. If you need to seed the database with measurements use the
`just fake insert` command which will do that for you.

### ERP

Our ERP system is responsible for accounting our operators billing data. We send
and synchronize this data via an Azure Service Bus. In development,
`docker compose` will start a RabbitMQ instance instead of an Azure Service Bus
because there is no docker image for Azure Service Bus. The MassTransit library
handles implementation differences. The relevant parts of the ERP system can be
faked via the `Ozds.Fake` project or the `just fake altibiz` command which calls
it.

### Data Types

We handle three main types of data:

- **Authentication data** - users, roles, permissions (currently handled by
  Orchard Core's part of our PostgreSQL database)
- **Relational data** - locations, billing info (traditional database stuff
  which will most likely always be in PostgreSQL)
- **Timeseries data** - electrical measurements over time (voltage, current,
  power, energy consumption which we currently use TimescaleDB for)

Currently all use TimescaleDB, but we keep them logically separate in queries to
maintain developer sanity. Some day we hope to separate these into an auth
service integration (something like Auth0), relational data integration and
timeseries data integration. The reason is that all of this data has different
constraints and we treat them separately anyway.

## Need Help?

Don't hesitate to ask questions! This is a complex system (approaching 100k
lines across all repos), and there's a lot to unpack. The code review process is
designed to help you learn and ensure everything stays coherent with the
existing codebase.

Remember: we'd rather you try things and learn through code review than be
paralyzed by fear of breaking something. That's how we figure out the best way
to communicate and work together!

---

Ready to dive in? Run `just prepare` and let's build something awesome! 🚀
