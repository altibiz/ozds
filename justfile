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
messagingcsproj := absolute_path('src/Ozds.Messaging/Ozds.Messaging.csproj')
jobscsproj := absolute_path('src/Ozds.Jobs/Ozds.Jobs.csproj')
fakecsproj := absolute_path('scripts/Ozds.Fake/Ozds.Fake.csproj')
fakeassets := absolute_path('scripts/Ozds.Fake/Assets')
migrationassets := absolute_path('scripts/migrations')
docs := absolute_path('docs')
doxyfile := absolute_path('docs/Doxyfile')
schema := absolute_path('docs/schema.md')
isready := absolute_path('scripts/database/isready.nu')
rewind := absolute_path('scripts/database/rewind.nu')
rollback := absolute_path('scripts/database/rollback.nu')
validate := absolute_path('scripts/database/validate.nu')
measurements := absolute_path('scripts/database/measurements.nu')
current := "current"

default:
    @just --choose

prepare:
    dvc pull
    dotnet tool restore
    (not (which prettier | is-empty)) or (npm install -g prettier) | ignore
    @just clean

lfs:
    dvc add {{ fakeassets }}/*.csv
    dvc add {{ migrationassets }}/*.sql
    dvc push

dev *args:
    docker compose up -d
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
      $env.DOTNET_ENVIRONMENT = "Development"; \
      dotnet watch --project '{{ servercsproj }}' {{ args }}

fake *args:
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
      $env.DOTNET_ENVIRONMENT = "Development"; \
      dotnet run  --project '{{ fakecsproj }}' -- {{ args }}

measurements *args:
    python -m scripts.database.measurements {{ args }}

format:
    cd '{{ root }}'; just --fmt --unstable

    nixpkgs-fmt '{{ root }}'

    prettier --write \
      --ignore-path '{{ gitignore }}' \
      --ignore-path '{{ prettierignore }}' \
      --cache --cache-strategy metadata \
      '{{ root }}'

    yapf --recursive --in-place --parallel '{{ root }}'

    dotnet jb cleanupcode '{{ sln }}' \
      --verbosity=WARN \
      --caches-home='{{ jbcache }}' \
      -o='{{ jbinspectlog }}' \
      --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

deps:
    exec (nix build ".#default.fetch-deps" --print-out-paths --no-link) deps.nix

lint:
    prettier --check \
      --ignore-path '{{ gitignore }}' \
      --ignore-path '{{ prettierignore }}' \
      --cache --cache-strategy metadata \
      '{{ root }}'

    cspell lint '{{ root }}' \
      --no-progress

    markdownlint '{{ root }}'
    markdown-link-check --config .markdown-link-check.json --quiet ...(glob '**/*.md')

    pyright '{{ root }}'
    ruff check '{{ root }}'

    dotnet build --no-incremental /warnaserror '{{ sln }}'

    dotnet roslynator analyze '{{ sln }}' \
      --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

    dotnet jb inspectcode '{{ sln }}' \
      --no-build \
      --verbosity=WARN \
      --caches-home='{{ jbcache }}' \
      -o='{{ jbinspectlog }}' \
      --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

    @just lint-model

lint-model:
    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ datacsproj }}' \
      has-pending-model-changes

    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ messagingcsproj }}' \
      has-pending-model-changes

    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ jobscsproj }}' \
      has-pending-model-changes

test *args:
    dotnet test '{{ sln }}' {{ args }}

publish *args:
    rm -rf '{{ artifacts }}'
    mkdir '{{ artifacts }}'

    dotnet publish '{{ sln }}' \
      --property PublishDir='{{ artifacts }}' \
      --property ConsoleLoggerParameters=ErrorsOnly \
      --property IsWebConfigTransformDisabled=true \
      --property DebugType=None \
      --property DebugSymbols=false \
      --configuration Release \
      {{ args }}

    rm -rf '{{ artifacts }}/App_Data'

docs:
    rm -rf '{{ artifacts }}'
    mkdir '{{ artifacts }}'

    dotnet docfx metadata '{{ docs }}/code/docfx.json'
    dotnet docfx build '{{ docs }}/code/docfx.json'
    cp -f '{{ docs }}/favicon.ico' '{{ artifacts }}/code'
    cp -f '{{ docs }}/logo.svg' '{{ artifacts }}/code'

    mdbook build '{{ docs }}/wiki/en'
    mdbook build '{{ docs }}/wiki/hr'
    mv '{{ docs }}/wiki/en/book' '{{ artifacts }}/wiki/en'
    mv '{{ docs }}/wiki/hr/book' '{{ artifacts }}/wiki/hr'

    cp '{{ docs }}/index.html' {{ artifacts }}
    cp '{{ docs }}/favicon.ico' {{ artifacts }}

migrate project name:
    @just clean

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ root }}/src/{{ project }}/{{ project }}.csproj' \
      migrations add \
      --output-dir Migrations \
      --namespace {{ project }}.Migrations \
      '{{ name }}'

    glob ('{{ root }}/src/{{ project }}/' + \
      ('{{ project }}.Migrations' | split row '.' | path join) + '/*') | \
      each { |x| mv -f $x '{{ root }}/src/{{ project }}/Migrations' } | ignore

    @just migrate-continue '{{ project }}' '{{ name }}'

