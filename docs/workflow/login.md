# Login

Login is a simple workflow that starts a user session and directs them to the
correct page based on their role. There are two types of users the login
workflow differentiates between: admin and regular users. Admin users (mostly
developers) are redirected to the admin page, while regular users are redirected
to the app page.

```plantuml
start

:User enters credentials;

if (Credentials valid?) then (yes)
  :Start user session;
  if (User is admin?) then (yes)
    :Redirect to /admin page;
  else (no)
    :Redirect to /app page;
  endif
else (no)
  :Show error message;
endif

stop
```
