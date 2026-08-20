# Identity Migration — Remaining Steps

This document tracks follow-up work after the initial code migration in
`feature/identity-migration`.

## Completed (this PR)

- [x] Replace `Novell.Directory.Ldap` + `OpenIdConnect` packages with
      `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- [x] Create `OzdsUser` entity extending `IdentityUser` with `DisplayName`
- [x] Create `UsersDbContext` extending `IdentityDbContext<OzdsUser>`
- [x] Create `UsersDbContextDesignTimeFactory` (Npgsql, snake_case, custom
      migration history table)
- [x] Rewrite `HostExtensions.cs` — Identity DI, cookie auth, optional OAuth
- [x] Rewrite `UserQueries.cs` — `UserManager<OzdsUser>` queries
- [x] Rewrite `UserMutations.cs` — `UserManager<OzdsUser>` CRUD
- [x] Rewrite `PasswordMutations.cs` — `UserManager.ChangePasswordAsync()`
- [x] Rewrite `AppAuthController.cs` — `SignInManager` login/logout
- [x] Simplify `OzdsUsersOptions.cs` — single connection string
- [x] Update `appsettings.Development.json` — remove LDAP/OIDC config
- [x] Comment out `auth` + `ldap` services in `docker-compose.yml`
- [x] Remove dead test infrastructure (containers, templates, crypto)
- [x] Full solution build — 0 errors, 0 warnings

## Remaining Steps

### 1. EF Core Migration (Required before first run)

Generate the initial Identity schema migration:

```bash
dotnet ef migrations add InitialIdentity \
  --startup-project src/Ozds.Server/Ozds.Server.csproj \
  --project src/Ozds.Users/Ozds.Users.csproj \
  --context Ozds.Users.Context.UsersDbContext
```

This creates Identity tables (`asp_net_users`, `asp_net_roles`, etc.) in the
existing PostgreSQL database with snake_case naming.

### 2. Login Razor View (Required)

`AppAuthController.Login()` returns `View()` but no view exists yet. Create:

```
src/Ozds.Server/Views/AppAuth/Login.cshtml
```

The view should include:
- Username and password fields
- "Remember me" checkbox
- Error message display (from `ViewData["Error"]`)
- Return URL as hidden field
- POST to `/app/auth/login`

### 3. LDAP User Migration Script (Required for cutover)

Create a one-time console app at `scripts/migrate-ldap-users/`:

- Connect to lldap via `Novell.Directory.Ldap` (one-time dependency)
- Read all users (uid, cn, mail)
- For each user, create `OzdsUser` via `UserManager<T>`:
  - `Id` = LDAP `uid` (preserves `RepresentativeEntity` FK references)
  - `UserName` = `uid`
  - `Email` = `mail`
  - `DisplayName` = `cn`
- Assign temporary passwords (e.g., `TempPass!{uid}123`)
- Log results — verify all `RepresentativeEntity.Id` values have matching users

### 4. Password Reset Flow (Required for cutover)

After migration, all users need new passwords. Options:
- **Manual**: Admin communicates temporary passwords via email/internal comms
- **Self-service**: Add a "forgot password" flow with email token
  (`UserManager.GeneratePasswordResetTokenAsync`)

### 5. Delete Authelia/lldap Config Files (After validation)

Once migration is validated in production:

```bash
rm -rf scripts/auth/
rm -rf scripts/ldap/
```

And uncomment → delete the Docker services in `docker-compose.yml`.

### 6. External OAuth Provider Testing (Optional)

Configure and test at least one provider:

```json
{
  "Ozds": {
    "Users": {
      "Authentication": {
        "Google": {
          "ClientId": "your-client-id",
          "ClientSecret": "your-client-secret"
        }
      }
    }
  }
}
```

### 7. MigrateOnStartup Service (Optional)

Add a `MigrationService` (like `Ozds.Data` has) to auto-apply Identity
migrations on startup when `OzdsUsersOptions.MigrateOnStartup` is `true`.

### 8. End-to-End Testing

- [ ] Login with username/password
- [ ] Logout
- [ ] "Remember me" persistent session
- [ ] User CRUD via existing admin UI
- [ ] Password change
- [ ] Verify `RepresentativeEntity` lookups still work
- [ ] Verify all Blazor pages render with auth state
- [ ] Verify API key auth (HMAC) is unaffected
- [ ] Run `just test-ci` — all existing tests pass
