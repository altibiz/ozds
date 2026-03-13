#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname --num-levels 2
let src = [$root "src"] | path join
let server_csproj = glob $"(glob $"($src)/**/Program.cs" | first | path dirname)/*.csproj" | first

def main [project_name: string, project_context: string, migration_name: string] {

  print "Adding dummy migration file..."

  (^dotnet ef migrations add $migration_name
    --startup-project $server_csproj
    --project $"($src)/($project_name)"
    --context $project_context | str join "")

  let createdMigrationFile = (ls $"($src)/($project_name)/Migrations"
  | get name
  | path basename
  | where { |x| $x =~ '\d{14}_[^\.]*\.cs'}
  | sort
  | last
  )

  print $"Created migration file: ($createdMigrationFile)"

  print "Using created file to generate custom insert migration..."

  (just migration generate
  -o $"../../src/($project_name)/Migrations/($createdMigrationFile)"
  -n $migration_name)

  print "Migration generated successfully, continuing migration process..."

  (just migrate-continue
   $project_name
   $project_context
   $migration_name
  )

  print "Process finished."
}

