# PRD: Identity Management Migration — Authelia/LDAP to ASP.NET Core Identity

| Field          | Value                     |
| -------------- | ------------------------- |
| Author         | OZDS Engineering          |
| Status         | Draft                     |
| Created        | 2026-03-03                |
| Target Release | TBD                       |
| Stakeholders   | Backend, DevOps, Security |

---

## 1. Problem Statement

OZDS currently uses **Authelia** (OIDC provider) + **lldap** (LDAP user store)
as two separate Docker containers for identity management. The `Ozds.Users`
module bridges both via:

- `Novell.Directory.Ldap` for user CRUD (create, read, update, delete, password
  change)
- `Microsoft.AspNetCore.Authentication.OpenIdConnect` for authentication flow

### Pain Points

- Two extra infrastructure services to maintain (Authelia + lldap containers)
- LDAP is a legacy protocol with poor developer experience
- No native .NET integration — manual LDAP operations for every user action
- Dual data source complexity: identity in LDAP, business data in PostgreSQL
- Limited extensibility (adding claims, roles, or OAuth providers requires
  Authelia configuration changes)

---

## 2. Goals

1. **Eliminate external identity infrastructure** — remove Authelia and lldap
   Docker containers
2. **Unify data storage** — all user data in the existing PostgreSQL database
3. **Native .NET identity management** — use Microsoft-provided, first-party
   APIs for user CRUD, authentication, and authorization
4. **Enable social login** — support optional OAuth providers (Google, Facebook,
   Microsoft) with minimal configuration
5. **Preserve business data integrity** — existing `RepresentativeEntity`
   references must continue to work without database migration
6. **Self-hosted** — no cloud dependencies, no SaaS

### Non-Goals

- IoT messenger authentication (remains HMAC-based, untouched)
- Authorization policy changes in `Ozds.Business`
- Building a standalone identity service for external consumers (can be added
  later via OpenIddict)
- Multi-tenancy / realm separation

---

## 3. Current Integration Surface

### 3.1 Architecture (Before)

```
┌──────────────┐   OIDC   ┌──────────────┐
│   Authelia   │◄────────►│  Ozds.Server │
│   (Docker)   │          └──────┬───────┘
└──────┬───────┘                 │
       │ LDAP              ┌────▼───────┐
┌──────▼───────┐           │ Ozds.Users │
│    lldap     │◄─────────►│  (Novell   │
│   (Docker)   │  Novell   │   .Ldap)   │
└──────────────┘  .Ldap    └────────────┘
```

### 3.2 File Inventory

| Layer                  | Technology                         | Files                                        |
| ---------------------- | ---------------------------------- | -------------------------------------------- |
| OIDC Authentication    | Authelia, OpenIdConnect middleware | `Ozds.Users/Extensions/HostExtensions.cs`    |
| User CRUD              | lldap, Novell.Directory.Ldap       | `Ozds.Users/Queries/UserQueries.cs`          |
| User Mutations         | lldap, Novell.Directory.Ldap       | `Ozds.Users/Mutations/UserMutations.cs`      |
| Password Management    | lldap, LDAP Extended Operation     | `Ozds.Users/Mutations/PasswordMutations.cs`  |
| Configuration          | OIDC + LDAP connection strings     | `Ozds.Users/Options/OzdsUsersOptions.cs`     |
| App Settings           | OIDC + LDAP sections               | `Ozds.Server/appsettings.Development.json`   |
| Docker                 | authelia:4.39.4 + lldap:2025-05-19 | `docker-compose.yml`                         |
| Authelia Config        | OIDC client, LDAP backend          | `scripts/auth/configuration.yml`             |
| User Entity (identity) | Simple DTO: Id, Name, Email        | `Ozds.Users/Entities/UserEntity.cs`          |
| User Entity (business) | RepresentativeEntity, string ID    | `Ozds.Data/Entities/RepresentativeEntity.cs` |

### 3.3 Key Coupling Point

`RepresentativeEntity` in PostgreSQL stores the LDAP `uid` as its identifier
(string). All business logic — invoices, locations, notifications — references
users through this ID. The migration strategy must preserve this mapping.

---

## 4. Solution Evaluation

### 4.1 Options Considered

