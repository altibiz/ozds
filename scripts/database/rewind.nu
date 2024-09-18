#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname --num-levels 2
let src = [$root, "src"] | path join
let dumps = [$root, "scripts", "migrations"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first

def main [project_name: string, name: string] {
  let migration = glob $"($src)/($project_name)/Migrations/*_($name).cs" | first
  if ($migration | is-empty) {
    print $"Migration '($migration)' not found."
    exit 1
  }
  let timestamp = $migration
    | path basename
    | split row '_'
    | first;
  let timestamp_plus_1 = (($timestamp | into int) + 1) | into string
  let project = $migration | path dirname --num-levels 2
  let project_migrations =  $projects
    | each { |x|
      let migrations = glob $"($x)/Migrations/*"
        | path basename | sort
        | filter { |x| $x < $timestamp_plus_1 and $x =~ '\d{14}_[^\.]*\.cs' }
      if ($migrations | is-empty) {
        return null
      }

      let migration = $migrations | last
        | split row "_" | last
        | split row "." | first

      let csproj = [$x $"($x | path basename).csproj"] | path join
      { project: $x migration: $migration csproj: $csproj }
    }
    | filter { |x| $x | is-not-empty }
  let orchard_dump = $"($dumps)/($timestamp)-($project_name)-($name)-orchard.sql"
  let normal_dump = $"($dumps)/($timestamp)-($project_name)-($name).sql"
  let hypertables_dump = $"($dumps)/($timestamp)-($project_name)-($name)-hypertables.sql"
  if not ($orchard_dump | path exists) {
    print $"Orchard dump '($orchard_dump)' not found for migration '($name)'."
    exit 1
  }
  if not ($normal_dump | path exists) {
    print $"Normal dump '($normal_dump)' not found for migration '($name)'."
    exit 1
  }
  if not ($hypertables_dump | path exists) {
    print $"Hypertables dump '($hypertables_dump)' not found for migration '($name)'."
    exit 1
  }

  just --yes clean
  open --raw $orchard_dump
    | (docker exec
        --env PGHOST="localhost"
        --env PGPORT="5432"
        --env PGDATABASE="ozds"
        --env PGUSER="ozds"
        --env PGPASSWORD="ozds"
        --interactive
        ozds-postgres-1
          psql)
  for $project_migration in $project_migrations {
    let project = $project_migration.project
    let migration = $project_migration.migration
    let csproj = $project_migration.csproj

    (dotnet ef
      --startup-project $server_csproj
      --project $csproj
      database update
      $migration)
  }
  open --raw $normal_dump
    | (docker exec
        --env PGHOST="localhost"
        --env PGPORT="5432"
        --env PGDATABASE="ozds"
        --env PGUSER="ozds"
        --env PGPASSWORD="ozds"
        --interactive
        ozds-postgres-1
          psql)
  open --raw $hypertables_dump
    | (docker exec
        --env PGHOST="localhost"
        --env PGPORT="5432"
        --env PGDATABASE="ozds"
        --env PGUSER="ozds"
        --env PGPASSWORD="ozds"
        --interactive
        ozds-postgres-1
          psql)
}
