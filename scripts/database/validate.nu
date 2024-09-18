#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname --num-levels 2
let src = [$root, "src"] | path join
let dumps = [$root, "scripts", "migrations"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first
let dump_types = [ "", "-orchard", "-hypertables" ]

let migration_parser =  ".*Migrations/(?P<timestamp>[0-9]+)_(?P<name>[^\\.]+).cs"

def main [project_name: string, name: string] {
  let migration_glob = $"($src)/($project_name)/Migrations/*.cs"
  let parsed_migrations =  glob $migration_glob
    | parse --regex $migration_parser

  let migrations = $parsed_migrations
    | filter { |x| $x.name == $name }
  if ($migrations | is-empty) {
    print $"Migration '($name)' not found."
    exit 1
  }
  let migration = $migrations | first
  let timestamp = $migration.timestamp
  let dump_name = $"($timestamp)-($project_name)-($name)"
  let rewind_dump_name = $"($dump_name)-rewind"

  let timestamp_plus_1 = (($timestamp | into int) + 1) | into string
  mut migrations_plus_1 = $parsed_migrations
    | sort-by timestamp
    | filter { |x| $x.timestamp > $timestamp_plus_1 }
  let migration_plus_1: string = (if ($migrations_plus_1 | is-empty) {
   $migration } else {
     $migrations_plus_1 | first
  })
  let name_plus_1 = $migration_plus_1.name

  if ($name_plus_1 != $name) {
    just --yes rewind $project_name $name_plus_1
  }

  just --yes rollback $project_name $name
  just --yes dump $dump_name
  just --yes rewind $project_name $name
  just --yes dump $rewind_dump_name

  mut differences = false
  for $dump_type in $dump_types {
    let original_dump = $"($dumps)/($dump_name)($dump_type).sql"
    let rewind_dump = $"($dumps)/($rewind_dump_name)($dump_type).sql"
    if (not ($original_dump | path exists)) {
      print $"Dump file '($original_dump)' does not exist."
      exit 1
    }
    if (not ($rewind_dump | path exists)) {
      print $"Rewind dump file '($rewind_dump)' does not exist."
      exit 1
    }
    try {
      delta $original_dump $rewind_dump
    }
    print $"Do you consider the migration valid for '($dump_type)' dump?"
    let user_input = ['y' 'n'] | input list
    if $user_input == 'y' {
      continue
    } else {
      $differences = true
      break  # Exit loop if any dump_type is invalid
    }
  }
  if $differences {
    exit 1
  } else {
    for $dump_type in $dump_types {
      let rewind_dump = $"($dumps)/($rewind_dump_name)($dump_type).sql"
      let original_dump = $"($dumps)/($dump_name)($dump_type).sql"
      mv -f $rewind_dump $original_dump
    }
  }
}
