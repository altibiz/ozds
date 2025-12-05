#!/usr/bin/env bash

root="$(dirname "${BASH_SOURCE[0]}")"
if [ -d "$root/playwright" ]; then
  mv "$root/playwright" "$root/.playwright"
fi

export PLAYWRIGHT_BROWSERS_PATH="$root/.playwright/package/.local-browsers"
export PLAYWRIGHT_NODEJS_PATH="$root/.playwright/node/linux-x64/node"

dll="$root/Ozds.Server.dll"
cd "$root" || exit
dotnet "$dll"
