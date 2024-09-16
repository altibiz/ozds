#!/usr/bin/env nu

loop {
  try {
    let timescale_container_id = (docker compose ps --format json
      | from json
      | where Image | str starts-with "timescale"
      | first
      | get id)
    docker exec $timescale_container_id pg_isready --host localhost
    break
  } catch {
    sleep 1sec
    continue
  }
}
