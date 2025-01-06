#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname --num-levels 2
let src = [$root "src"] | path join
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
    | each { |x| (($x | into int) + 1) | into string }
    | first;
  let project_migrations =  $projects
    | each { |x|
      let migrations = glob $"($x)/Migrations/*"
        | path basename | sort
        | filter { |x| $x < $timestamp and $x =~ '\d{14}_[^\.]*\.cs' }
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
