# OZDS Project Style Guide

## Overview

This style guide documents the patterns, conventions, and architectural
decisions used in the OZDS project. Each rule exists for specific reasons -
usually to maintain decoupling, improve maintainability, or prevent common
mistakes. Understanding the "why" behind these patterns is just as important as
following them.

## Core Architectural Principles

### 12-Factor App Structure

We follow the 12-factor app methodology with strict project separation:

- **Ozds.Client & Ozds.Server**: Startup and client projects for convenience
- **Ozds.Business**: Core logic project - the heart of our application
- **Integration Projects**: Handle specific external services (databases, APIs,
  etc.)

**Dependency Flow**: `Client/Server` → `Business` → `Integration Projects`

**Why**: This separation ensures our core business logic remains completely
independent from external services. When an external service becomes inadequate
or changes, we can swap it out without touching the core logic. The frontend
also stays stable across such migrations because it only depends on
`Ozds.Business`.

## Entity/Model Pattern

### The Golden Rule: External ↔ Internal Separation

For every external service integration:

1. **Entity Class**: Created in the integration project, matches external
   service 1:1 (database record, API response, queue message, etc.)
2. **Model Class**: Created in `Ozds.Business`, represents the concept for
   internal use
3. **Converter Class**: Bridges between entity and model

**Example**:

```txt
SomeServiceUserEntity (in integration project)
     ↓ (via SomeServiceUserEntityConverter)
UserModel (in Ozds.Business)
```

**Why**: The rest of the codebase that does real work is totally independent
from external services - it doesn't even know where the data came from! This
allows us to change external services without affecting core logic.

## Naming Conventions

### Verbosity Over Ambiguity

**Rule**: Always prefer verbose, descriptive names over short, ambiguous ones.

**Why**: We'd rather know exactly what something does than spend time guessing.
Code is read far more often than it's written.

**Examples**:

- ✅ `NetworkUserInvoiceModelChangedEventArgs`
- ❌ `UserInvChgArgs`
- ✅ `ActiveEnergyTotalImportT1PowerCalculationItemEntity`
- ❌ `AETIPCalcItem`

### Acceptable Abbreviations

Use abbreviations only when they're universally understood:

**Always OK**:

- `Id` (instead of `Identifier`)
- `Max`, `Min`, `Avg` (mathematical terms)
- Domain-specific terms that are widely accepted (like database terminology)

**When in doubt, spell it out.**

### Event Naming Pattern

Format: `[Name]Model[Action]EventArgs`

**Examples**:

- `NetworkUserInvoiceModelChangedEventArgs`
- `MeasurementModelCreatedEventArgs`

## Electrical Property Naming Convention

**This is the language of our project** - electrical properties follow a
specific format that covers all Croatian energy market scenarios.

### Format

```txt
({Item})?{PropertyName}{Phases}{Direction}{Tariff}_{Unit}(.{Aggregate}?)
```

### Components Explained

- **Item** (optional): `Supply` or `Usage` - related to what calculation section
  it is a part of if applicable
- **PropertyName**: the electrical measurement
- **Phases**:
  - `L1`/`L2`/`L3` - specific phase
  - `Total` - all phases
- **Direction**:
  - `Import` - consumption direction
  - `Export` - production direction
  - `Any` - direction doesn't matter
- **Tariff**:
  - `T0` - one tariff
  - `T1` - higher tariff (Croatian peak hours)
  - `T2` - lower tariff (Croatian off-peak hours)
- **Unit**: physical unit with SI prefix if applicable
- **Aggregate** (optional): `Min`/`Max`/`Avg`/`MinTimestamp`/`MaxTimestamp`

### Examples

- `ActiveEnergyTotalImportT1_kWh` → "Active energy on all phases imported higher
  tariff (kWh)"
- `VoltageTotalAnyT0_V.Max` → "Maximum voltage on all phases one tariff (V)"

**Why**: This convention came from implementing measurement logic that covers
all Croatian energy market requirements. It's verbose but eliminates any
ambiguity about what measurement we're dealing with.

## Project Structure Patterns

### Functionality-First Organization

We group by functionality (Queries, Mutations, Reactors, Observers) rather than
by entity type.

