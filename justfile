set windows-shell := ["nu.exe", "-c"]
set shell := ["nu", "-c"]

root := absolute_path('')
sln := absolute_path('ozds.sln')
gitignore := absolute_path('.gitignore')
prettierignore := absolute_path('.prettierignore')
jbcache := absolute_path('.jb/cache')
jbcleanuplog := absolute_path('.jb/cleanup.log')
jbinspectlog := absolute_path('.jb/inspect.log')
artifacts := absolute_path('artifacts')
servercsproj := absolute_path('src/Ozds.Server/Ozds.Server.csproj')
datacsproj := absolute_path('src/Ozds.Data/Ozds.Data.csproj')
fakecsproj := absolute_path('scripts/Ozds.Fake/Ozds.Fake.csproj')
fakeassets := absolute_path('scripts/Ozds.Fake/Assets')
migrationassets := absolute_path('src/Ozds.Data/Assets')
docs := absolute_path('docs')
doxyfile := absolute_path('docs/Doxyfile')
schema := absolute_path('docs/schema.md')

default: prepare

prepare:
  dvc install
  dvc pull
  dotnet tool restore
  prettier --version or npm install -g prettier

  docker compose up -d
  sleep 10sec

  open --raw '{{migrationassets}}/current-orchard.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update

  open --raw '{{migrationassets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

lfs:
  dvc add {{fakeassets}}/*.csv
  dvc add {{migrationassets}}/*.sql

dev *args:
  dotnet watch --project '{{servercsproj}}' {{args}}

fake *args:
  dotnet run --project '{{fakecsproj}}' -- {{args}}

format:
  nixpkgs-fmt {{root}}

  prettier --write \
    --ignore-path '{{gitignore}}' \
    --ignore-path '{{prettierignore}}' \
    --cache --cache-strategy metadata \
    '{{root}}'

  dotnet build '{{sln}}'

  dotnet format '{{sln}}' \
    --no-restore \
    --verbosity minimal \
    --severity info \
    --exclude '**/.git/**/*' \
    --exclude '**/.nuget/**/*' \
    --exclude '**/obj/**/*' \
    --exclude '**/bin/**/*'

  dotnet roslynator fix '{{sln}}' \
    --format \
    --verbosity minimal \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet jb cleanupcode '{{sln}}' \
    --no-build \
    --verbosity=WARN \
    --caches-home='{{jbcache}}' \
    -o='{{jbinspectlog}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