| Criterion           | ASP.NET Core Identity | Keycloak           | Duende IdentityServer | OpenIddict            | Entra ID     |
| ------------------- | --------------------- | ------------------ | --------------------- | --------------------- | ------------ |
| Embedded in .NET    | **Yes**               | No (Java/Docker)   | Yes                   | Yes                   | No (Cloud)   |
| Self-hosted         | **Yes**               | Yes                | Yes                   | Yes                   | **No**       |
| Free / OSS          | **MIT**               | Apache 2.0         | **RPL (Paid)**        | Apache 2.0            | Consumption  |
| User CRUD API       | **UserManager\<T\>**  | Admin REST API     | None (needs Identity) | None (needs Identity) | Graph API    |
| PostgreSQL support  | **Native (Npgsql)**   | Own DB required    | Via EF Core           | Via EF Core           | N/A          |
| External OAuth      | **Built-in**          | Identity Brokering | Via Identity          | Via .NET handlers     | External IDs |
| Password management | **Built-in**          | Built-in           | Via Identity          | Via Identity          | Built-in     |
| Admin UI            | Must build            | **Built-in**       | Must build            | Must build            | Azure Portal |
| Setup complexity    | **Low**               | Medium             | High                  | Medium                | Low          |

### 4.2 Decision

**ASP.NET Core Identity** is the recommended solution.

**Rationale:**

- Only option that is embedded, free, and provides native user CRUD — all three
  hard requirements
- Keycloak eliminated: requires a separate Docker container (contradicts
  "embedded" requirement)
- Duende IdentityServer eliminated: commercial RPL license (contradicts
  "free/OSS" requirement)
- OpenIddict: not needed for Blazor Server cookie auth. Can be added later as a
  non-breaking enhancement if OIDC endpoints are ever needed for external
  clients
- Entra ID eliminated: cloud-only SaaS, not self-hostable

---

## 5. Proposed Solution: ASP.NET Core Identity

### 5.1 Architecture (After)

```
┌──────────────────────────────────────┐
│ Ozds.Server                          │
│                                      │
│   ASP.NET Core Identity              │
│   ├─ UserManager<OzdsUser>   (CRUD)  │
│   ├─ SignInManager<OzdsUser> (Auth)  │
│   ├─ RoleManager<OzdsRole>  (Roles) │
│   └─ Cookie Authentication           │
│                                      │
│   External OAuth (optional)          │
│   ├─ Google                          │
│   ├─ Facebook                        │
│   └─ Microsoft Account               │
│                                      │
│   EF Core ──► PostgreSQL             │
│   (Identity tables + business data)  │
└──────────────────────────────────────┘
```

Two Docker containers eliminated. All identity data in PostgreSQL. Pure C#/.NET
stack.

### 5.2 Operation Mapping

| Current (LDAP)                        | New (ASP.NET Core Identity)                           |
| ------------------------------------- | ----------------------------------------------------- |
| `LdapConnection.Search()` find user   | `UserManager<T>.FindByIdAsync()`                      |
| `LdapConnection.Add()` create user    | `UserManager<T>.CreateAsync()`                        |
| `LdapConnection.Modify()` update user | `UserManager<T>.UpdateAsync()`                        |
| `LdapConnection.Delete()` delete user | `UserManager<T>.DeleteAsync()`                        |
| LDAP Extended Op, change password     | `UserManager<T>.ChangePasswordAsync()`                |
| Authelia OIDC, cookie auth            | `SignInManager<T>.PasswordSignInAsync()` + Cookie     |
| Claims from OIDC token                | `UserManager<T>.GetClaimsAsync()` + `ClaimsPrincipal` |
| LDAP search with pagination           | EF Core LINQ queries on `AspNetUsers`                 |

### 5.3 Custom User Entity

```csharp
using Microsoft.AspNetCore.Identity;

public class OzdsUser : IdentityUser
{
  // IdentityUser.Id is string by default.
  // Set to existing LDAP uid during migration to preserve
  // RepresentativeEntity FK references.

  public string DisplayName { get; set; } = string.Empty;
}
```

Identity tables are created in the existing PostgreSQL database via a new EF
Core migration. They can optionally use a separate schema (e.g. `identity.`) to
avoid naming conflicts.

### 5.4 External OAuth Providers

Built-in NuGet packages, zero custom code:

```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.Google" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.Facebook" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.MicrosoftAccount" />
```

Each provider is opt-in via configuration. No code changes needed to add or
remove providers after initial setup.

