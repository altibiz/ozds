#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname
let src = [$root, "src"] | path join
let projects = glob $"($src)/**/Migrations" | path dirname
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first

def main [name: string] {
  let migration = glob $"($src)/**/Migrations/*_($name).cs" | first
  if ($migration | is-empty) {
    print $"Migration '($migration)' not found."
    exit 1
  }

  let timestamp = $migration | path basename | split row '_' | first

  let project_migrations =  $projects | each { ||
    let migration = glob $"($in)/Migrations/*"
      | path basename | sort
      | filter {  $in >= $timestamp and $in =~ '\d{14}_.*\.cs' } | last
      | split row "_" | last
      | split row "." | first
    let csproj = [$in, $"($in | path basename).csproj"] | path join
    { project: $in, migration: $migration, csproj: $csproj }
  }

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
}
