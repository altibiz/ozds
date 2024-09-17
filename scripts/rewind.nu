#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname
let src = [$root, "src"] | path join
let dumps = [$root, "scripts", "migration"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = [$root, "Ozds.Server", "Ozds.Server.csproj"] | path join

def main [name: string] {
  let migration = glob $"($src)/**/Migrations/*_($name).cs" | first
  if ($migration | is-empty) {
    print $"Migration '($migration)' not found."
    exit 1
  }

  let timestamp = $migration | path basename | split row '_' | first
  let project = $migration | path dirname --num-levels 2
  let project_name = $project | path basename

  let project_migrations =  $projects | each { ||
    let migration = glob $"($in)/Migrations/*"
      | path basename | sort
      | filter {  $in >= $timestamp and $in =~ '\d{14}_.*\.cs' } | last
      | split row "_" | last
      | split row "." | first
    let csproj = [$in, $"($in | path basename).csproj"] | path join
    { project: $in, migration: $migration, csproj: $csproj }
  }

  let orchard_dump = $"($dumps)/($timestamp)-($project_name)-($name)-orchard.sql"
  let normal_dump = $"($dumps)/($timestamp)-($project_name)-($name).sql"
  let hypertables_dump = $"($dumps)/($timestamp)-($project_name)-($name)-hypertables.sql"

  if not ($orchard_dump | path exists) {
    print $"Orchard dump not found for migration '($name)'."
    exit 1
  }
  if not ($normal_dump | path exists) {
    print $"Normal dump not found for migration '($name)'."
    exit 1
  }
  if not ($hypertables_dump | path exists) {
    print $"Hypertables dump not found for migration '($name)'."
    exit 1
  }

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