**Why**:

- **Critical operations get more attention**: Mutations are usually more
  critical than queries, so grouping them makes developers think more carefully
  about them
- **Testing strategy alignment**: Different functionality types need different
  testing approaches (e.g., financial calculations need fuzzy testing,
  converters just need coverage verification)
- **Optimization insights**: If you're doing more queries than mutations, you
  might have an optimization problem

### Abstractions vs Base vs Implementations

**Abstractions Namespace**: Interfaces only

**Base Namespace**: Abstract base classes with shared implementation

**Implementations Namespace**: Concrete implementations

**Root Namespace**: Optional top-level classes that fetch the right
implementation from DI container according to the type of model

**Rule**: Outside code should use dependency injection and rely on interfaces,
not concrete classes.

**Why**: This discourages tight coupling to specific implementations. If you
need a converter, inject `ModelEntityConverter` (top-level class), don't
directly inject some `ConcreteModelEntityConverter`.

### When to Create Base Classes

Create abstract base classes when there's behavior common to all
implementations.

**Pro tip**: It's often worth creating base classes preemptively - you usually
discover shared behavior later, and a base class never hurt anyone.

**Why**: Our inheritance chains enable powerful DRY patterns. For example, you
can define a converter for a base class that handles common fields, and it
automatically works for all derived classes (with override capability when
needed).

## Observer/Reactor Pattern (Current Approach)

### The Problem: Reverse Dependencies

Our strict hierarchy prevents integration projects from directly calling
business logic. But sometimes integration projects need to trigger business
operations.

### The Solution: Pub/Sub with Observers and Reactors

**Current Implementation**: Built on C# Channels and Events

**Flow**:

1. **Integration project** declares observers (singletons with pub/sub
   capabilities)
2. **Business project** creates reactors that subscribe to integration events
3. **Relays** convert entities to models before publishing (maintaining
   decoupling)

**Example Flow**:

```txt
Integration Project publishes entity event
     ↓
Relay converts entity to model
     ↓
Relay publishes model event
     ↓
Business reactor subscribes to model event
```

**Frontend**: Components subscribe in `OnParametersSet` and unsubscribe in
`OnDispose` - standard pub/sub pattern.

**Why**: This maintains our clean dependency hierarchy while allowing
integration projects to trigger business logic. Everything stays decoupled from
external services.

**Note**: This introduces some state into our app. We have plans to make it more
stateless later, but the changes won't affect the calling code (reactors and
pub/sub logic will remain the same).

## Blazor Component Guidelines

### The .razor + .cs Pattern

**Start with**: `.razor` file only **Add `.cs` file when**: You need functions
(event handlers, lifecycle methods, etc.)

**Why**: Blazor tooling isn't as good as general C# tooling. If you put service
calls in `.razor` files, you can't use F2 to rename, and LSP actions break. The
separate `.cs` file gives you full C# tooling support.

**What stays in .razor**:

- Component parameters and properties (these work fine with LSP)
- Simple `@code` blocks without functions
- Markup and basic binding

**What goes in .cs**:

- Event handlers
- Lifecycle methods
- Any service interactions
- Complex logic

## Error Handling

### Current Approach: Exceptions + Observability

We use standard C# exceptions with an observer pattern for error tracking:

- `ErrorObserver` publishes errors for logging/email notifications to developers
- Errors get stored and sent to developers who are also users in production

### Desired Pattern: Defensive Programming

**Rule**: For critical functions with clear expectations not captured by the
type system, use asserts.

**Assert vs Exception Guidelines**:

- **Asserts**: "This should never happen if my logic is correct" (runs in debug
  mode)
- **Exceptions**: "This could happen due to external factors"

**Why**: Asserts help catch logic errors during development without impacting
production performance.

## Living Document

This style guide evolves with our codebase. When you encounter patterns not
covered here, or find rules that need clarification:

1. Discuss with the team
2. Update this document
3. Share your reasoning

**Why**: We learn from each other's questions and mistakes. Your confusion today
prevents someone else's confusion tomorrow.

---

_Remember: These patterns exist to make our lives easier, not harder. If a rule
seems to be causing more problems than it solves, let's talk about it and
potentially evolve our approach._
