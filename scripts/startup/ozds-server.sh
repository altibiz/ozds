#!/usr/bin/env bash

root="$(dirname "${BASH_SOURCE[0]}")"

export PLAYWRIGHT_BROWSERS_PATH="$root/.playwright/package/.local-browsers"

dll="$root/Ozds.Server.dll"
cd "$root" || exit
dotnet "$dll"
