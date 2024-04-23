# TODO: rm -rf on windows

set windows-shell := ["nu.exe", "-c"]
set shell := ["nu", "-c"]

root := absolute_path('')
assets := absolute_path('assets')
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
schema := absolute_path('docs/structure/data/schema.md')

default: prepare

prepare:
  dvc install
  dvc pull
  dotnet tool restore
  prettier --version or npm install -g prettier

  docker compose up -d
  sleep 3sec

  cat '{{assets}}/current-orchard.sql' | \
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

  cat '{{assets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql

ci:
  dotnet tool restore
  prettier --version or npm install -g prettier

dev *args:
  dotnet watch --project '{{servercsproj}}' {{args}}

fake *args:
  dotnet run --project '{{fakecsproj}}' -- {{args}}

format:
  prettier --write \
    --ignore-path '{{gitignore}}' \
    --ignore-path '{{prettierignore}}' \
    --cache --cache-strategy metadata \
    '{{root}}'

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
    --verbosity=ERROR \
    --caches-home='{{jbcache}}' \
    -o='{{jbinspectlog}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

lint:
  prettier --check \
    --ignore-path '{{gitignore}}' \
    --ignore-path '{{prettierignore}}' \
    --cache --cache-strategy metadata \
    '{{root}}'

  dotnet build '{{sln}}'

  dotnet format '{{sln}}' \
    --verify-no-changes \
    --no-restore \
    --verbosity minimal \
    --severity info \
    --exclude '**/.git/**/*' \
    --exclude '**/.nuget/**/*' \
    --exclude '**/obj/**/*' \
    --exclude '**/bin/**/*'

  dotnet roslynator analyze '{{sln}}' \
    --verbosity minimal \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet jb inspectcode '{{sln}}' \
    --no-build \
    --verbosity=ERROR \
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
    out> '{{assets}}/current-orchard.sql'

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
        --exclude-table-data=%aggregates \
        --exclude-table-data=%measurements \
        --exclude-table-data=%invoices \
        --exclude-table-data=%calculations \
        --exclude-table-data='"__EFMigrationsHistory"' \
    out> '{{assets}}/current.sql'

migrate name:
  docker compose down -v
  docker compose up -d
  sleep 3sec

  cat '{{assets}}/current-orchard.sql' | \
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

  cat '{{assets}}/current.sql' | \
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
    out> '{{assets}}/{{name}}-orchard.sql'

  cp -f \
    '{{assets}}/{{name}}-orchard.sql' \
    '{{assets}}/current-orchard.sql'

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
        --exclude-table-data=%aggregates \
        --exclude-table-data=%measurements \
        --exclude-table-data=%invoices \
        --exclude-table-data=%calculations \
        --exclude-table-data='"__EFMigrationsHistory"' \
    out> '{{assets}}/{{name}}.sql'

  cp -f \
    '{{assets}}/{{name}}.sql' \
    '{{assets}}/current.sql'

  mermerd \
    --schema public \
    --useAllTables \
    --encloseWithMermaidBackticks \
    --outputFileName '{{schema}}'

publish *args:
  dotnet publish '{{sln}}' \
    --property PublishDir='{{artifacts}}' \
    --property ConsoleLoggerParameters=ErrorsOnly \
    --property IsWebConfigTransformDisabled=true \
    --property DebugType=None \
    --property DebugSymbols=false \
    --configuration Release \
    {{args}}

[confirm("This will clean docker containers. Do you want to continue?")]
clean:
  docker compose down -v
  docker compose up -d
  sleep 3sec

  cat '{{assets}}/current-orchard.sql' | \
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

  cat '{{assets}}/current.sql' | \
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
  sleep 3sec

  cat '{{assets}}/current-orchard.sql' | \
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

  cat '{{assets}}/current.sql' | \
    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
        psql
