#!/usr/bin/env nu

# Define variables
let root = $env.FILE_PWD | path dirname
let src = [$root, "src"] | path join
let dumps = [$root, "scripts", "migrations"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first
let dump_types = [ "", "-orchard", "-hypertables" ]

def main [name: string] {
  let migration_file = glob $"($src)/**/Migrations/*_($name).cs" | first
  if ($migration_file | is-empty) {
    print $"Migration '($name)' not found."
    exit 1
  }
  let timestamp = $migration_file
    | path basename
    | split row '_'
    | get 0
  let project = $migration_file | path dirname --num-levels 2
  let project_name = $project | path basename
  let dump_name = $"($timestamp)-($project_name)-($name)"
  let rewind_dump_name = $"($dump_name)-rewind"

  just clean
  just rollback $name
  just dump $dump_name
  just rewind $name
  just dump $rewind_dump_name

  let differences = false
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

    let diff_output = delta $original_dump $rewind_dump
    if ($diff_output | str length | into int > 0) {
      print $"Differences found in '($dump_type)' dump:"
      print $diff_output
      let differences = true
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
