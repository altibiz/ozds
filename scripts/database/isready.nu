#!/usr/bin/env nu

def main [] {
  loop {
    try {
      let timescale_container_id = nu ([ $env.FILE_PWD "postgrescontainer.nu" ] | path join) id
      docker exec $timescale_container_id pg_isready --host localhost
      break
    } catch {
      sleep 1sec
      continue
    }
  }
}
