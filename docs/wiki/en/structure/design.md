# Design

- `.` - Config files
- `src` - Source code
- `docs` - Documentation
- `test` - Test code
- `scripts` - Script code

## Source code

Contains projects.

Ozds.Server is the startup project.

Ozds.Business is the business/core logic project.

All other projects are integration projects. This is to separate external
library use from core logic.

### Ozds.Business

Folders contain functionality for what they are named.

Lots of namespaces are visitors to have business logic over entities in
integration projects in this project:

- Activation - Creation of new models/entities
- Aggregation - Aggregation of measurements/aggregates
- Capabilities - Capabilities of meter models/entities
- Conversion - Conversion of models/entities, measurements->aggregates, push
  requests -> models
- Naming - How naming of meters dictates types

Queries/mutations route requests to other systems/namespaces.

Observers are classes that dispatch events and define events.

Reactors are business logic that triggers on events.

Assets for now contain only translations.

Extensions are just C# stuff.

Models are data definitions on which business logic is performed.

The rest are business logic that is tested in Ozds.Business.Test.

### Ozds.Data

Folders contain functionality for what they are named.

Queries/mutations route requests to other systems/namespaces.

Interceptors react to database queries - EF Core thing.

Observers are classes that dispatch events and define events.

Timescale is timescale related queries.

Migrations are migrations and you do not touch them unless absolutely necessary.

Options are config definitions.

Entities are data definitions.

Context is EF Core DB Context.

### Ozds.Email

Folders contain functionality for what they are named.

Extensions are just C# stuff.

Sender is email sending implementation via MailKit.

### Ozds.Iot

Entities are data definitions.

Extensions are just C# stuff.

Observers are classes that dispatch events and define events.

Converters define conversions via System.Text.Json.

### Ozds.Jobs

Context is EF Core DB Context.

Extensions are just C# stuff.

Jobs is Quartz job definitions.

Managers take care of job scheduling.

Migrations are migrations and you do not touch them unless absolutely necessary.

Options are config definitions.

Observers are classes that dispatch events and define events.

### Ozds.Messaging

Context is EF Core DB Context.

Contracts are what gets sent.

Entities are data definitions.

Extensions are just C# stuff.

Migrations are migrations and you do not touch them unless absolutely necessary.

Observers are classes that dispatch events and define events.

Options are config definitions.

Sagas define state machines - what happens when altibiz sends us an event.

Sender defines where to send messages.

### Ozds.Server

Middleware is adds functionality to request handling.

Controllers control responses to requests.

Extensions are just C# stuff.

View models define view data.

Views are how we present stuff (basically just Blazor entrypoint).

### Ozds.Users

Entities are data definitions.

Extensions are just C# stuff.

Queries/mutations route requests to other systems/namespaces.

### Ozds.Client

App is entrypoint.

Theme is the theme.

Attributes are C# stuff - metadata.

Base is base components - keep this very minimal.

Extensions are just C# stuff.

Pages are web pages.

Components are Blazor components.

State is cascading values - state of the app.

Components:

- Charts - chart components
- Dialogs - dialog components - used with MudBlazor DialogService
- Fields - Field components
- Layout - layout related components
- Models - model related components - no fetching here except joins
- Streaming - components that control the flow querying/mutations
- Print - components to be rendered inside PrintLayout

## TODO

### Ozds.Client

- flatten Components folder/namespace
- move more logic to business
- ban methods in @code
