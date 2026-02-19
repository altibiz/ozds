#!/usr/bin/env nu

let container = docker compose ps --format json
  | lines
  | each { $in | from json }
  | where { $in.Image | str starts-with "timescale" }
  | first

def "main" [] {
  main name
}

def "main name" [] {
  $container.Name
}

def "main id" [] {
  $container.ID
}
