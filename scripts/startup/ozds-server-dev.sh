#!/usr/bin/env bash

export ASPNETCORE_ENVIRONMENT="Development"
export ASPNETCORE_URLS="http://localhost:5000"
export PLAYWRIGHT_BROWSERS_PATH=0
root="$(dirname "${BASH_SOURCE[0]}")"
dll="$root/Ozds.Server.dll"
cd "$root" || exit
dotnet "$dll"
