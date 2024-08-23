set windows-shell := ["nu.exe", "-c"]
set shell := ["nu", "-c"]

# TODO: use hunspell with dictionaries
# FIXME: pyright '{{root}}'

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
messagingcsproj := absolute_path('src/Ozds.Messaging/Ozds.Messaging.csproj')
fakecsproj := absolute_path('scripts/Ozds.Fake/Ozds.Fake.csproj')
fakeassets := absolute_path('scripts/Ozds.Fake/Assets')
migrationassets := absolute_path('scripts/migrations')
docs := absolute_path('docs')
doxyfile := absolute_path('docs/Doxyfile')
schema := absolute_path('docs/schema.md')

waitservicescommand := (
  'loop { ' +
  '  try { ' +
  '    (docker exec (' +
  '      docker compose ps --format json | ' +
  '        lines | ' +
  '        each { $in | from json } | ' +
  '        filter { $in.Image | str starts-with "timescale" } | ' +
  '        first | ' +
  '        get id) pg_isready --host localhost); ' +
  '    break ' +
  '  } catch { ' +
  '    sleep 1sec; ' +
  '    continue ' +
  '  } ' +
  '}'
)

now := `date now | format date '%Y%m%d%H%M%S'`

default: prepare

prepare:
  do -i { dvc install } o+e>| ignore
  dvc pull
  dotnet tool restore
  (not (which prettier | is-empty)) or (npm install -g prettier) | ignore

  docker compose --profile "*" down -v
  docker compose up -d
  {{waitservicescommand}}

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

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
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
  dvc push

dev *args:
  $env.ASPNETCORE_ENVIRONMENT = "Development"; \
    $env.DOTNET_ENVIRONMENT = "Development"; \
    dotnet watch --project '{{servercsproj}}' {{args}}

fake *args:
  $env.ASPNETCORE_ENVIRONMENT = "Development"; \
    $env.DOTNET_ENVIRONMENT = "Development"; \
    dotnet run  --project '{{fakecsproj}}' -- {{args}}

format:
  nixpkgs-fmt {{root}}

  prettier --write \
    --ignore-path '{{gitignore}}' \
    --ignore-path '{{prettierignore}}' \
    --cache --cache-strategy metadata \
    '{{root}}'

  yapf --recursive --in-place --parallel '{{root}}'

  dotnet jb cleanupcode '{{sln}}' \
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

  cspell lint '{{root}}' \
    --no-progress

  markdownlint '{{root}}'
  markdown-link-check --config .markdown-link-check.json --quiet ...(glob '**/*.md')

  ruff check '{{root}}'

  dotnet build --no-incremental /warnaserror '{{sln}}'

  dotnet roslynator analyze '{{sln}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet jb inspectcode '{{sln}}' \
    --no-build \
    --verbosity=WARN \
    --caches-home='{{jbcache}}' \
    -o='{{jbinspectlog}}' \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet ef migrations \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    has-pending-model-changes

  dotnet ef migrations \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
    has-pending-model-changes

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
        --exclude-table-data='*state' \
        --exclude-table-data='outbox_state' \
        --exclude-table-data='inbox_state' \
        --exclude-table-data='outbox_message' \
        --exclude-table-data='"__"*' \
    out> '{{migrationassets}}/current.sql'

migrate project name:
  docker compose --profile "*" down -v
  docker compose up -d
  {{waitservicescommand}}

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

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
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
    --project '{{root}}/src/{{project}}/{{project}}.csproj' \
    migrations add \
    --output-dir Migrations \
    --namespace {{project}}.Migrations \
    '{{name}}'

  glob ('{{root}}/src/{{project}}/' + \
    ('{{project}}.Migrations' | split row '.' | path join) + '/*') | \
    each { |x| mv -f $x '{{root}}/src/{{project}}/Migrations' } | ignore

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{datacsproj}}' \
    database update

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
    database update

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
    out> '{{migrationassets}}/{{project}}-{{name}}-{{now}}-orchard.sql'

  cp -f \
    '{{migrationassets}}/{{project}}-{{name}}-{{now}}-orchard.sql' \
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
        --exclude-table-data='*state' \
        --exclude-table-data='outbox_state' \
        --exclude-table-data='inbox_state' \
        --exclude-table-data='outbox_message' \
        --exclude-table-data='"__"*' \
    out> '{{migrationassets}}/{{project}}-{{name}}-{{now}}.sql'

  cp -f \
    '{{migrationassets}}/{{project}}-{{name}}-{{now}}.sql' \
    '{{migrationassets}}/current.sql'

  mermerd \
    --schema public \
    --useAllTables \
    --encloseWithMermaidBackticks \
    --outputFileName '{{schema}}'

  $"# Database schema\n\n(open --raw '{{schema}}')" | \
    save --force '{{schema}}'

# NOTE: https://github.com/doxygen/doxygen/issues/6783
docs:
  rm -rf '{{artifacts}}'
  mkdir '{{artifacts}}'

  dotnet docfx metadata '{{docs}}/code/docfx.json'
  dotnet docfx build '{{docs}}/code/docfx.json'
  cp -f '{{docs}}/favicon.ico' '{{artifacts}}/code'
  cp -f '{{docs}}/logo.svg' '{{artifacts}}/code'

  mdbook build '{{docs}}/wiki/en'
  mdbook build '{{docs}}/wiki/hr'
  mv '{{docs}}/wiki/en/book' '{{artifacts}}/wiki/en'
  mv '{{docs}}/wiki/hr/book' '{{artifacts}}/wiki/hr'

  cp '{{docs}}/index.html' {{artifacts}}
  cp '{{docs}}/favicon.ico' {{artifacts}}

deps:
  exec (nix build ".#default.fetch-deps" --print-out-paths --no-link) deps.nix

report quarter language ext:
  rm -rf '{{artifacts}}'
  mkdir '{{artifacts}}'

  pandoc \
    --from=markdown+rebase_relative_paths \
    --to=docx+native_numbering \
    --standalone \
    --table-of-contents \
    --output='{{artifacts}}/ozds-{{quarter}}-report-{{language}}.{{ext}}' \
    --filter=pandoc-plantuml \
    {{docs}}/wiki/{{language}}/report/{{quarter}}/*.md

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
  docker compose --profile "*" down -v
  docker compose up -d
  {{waitservicescommand}}

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

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
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

  docker compose --profile "*" down -v
  docker compose up -d
  {{waitservicescommand}}

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

  dotnet ef \
    --startup-project '{{servercsproj}}' \
    --project '{{messagingcsproj}}' \
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
