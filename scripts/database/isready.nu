#!/usr/bin/env nu

def main [] {
  loop {
    try {
      let timescale_container_id = (docker compose ps --format json
        | lines
        | each { $in | from json }
        | filter { $in.Image | str starts-with "timescale" }
        | first
        | get id)
      docker exec $timescale_container_id pg_isready --host localhost
      break
    } catch {
      sleep 1sec
      continue
    }
  }
}
