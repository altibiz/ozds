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
migrationcsproj := absolute_path('scripts/Ozds.Migration/Ozds.Migration.csproj')
migrationassets := absolute_path('scripts/migrations')
docs := absolute_path('docs')
doxyfile := absolute_path('docs/Doxyfile')
schema := absolute_path('docs/schema.md')
postgrescontainer := absolute_path('scripts/database/postgrescontainer.nu')
isdatabaseready := absolute_path('scripts/database/isready.nu')
isllmready := absolute_path('scripts/llm/isready.nu')
rewind := absolute_path('scripts/database/rewind.nu')
rollback := absolute_path('scripts/database/rollback.nu')
validate := absolute_path('scripts/database/validate.nu')
measurements := absolute_path('scripts/database/measurements.nu')
playwright := absolute_path('src/Ozds.Server/bin/Debug/net8.0/playwright.ps1')
ozdsserver := absolute_path('scripts/startup/ozds-server.sh')
ozdsserverdev := absolute_path('scripts/startup/ozds-server-dev.sh')
raspberryPi4 := absolute_path('scripts/flake/raspberryPi4.nu')
translationshr := absolute_path('src/Ozds.Assets/Assets/Translations/hr.xml')
translationsen := absolute_path('src/Ozds.Assets/Assets/Translations/en.xml')
srcdir := absolute_path('src')
testdir := absolute_path('test')
usersdb := absolute_path('scripts/ldap/users.db')
usersdbtemplate := absolute_path('scripts/ldap/users.db.template')
lldapconfigtoml := absolute_path('scripts/ldap/lldap_config.toml')
lldapconfigtomltemplate := absolute_path('scripts/ldap/lldap_config.toml.template')
usersyml := absolute_path('scripts/auth/users.yml')
usersymltemplate := absolute_path('scripts/auth/users.yml.template')
configurationyml := absolute_path('scripts/auth/configuration.yml')
configurationymltemplate := absolute_path('scripts/auth/configuration.yml.template')
current := "current"

default:
    @just --choose

prepare:
    dvc pull
    dotnet restore
    dotnet tool restore
    dotnet build
    (which prettier | is-not-empty) or (npm install -g prettier)
    ($env | get --optional PLAYWRIGHT_BROWSERS_PATH | is-not-empty) or \
      ((pwsh '{{ playwright }}' install --with-deps chromium) | is-empty)
    @just clean

up *args:
    ((docker run --rm --device=nvidia.com/gpu=all hello-world \
      | complete | get exit_code) == 0) \
      and (docker compose --profile cuda up -d {{ args }}; true) \
      or (docker compose --profile cpu up -d {{ args }}; true)

lfs:
    dvc add {{ fakeassets }}/*.csv
    dvc add {{ migrationassets }}/*.sql
    dvc push

dev *args:
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
      $env.DOTNET_ENVIRONMENT = "Development"; \
      dotnet watch --project '{{ servercsproj }}' {{ args }}

fake *args:
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
      $env.DOTNET_ENVIRONMENT = "Development"; \
      dotnet run  --project '{{ fakecsproj }}' -- {{ args }}

migration *args:
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
      $env.DOTNET_ENVIRONMENT = "Development"; \
      dotnet run  --project '{{ migrationcsproj }}' -- {{ args }}

translation *args:
    $env.ASPNETCORE_ENVIRONMENT = "Development"; \
    $env.DOTNET_ENVIRONMENT = "Development"; \
    dotnet run --project 'scripts/Ozds.Translation/Ozds.Translation.csproj' -- {{ args }}

translate:
    @just translation type \
      -l hr \
      -a \
        Ozds.Business \
        Ozds.Document \
        Ozds.Report \
      -n \
        Ozds.Business.Models \
        Ozds.Business.Analysis \
        Ozds.Document.Entities \
        Ozds.Report.Entities \
      -u {{ translationshr }} \
      -o {{ translationshr }} \
      -r
    @just translation regex \
      -l hr \
      -i {{ srcdir }} \
      -u {{ translationshr }} \
      -o {{ translationshr }} \
      -r
    @just translation type \
      -l en \
      -a \
        Ozds.Business \
        Ozds.Document \
        Ozds.Report \
      -n \
        Ozds.Business.Models \
        Ozds.Business.Analysis \
        Ozds.Document.Entities \
        Ozds.Report.Entities \
      -u {{ translationsen }} \
      -o {{ translationsen }} \
      -r
    @just translation regex \
      -l en \
      -i {{ srcdir }} \
      -u {{ translationsen }} \
      -o {{ translationsen }} \
      -r

sdk:
    dotnet nswag openapi2csclient \
      /input:"http://localhost:5000/api/v1/openapi.json" \
      /output:"{{ root }}/src/Ozds.Sdk/Client/V1/OzdsApiV1Client.cs" \
      /namespace:Ozds.Sdk.Client.V1 \
      /generateClientInterfaces:true \
      /generateClientClasses:true \
      /className:OzdsApiV1Client \
      /clientClassAccessModifier:internal \
      /jsonLibrary:SystemTextJson \
      /jsonPolymorphicSerializationStyle:SystemTextJson \
      /useHttpClientCreationMethod:true \
      /useBaseUrl:false \
      /generateSyncMethods:true \
      /generateResponseClasses:true \
      /responseClass:"OzdsApiV1{controller}Response" \
      /generateExceptionClasses:true \
      /exceptionClass:"OzdsApiV1{controller}Exception" \
      /generateContractsOutput:true \
      /contractsNamespace:Ozds.Sdk.Contracts.V1 \
      /contractsOutput:"{{ root }}/src/Ozds.Sdk/Contracts/V1/OzdsApiV1Contracts.cs" \
      /newLineBehavior:LF

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

    # yapf --recursive --in-place --parallel '{{ root }}'

    dotnet jb cleanupcode '{{ sln }}' \
      --verbosity=WARN \
      --caches-home='{{ jbcache }}' \
      -o='{{ jbinspectlog }}' \
      --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*;**/*.xml'

