#!/usr/bin/env bash

root="$(dirname "${BASH_SOURCE[0]}")"
if [ -d "$root/playwright" ]; then
  mv "$root/playwright" "$root/.playwright"
fi

export PLAYWRIGHT_BROWSERS_PATH="$root/.playwright/package/.local-browsers"
export PLAYWRIGHT_NODEJS_PATH="$root/.playwright/node/linux-x64/node"

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
