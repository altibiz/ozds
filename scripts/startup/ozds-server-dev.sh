#!/usr/bin/env bash

root="$(dirname "${BASH_SOURCE[0]}")"
if [ -d "$root/playwright" ]; then
  mv "$root/playwright" "$root/.playwright"
fi

if [ -n "$DIRENV_DIR" ]; then
  # NOTE: chromium is already bundled and
  # this would lead to a double bundle to which linux says hard nope
  printf "Exiting direvn at: %s\n" "${DIRENV_DIR:1}"
  DIRENV_DOTNET_PATH="$(which dotnet)"
  export DIRENV_DOTNET_PATH
  export DIRENV_PLAYWRIGHT_NODEJS_PATH="$PLAYWRIGHT_NODEJS_PATH"
  exec direnv exec / "$0" "$@"
  exit 0
else
  if [ -n "$DIRENV_DOTNET_PATH" ]; then
    export DOTNET_PATH="$DIRENV_DOTNET_PATH"
  else
    DOTNET_PATH="$(which dotnet)"
    export DOTNET_PATH
  fi
  if [ -n "$DIRENV_PLAYWRIGHT_NODEJS_PATH" ]; then
    export PLAYWRIGHT_NODEJS_PATH="$DIRENV_PLAYWRIGHT_NODEJS_PATH"
  else
    export PLAYWRIGHT_NODEJS_PATH="$root/.playwright/node/linux-x64/node"
  fi
fi

export ASPNETCORE_ENVIRONMENT="Production"
export ASPNETCORE_URLS="http://localhost:5000"
cp -f "$root/appsettings.Development.json" "$root/appsettings.Production.json"

export DEBUG=pw:api,pw:server
export PLAYWRIGHT_BROWSERS_PATH="$root/.playwright/package/.local-browsers"
printf "Dotnet version: %s\n" "$(bash -c "$DOTNET_PATH --version")"
printf "Playwright node version: %s\n" "$(bash -c "$PLAYWRIGHT_NODEJS_PATH -v")"
printf "Playwright chromium version: %s\n" "$(bash -c "$PLAYWRIGHT_BROWSERS_PATH/chromium_headless_shell-1155/chrome-linux/headless_shell --version")"

dll="$root/Ozds.Server.dll"
cd "$root" || exit
exec "$DOTNET_PATH" "$dll"