### 5.5 OpenIddict (Future Option)

Not required for the Blazor Server UI. Add only if OZDS must later act as an
OIDC provider for external applications (mobile app, third-party integrations).
OpenIddict is Apache 2.0 licensed and integrates natively with ASP.NET Core
Identity. Adding it is a non-breaking, additive change.

---

## 6. Migration Plan

### 6.1 Data Migration (50-500 Users)

**Source:** lldap (LDAP entries with `uid`, `cn`, `mail`) **Target:** ASP.NET
Core Identity tables in PostgreSQL (`AspNetUsers`)

| Step | Action                                                                                                          |
| ---- | --------------------------------------------------------------------------------------------------------------- |
| 1    | Export all lldap users to JSON via LDAP search script                                                           |
| 2    | Create `OzdsUser` for each entry: `Id` = LDAP `uid`, `UserName` = `uid`, `Email` = `mail`, `DisplayName` = `cn` |
| 3    | Set temporary passwords and flag for mandatory reset on first login                                             |
| 4    | Verify all `RepresentativeEntity.Id` values resolve to migrated `OzdsUser.Id`                                   |

**Critical constraint:** By setting `IdentityUser.Id` to the existing LDAP
`uid`, all `RepresentativeEntity` foreign key references are preserved without
any changes to business data tables.

**Password limitation:** LDAP password hashes cannot be exported from lldap. All
migrated users must reset their passwords on first login after migration.

### 6.2 Code Migration Scope