deps:
    exec \
      (nix build ".#default.fetch-deps" --print-out-paths --no-link) \
      ./scripts/flake/ozds/deps.json

lint:
    prettier --check \
      --ignore-path '{{ gitignore }}' \
      --ignore-path '{{ prettierignore }}' \
      --cache --cache-strategy metadata \
      '{{ root }}'

    @just lint-spelling

    markdownlint '{{ root }}'
    # FIXME: config file usage breaking markdown-link-check
    if (markdown-link-check ...(fd '^.*.md$' | lines) \
      | rg -q error \
      | complete \
      | get exit_code) == 0 { exit 1 }

    # TODO: make it work in CI
    # ($env | get CI? | is-not-empty) \
    #   or ((pyright '{{ root }}' | complete | get exit_code) == 0)
    # ruff check '{{ root }}'

    @just lint-dotnet

    @just lint-model

lint-spelling:
    cspell lint '{{ root }}' \
      --no-progress

lint-dotnet:
    dotnet build --no-incremental /warnaserror '{{ sln }}'

    # commented out for now since roslynator is not yet compatible with .NET 10 SDK
    #dotnet roslynator analyze '{{ sln }}' \
    #  --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

    dotnet jb inspectcode '{{ sln }}' \
      --no-build \
      --verbosity=WARN \
      --caches-home='{{ jbcache }}' \
      -o='{{ jbinspectlog }}' \
      --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

lint-model:
    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ datacsproj }}' \
      has-pending-model-changes \
      --context 'Ozds.Data.Context.DataDbContext'

    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ messagingcsproj }}' \
      has-pending-model-changes \
      --context 'Ozds.Messaging.Context.MessagingDbContext'

    dotnet ef migrations \
      --startup-project '{{ servercsproj }}' \
      --project '{{ jobscsproj }}' \
      has-pending-model-changes \
      --context 'Ozds.Jobs.Context.JobsDbContext'

test-sln *args:
    dotnet test '{{ sln }}' {{ args }}

test-ci *args:
    ls '{{ testdir }}' \
      | where $it.type == "dir" \
      | where { not ($in.name | str ends-with "Ozds.Server.Test") } \
      | where { not ($in.name | str ends-with "Ozds.Caching.Test") } \
      | each { \
          print ($in.name | path basename); \
          let result = (dotnet test \
            $"($in.name)/($in.name | path basename).csproj" \
            {{ args }}) | complete; \
          print $result.stdout; \
          print $result.stderr; \
          if $result.exit_code != 0 { exit 1; }; \
        } \
      | ignore

test *args:
    ls '{{ testdir }}' \
      | where $it.type == "dir" \
      | each { \
          print ($in.name | path basename); \
          let result = (dotnet test \
            $"($in.name)/($in.name | path basename).csproj" \
            {{ args }}) | complete; \
          print $result.stdout; \
          print $result.stderr; \
          if $result.exit_code != 0 { exit 1; }; \
        } \
      | ignore

