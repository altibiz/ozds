# Moderation

Moderation, in the context of OZDS, is the act of user management including user
creation and modification. For now, an admin is required to create a user and
then the user data can be modified by other users. This is because we are using
OrchardCore for user management and the easiest route, for now, was to use
OrchardCore UI to create users and then join our representative table with
OrchardCore user tables in the database. In the future, we strive to make this
process streamlined via registration and authorization of specific users making
other users.

```plantuml
start

:Admin creates user via /admin page;

:Representative for user is created via /app/users page;

:Representative for user is modified and roles are assigned via /app/users page;

stop
```