| Module                                      | Action                                                                                  | Effort  |
| ------------------------------------------- | --------------------------------------------------------------------------------------- | ------- |
| `Ozds.Users/Extensions/HostExtensions.cs`   | Rewrite: replace LDAP + OIDC with Identity DI setup                                     | Medium  |
| `Ozds.Users/Queries/UserQueries.cs`         | Rewrite: `LdapConnection` to `UserManager<T>`                                           | Medium  |
| `Ozds.Users/Mutations/UserMutations.cs`     | Rewrite: LDAP add/modify/delete to `UserManager<T>`                                     | Medium  |
| `Ozds.Users/Mutations/PasswordMutations.cs` | Rewrite: LDAP extended op to `ChangePasswordAsync`                                      | Low     |
| `Ozds.Users/Options/OzdsUsersOptions.cs`    | Simplify: remove LDAP/OIDC connection string models                                     | Low     |
| `Ozds.Users/Entities/`                      | Remove or adapt (Identity provides its own entities)                                    | Low     |
| `Ozds.Users.csproj`                         | Remove `Novell.Directory.Ldap`, add `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | Trivial |
| Blazor login pages                          | Replace OIDC redirect with Identity login form                                          | Medium  |
| EF Migration                                | Add Identity tables to PostgreSQL                                                       | Low     |
| `docker-compose.yml`                        | Remove `auth` + `ldap` services                                                         | Trivial |
| `scripts/auth/`, `scripts/ldap/`            | Delete Authelia and lldap config/data                                                   | Trivial |
| `appsettings.*.json`                        | Remove `Ozds:Users:Ldap` + `Ozds:Users:Oidc`                                            | Trivial |

### 6.3 Modules Not Affected

| Module                         | Reason                                                       |
| ------------------------------ | ------------------------------------------------------------ |
| `Ozds.Business/Authorization/` | HMAC-based IoT auth — completely independent                 |
| `Ozds.Business/` (all)         | Consumes `ClaimsPrincipal` — source of claims is transparent |
| `Ozds.Data/Entities/`          | `RepresentativeEntity.Id` unchanged (same string IDs)        |
| `Ozds.Client/` (components)    | `OzdsComponentBase` reads `ClaimsPrincipal` — still works    |
| `Ozds.Iot/`                    | No identity dependency                                       |

---

## 7. Rollback Strategy

- Keep Authelia/lldap Docker definitions commented out (not deleted) until
  migration is validated in production
- Identity migration is additive — new tables only, no existing tables modified
- `RepresentativeEntity` references unchanged (same string IDs)
- If rollback needed: re-enable Docker services, revert `Ozds.Users` code, drop
  Identity tables

---

## 8. Risks

| Risk                                                 | Likelihood  | Impact | Mitigation                                                           |
| ---------------------------------------------------- | ----------- | ------ | -------------------------------------------------------------------- |
| ID mismatch during migration breaks business data    | Medium      | High   | Use LDAP `uid` as `IdentityUser.Id` — zero FK changes needed         |
| Users lose passwords (LDAP hashes not exportable)    | **Certain** | Medium | Force password reset for all migrated users; communicate in advance  |
| Missing OIDC capability if future apps need it       | Low         | Low    | Add OpenIddict later — non-breaking, additive change                 |
| Identity table naming conflicts with existing schema | Low         | Low    | Use separate schema (`identity.`) or prefixed table names            |
| Blazor login UX regression (form vs redirect)        | Medium      | Low    | Build login page matching current UX; test with users before cutover |

---

## 9. Effort Estimate

| Phase                                     | Estimate      |
| ----------------------------------------- | ------------- |
| Identity + EF Core setup, DI registration | 1-2 days      |
| Rewrite `Ozds.Users` (queries, mutations) | 2-3 days      |
| User migration script                     | 1 day         |
| Blazor login flow update                  | 1-2 days      |
| External OAuth provider setup             | 0.5 day       |
| Docker and config cleanup                 | 0.5 day       |
| Testing and validation                    | 2-3 days      |
| **Total**                                 | **8-12 days** |

---

## 10. Success Criteria

- [ ] All existing users accessible via `UserManager<OzdsUser>` with preserved
      IDs
- [ ] Login/logout works via ASP.NET Core Identity cookie authentication
- [ ] User CRUD (create, read, update, delete) functional through `Ozds.Users`
- [ ] Password change/reset functional
- [ ] At least one external OAuth provider (Google) configurable and working
- [ ] `RepresentativeEntity` and all dependent business logic unchanged
- [ ] Authelia and lldap Docker containers removed from `docker-compose.yml`
- [ ] `Novell.Directory.Ldap` package removed from solution
- [ ] All existing tests pass (`just test-ci`)
- [ ] No new `lsp_diagnostics` errors in modified files

---

## 11. Resolved Decisions

| #   | Question                               | Decision                         | Rationale                                                                                                                                                                           |
| --- | -------------------------------------- | -------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| 1   | DbContext strategy for Identity tables | **Separate `IdentityDbContext`** | Follows existing multi-context pattern (`DataDbContext`, `MessagingDbContext`, `JobsDbContext`). Clean separation of identity schema from business entities. Own migration history. |
| 2   | Migration script tooling               | **One-time console app**         | Throwaway script kept in `scripts/` for reference. No ongoing maintenance burden. Not worth integrating into `Ozds.Migration` for a single-use operation.                           |
| 3   | Password reset communication           | **Manual communication**         | Admin sends announcement via existing channels (email blast, internal comms) before cutover. No automated email flow needed.                                                        |
| 4   | Additional LDAP attributes to migrate  | **Id, Name, Email only**         | Current `UserEntity` already captures everything needed. No additional LDAP attributes in use.                                                                                      |
| 5   | "Remember me" / persistent cookies     | **Yes**                          | Users can opt in to stay logged in across browser sessions. `SignInManager.PasswordSignInAsync(isPersistent: true)` with configurable cookie expiration.                            |

### Implications of Decisions

**Separate IdentityDbContext:**

- New project or folder for the context (e.g. within `Ozds.Users` or a new
  `Ozds.Identity` project)
- EF Core migrations managed independently:
  ```
  dotnet ef migrations add <Name> \
    --startup-project src/Ozds.Server/Ozds.Server.csproj \
    --project src/Ozds.Users/Ozds.Users.csproj \
    --context Ozds.Users.Context.IdentityDbContext
  ```
- Registration follows existing pattern in `HostExtensions.cs`:
  ```csharp
  builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseNpgsql(connectionString));
  ```

**One-time migration script:**

- Located at `scripts/migrate-ldap-users/` as a standalone .NET console app
- Reads lldap via LDAP, writes to `IdentityDbContext` via `UserManager<T>`
- Sets `OzdsUser.Id` = LDAP `uid` to preserve `RepresentativeEntity` references
- Assigns temporary passwords, does NOT send emails
- Run once during cutover, kept in repo as historical reference

**Persistent cookies:**

- Login page includes "Remember me" checkbox
- Persistent cookie expiration configurable via `appsettings.json`
- Default: 30 days (standard for web applications)
- Non-persistent sessions expire on browser close (default Identity behavior)
