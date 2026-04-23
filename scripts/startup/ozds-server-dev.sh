#!/usr/bin/env bash

root="$(dirname "${BASH_SOURCE[0]}")"
if [ -d "$root/playwright" ]; then
  mv "$root/playwright" "$root/.playwright"
fi

if [ -n "$DIRENV_DIR" ]; then
  # NOTE: chromium is already bundled and this would lead to a double bundle to which linux says hard nope
  export DIRENV_PLAYWRIGHT_NODEJS_PATH="$PLAYWRIGHT_NODEJS_PATH"
  exec direnv exec / "$0" "$@"
else
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

if ! compgen -G "$PLAYWRIGHT_BROWSERS_PATH/chromium_headless_shell-*" > /dev/null; then
  echo "Installing Playwright chromium-headless-shell into $PLAYWRIGHT_BROWSERS_PATH..."
  mkdir -p "$PLAYWRIGHT_BROWSERS_PATH"
  "$PLAYWRIGHT_NODEJS_PATH" "$root/.playwright/package/cli.js" install --with-deps chromium-headless-shell
fi

printf "Playwright node version: %s\n" "$("$PLAYWRIGHT_NODEJS_PATH" -v)"
chrome="$(compgen -G "$PLAYWRIGHT_BROWSERS_PATH/chromium_headless_shell-*/chrome-linux/headless_shell" | head -n1)"
if [ -n "$chrome" ]; then
  printf "Playwright chromium version: %s\n" "$("$chrome" --version)"
fi

dll="$root/Ozds.Server.dll"
cd "$root" || exit
dotnet "$dll"