migrate-continue project name:
    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ root }}/src/{{ project }}/{{ project }}.csproj' \
      database update

    let now = ls '{{ root }}/src/{{ project }}/Migrations' \
      | sort-by name \
      | reverse \
      | get name \
      | path basename \
      | parse "{timestamp}_{name}.cs" \
      | get timestamp \
      | first; \
    just dump $"($now)-{{ project }}-{{ name }}"; \
    cp -f \
      $"{{ migrationassets }}/($now)-{{ project }}-{{ name }}-orchard.sql" \
      '{{ migrationassets }}/current-orchard.sql'; \
    cp -f \
      $"{{ migrationassets }}/($now)-{{ project }}-{{ name }}.sql" \
      '{{ migrationassets }}/current.sql'; \
    cp -f \
      $"{{ migrationassets }}/($now)-{{ project }}-{{ name }}-hypertables.sql" \
      '{{ migrationassets }}/current-hypertables.sql'

    mermerd \
      --schema public \
      --useAllTables \
      --encloseWithMermaidBackticks \
      --outputFileName '{{ schema }}'

    $"# Database schema\n\n(open --raw '{{ schema }}')" | \
      save --force '{{ schema }}'

dump name=current:
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
      out> '{{ migrationassets }}/{{ name }}-orchard.sql'

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
          --exclude-table-data='outbox_state' \
          --exclude-table-data='inbox_state' \
          --exclude-table-data='outbox_message' \
          --exclude-table-data='"qrtz_"*' \
          --exclude-table-data='"__"*' \
      out> '{{ migrationassets }}/{{ name }}.sql'

    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
      psql -c "DO $$ \
        DECLARE \
          ht RECORD; \
        BEGIN \
          FOR ht IN \
            SELECT hypertable_schema, hypertable_name \
            FROM timescaledb_information.hypertables \
            WHERE hypertable_name LIKE '%measurements' OR hypertable_name LIKE '%aggregates' \
          LOOP \
            EXECUTE format('CREATE TABLE %I_export AS SELECT * FROM %I.%I;', ht.hypertable_name, ht.hypertable_schema, ht.hypertable_name); \
          END LOOP; \
        END; \
      $$;"

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
        --table='*_export' \
      out> '{{ migrationassets }}/{{ name }}-hypertables.sql'

    open '{{ migrationassets }}/{{ name }}-hypertables.sql' \
      | str replace -ar 'COPY (\S+)_export ' 'COPY $1 ' \
      | save -f '{{ migrationassets }}/{{ name }}-hypertables.sql'

    docker exec \
      --env PGHOST="localhost" \
      --env PGPORT="5432" \
      --env PGDATABASE="ozds" \
      --env PGUSER="ozds" \
      --env PGPASSWORD="ozds" \
      --interactive \
      ozds-postgres-1 \
      psql -c "DO $$ \
        DECLARE \
          tbl RECORD; \
        BEGIN \
          FOR tbl IN \
            SELECT table_schema, table_name \
            FROM information_schema.tables \
            WHERE table_name LIKE '%_export' AND table_schema = 'public' \
          LOOP \
            EXECUTE format('DROP TABLE %I.%I;', tbl.table_schema, tbl.table_name); \
          END LOOP; \
        END; \
      $$;"

report quarter language ext:
    rm -rf '{{ artifacts }}'
    mkdir '{{ artifacts }}'

    pandoc \
      --from=markdown+rebase_relative_paths \
      --to=docx+native_numbering \
      --standalone \
      --table-of-contents \
      --output='{{ artifacts }}/ozds-{{ quarter }}-report-{{ language }}.{{ ext }}' \
      --filter=pandoc-plantuml \
      {{ docs }}/wiki/{{ language }}/report/{{ quarter }}/*.md

[confirm("This will clean docker containers. Do you want to continue?")]
rewind *args:
    {{ rewind }} {{ args }}

[confirm("This will clean docker containers. Do you want to continue?")]
rollback *args:
    {{ rollback }} {{ args }}

[confirm("This will clean docker containers. Do you want to continue?")]
validate *args:
    {{ validate }} {{ args }}

[confirm("This will clean docker containers. Do you want to continue?")]
clean:
    docker compose ps -a -q | lines | each { |x| docker stop $x }
    docker compose --profile "*" down -v
    docker compose up -d
    nu {{ isready }}

    open --raw '{{ migrationassets }}/current-orchard.sql' | \
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
      --startup-project '{{ servercsproj }}' \
      --project '{{ datacsproj }}' \
      database update

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ messagingcsproj }}' \
      database update

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ jobscsproj }}' \
      database update

    open --raw '{{ migrationassets }}/current.sql' | \
      docker exec \
        --env PGHOST="localhost" \
        --env PGPORT="5432" \
        --env PGDATABASE="ozds" \
        --env PGUSER="ozds" \
        --env PGPASSWORD="ozds" \
        --interactive \
        ozds-postgres-1 \
          psql

    open --raw '{{ migrationassets }}/current-hypertables.sql' | \
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

    @just clean