publish *args:
    rm -rf '{{ artifacts }}'
    mkdir '{{ artifacts }}'

    dotnet publish '{{ servercsproj }}' \
      --property PublishDir='{{ artifacts }}' \
      --property ConsoleLoggerParameters=ErrorsOnly \
      --property IsWebConfigTransformDisabled=true \
      --property DebugType=None \
      --property DebugSymbols=false \
      --configuration Release \
      {{ args }}
    mv "{{ artifacts }}/.playwright" "{{ artifacts }}/playwright"

    cp '{{ ozdsserver }}' '{{ artifacts }}/ozds-server'
    cp '{{ ozdsserverdev }}' '{{ artifacts }}/ozds-server-dev'

    mkdir ("{{ artifacts }}/playwright/package/.local-browsers" \
      + "/chromium_headless_shell-1155/chrome-linux")

    cd ("{{ artifacts }}/playwright/package/.local-browsers" \
      + "/chromium_headless_shell-1155/chrome-linux"); \
      nix-bundle \
        '(builtins.getFlake "git+file:{{ root }}").packages.${builtins.currentSystem}.playwrightBrowsers' \
        "/chromium_headless_shell-1155/chrome-linux/headless_shell"

    # NOTE: leaving it here for future reference if linus decies nested namespaces are cool
    # mkdir "{{ artifacts }}/.playwright/node/linux-x64"
    # cd "{{ artifacts }}/.playwright/node/linux-x64"; \
    #   nix-bundle \
    #     '(builtins.getFlake "git+file:{{ root }}").packages.${builtins.currentSystem}.playwrightNode' \
    #     "/bin/node"

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

raspberryPi4 *args:
    {{ raspberryPi4 }} {{ args }}

migrate project context name:
    @just clean

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ root }}/src/{{ project }}/{{ project }}.csproj' \
      migrations add \
      --context '{{ context }}' \
      --output-dir Migrations \
      --namespace {{ project }}.Migrations \
      '{{ name }}'

    glob ('{{ root }}/src/{{ project }}/' + \
      ('{{ project }}.Migrations' | split row '.' | path join) + '/*') | \
      each { |x| mv -f $x '{{ root }}/src/{{ project }}/Migrations' } | ignore

    @just --yes migrate-continue '{{ project }}' '{{ context }}' '{{ name }}'

[confirm("This will proceed with the migration and dump the database. Would you like to continue?")]
migrate-continue project context name:
    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ root }}/src/{{ project }}/{{ project }}.csproj' \
      database update \
      --context '{{ context }}'

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
      (nu {{ postgrescontainer }} name) \
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
      (nu {{ postgrescontainer }} name) \
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
      (nu {{ postgrescontainer }} name) \
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
      (nu {{ postgrescontainer }} name) \
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
    if (not ('{{ usersdb }}' | path exists)) { cp -f '{{ usersdbtemplate }}' '{{ usersdb }}' }
    if (not ('{{ lldapconfigtoml }}' | path exists)) { cp -f '{{ lldapconfigtomltemplate }}' '{{ lldapconfigtoml }}' }
    if (not ('{{ usersyml }}' | path exists)) { cp -f '{{ usersymltemplate }}' '{{ usersyml }}' }
    if (not ('{{ configurationyml }}' | path exists)) { cp -f '{{ configurationymltemplate }}' '{{ configurationyml }}' }

    docker compose ps -a -q | lines | each { |x| docker stop $x }
    docker compose --profile "*" down
    docker volume ls -q | lines \
      | where { |x| \
          ($x | str starts-with "ozds") \
          and not ($x | str contains "ollama") \
        } \
      | each { |x| docker volume rm $x }
    @just up

    #nu {{ isllmready }}

    nu {{ isdatabaseready }}

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ datacsproj }}' \
      database update \
      --context 'Ozds.Data.Context.DataDbContext'

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ messagingcsproj }}' \
      database update \
      --context 'Ozds.Messaging.Context.MessagingDbContext'

    dotnet ef \
      --startup-project '{{ servercsproj }}' \
      --project '{{ jobscsproj }}' \
      database update \
      --context 'Ozds.Jobs.Context.JobsDbContext'

    open --raw '{{ migrationassets }}/current.sql' | \
      docker exec \
        --env PGHOST="localhost" \
        --env PGPORT="5432" \
        --env PGDATABASE="ozds" \
        --env PGUSER="ozds" \
        --env PGPASSWORD="ozds" \
        --interactive \
        (nu {{ postgrescontainer }} name) \
          psql

    open --raw '{{ migrationassets }}/current-hypertables.sql' | \
      docker exec \
        --env PGHOST="localhost" \
        --env PGPORT="5432" \
        --env PGDATABASE="ozds" \
        --env PGUSER="ozds" \
        --env PGPASSWORD="ozds" \
        --interactive \
        (nu {{ postgrescontainer }} name) \
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
      -e !.direnv/bin/ \
      -e !.dvc/config.local
    dvc pull
    dotnet tool restore
    dotnet restore

    @just clean
