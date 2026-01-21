#!/usr/bin/env nu

def main [] {
  loop {
    try {
      let ollama_container_id = (docker compose ps --format json
        | lines
        | each { $in | from json }
        | where { $in.Image | str starts-with "ollama" }
        | first
        | get id)
      (docker exec $ollama_container_id
        ollama pull deepseek-r1:14b-qwen-distill-q4_K_M)
      (docker exec $ollama_container_id
        ollama cp deepseek-r1:14b-qwen-distill-q4_K_M deepseek-chat)
      break
    } catch {
      sleep 1sec
      continue
    }
  }
}