lint:
  prettier --check \
    --ignore-path '{{gitignore}}' \
    --ignore-path '{{prettierignore}}' \
    --cache --cache-strategy metadata \
    '{{root}}'

  # TODO: use hunspell with dictionaries
  cspell lint . \
    --no-progress

  dotnet build '{{sln}}'

  dotnet roslynator analyze '{{sln}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet jb inspectcode '{{sln}}' \
    --no-build \
    --verbosity=WARN \
    --caches-home='{{jbcache}}' \
    -o='{{jbinspectlog}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

test *args:
  dotnet test '{{sln}}' {{args}}

dump:
  docker exec \
    --env PGHOST="localhost" \
    --env PGPORT="5432" \
    --env PGDATABASE="ozds" \
    --env PGUSER="ozds" \
    --env PGPASSWORD="ozds" \
    --interactive \
    ozds-postgres-1 \
      pg_dump \
        --schema=public \
        --table='"Document"' \
        --table='"Identifiers"' \
        --table='"User"*' \
    out> '{{migrationassets}}/current-orchard.sql'

  docker exec \
    --env PGHOST="localhost" \
    --env PGPORT="5432" \
    --env PGDATABASE="ozds" \
    --env PGUSER="ozds" \
    --env PGPASSWORD="ozds" \
    --interactive \
    ozds-postgres-1 \
      pg_dump \
        --data-only \
        --schema=public \
        --exclude-table-data='"Document"' \
        --exclude-table-data='"Identifiers"' \
        --exclude-table-data='"User"*' \
        --exclude-table-data='*aggregates' \
        --exclude-table-data='*measurements' \
        --exclude-table-data='*invoices' \
        --exclude-table-data='*calculations' \
        --exclude-table-data='"__EFMigrationsHistory"' \
    out> '{{migrationassets}}/current.sql'

migrate name:
  docker compose down -v
  docker compose up -d
  sleep 10sec

  open --raw '{{migrationassets}}/current-orchard.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update

  open --raw '{{migrationassets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    migrations add \
    --output-dir Migrations \
    --namespace Ozds.Data.Migrations \
    '{{name}}'

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update \
    '{{name}}'

  docker exec \
    --env PGHOST="localhost" \
    --env PGPORT="5432" \
    --env PGDATABASE="ozds" \
    --env PGUSER="ozds" \
    --env PGPASSWORD="ozds" \
    --interactive \
    ozds-postgres-1 \
      pg_dump \
        --schema=public \
        --table='"Document"' \
        --table='"Identifiers"' \
        --table='"User"*' \
    out> '{{migrationassets}}/{{name}}-orchard.sql'

  cp -f \
    '{{migrationassets}}/{{name}}-orchard.sql' \
    '{{migrationassets}}/current-orchard.sql'

  docker exec \
    --env PGHOST="localhost" \
    --env PGPORT="5432" \
    --env PGDATABASE="ozds" \
    --env PGUSER="ozds" \
    --env PGPASSWORD="ozds" \
    --interactive \
    ozds-postgres-1 \
      pg_dump \
        --data-only \
        --schema=public \
        --exclude-table-data='"Document"' \
        --exclude-table-data='"Identifiers"' \
        --exclude-table-data='"User"*' \
        --exclude-table-data='*aggregates' \
        --exclude-table-data='*measurements' \
        --exclude-table-data='*invoices' \
        --exclude-table-data='*calculations' \
        --exclude-table-data='"__EFMigrationsHistory"' \
    out> '{{migrationassets}}/{{name}}.sql'

  cp -f \
    '{{migrationassets}}/{{name}}.sql' \
    '{{migrationassets}}/current.sql'

  mermerd \
    --schema public \
    --useAllTables \
    --encloseWithMermaidBackticks \
    --outputFileName '{{schema}}'

# NOTE: https://github.com/doxygen/doxygen/issues/6783
docs:
  rm -rf '{{artifacts}}'
  mkdir '{{artifacts}}'

  let result = glob '{{docs}}/**/Doxyfile' | \
    filter { |$doxyfile| $doxyfile != '{{doxyfile}}' } | \
    par-each { |$doxyfile| \
      let temp = mktemp -d; \
      open {{doxyfile}} $doxyfile | \
        str join | \
        save $"($temp)/Doxyfile"; \
      let assets = $"($doxyfile | path dirname)/assets"; \
      let lang = $"($doxyfile | path dirname | path basename)"; \
      print $"Generating documentation for language '($lang)'..."; \
      doxygen -q $"($temp)/Doxyfile" o+e>| ignore; \
      mkdir $"{{artifacts}}/($lang)/docs/($lang)"; \
      cp -r $"{{docs}}/($lang)/assets" $"{{artifacts}}/($lang)/docs/($lang)"; \
      rm -rf $temp; \
    };

  cp '{{docs}}/index.html' {{artifacts}}
  cp '{{docs}}/favicon.ico' {{artifacts}}

publish *args:
  rm -rf '{{artifacts}}'
  mkdir '{{artifacts}}'

  dotnet publish '{{sln}}' \
    --property PublishDir='{{artifacts}}' \
    --property ConsoleLoggerParameters=ErrorsOnly \
    --property IsWebConfigTransformDisabled=true \
    --property DebugType=None \
    --property DebugSymbols=false \
    --configuration Release \
    {{args}}

  rm -rf '{{artifacts}}/App_Data'

[confirm("This will clean docker containers. Do you want to continue?")]
clean:
  docker compose down -v
  docker compose up -d
  sleep 10sec

  open --raw '{{migrationassets}}/current-orchard.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update

  open --raw '{{migrationassets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

[confirm("This will clean docker containers and dotnet artifacts. Do you want to continue?")]
purge:
  git clean -Xdf \
    -e !.vscode/ \
    -e !.vscode/** \
    -e !.vs/ \
    -e !.vs/** \
    -e !.idea/ \
    -e !.idea/** \
    -e !**/*.csproj.user \
    -e !.direnv/ \
    -e !.direnv/bin/
  dotnet tool restore
  dotnet restore

  docker compose down -v
  docker compose up -d
  sleep 10sec

  open --raw '{{migrationassets}}/current-orchard.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update

  open --raw '{{migrationassets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql
