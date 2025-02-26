#!/usr/bin/env nu

def "main" [] {
  let root = $env.FILE_PWD | path dirname | path dirname
  let artifacts = [ $root "artifacts" ] | path join
  let expr = $"\(builtins.getFlake \"($root)\"\).lib.rumor.\"raspberryPi4-aarch64-linux\""
  let spec = nix eval --json --impure --expr $expr
  cd $artifacts
  $spec | rumor stdin json --stay
}
