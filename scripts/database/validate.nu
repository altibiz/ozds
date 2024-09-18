#!/usr/bin/env nu

# Define variables
let root = $env.FILE_PWD | path dirname --num-levels 2
let src = [$root, "src"] | path join
let dumps = [$root, "scripts", "migrations"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first
let dump_types = [ "", "-orchard", "-hypertables" ]

def main [project_name: string, name: string] {
  let migration = glob $"($src)/($project_name)/Migrations/*_($name).cs" | first
  if ($migration | is-empty) {
    print $"Migration '($name)' not found."
    exit 1
  }
  let timestamp = $migration
    | path basename
    | split row '_'
    | get 0
  let project = $migration | path dirname --num-levels 2
  let dump_name = $"($timestamp)-($project_name)-($name)"
  let rewind_dump_name = $"($dump_name)-rewind"

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
    delta $original_dump $rewind_dump
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
      rm $rewind_dump
    }
  }
}
