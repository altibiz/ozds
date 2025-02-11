#!/usr/bin/env bash

export ASPNETCORE_ENVIRONMENT="Development"
export ASPNETCORE_URLS="http://localhost:5000"
export PLAYWRIGHT_BROWSERS_PATH=0
root="$(dirname "${BASH_SOURCE[0]}")"
cp -r "$root/App_Data_Dev" "$root/App_Data"
dll="$root/Ozds.Server.dll"
cd "$root" || exit
dotnet "$dll"
rm -rf "$root/App_Data"
