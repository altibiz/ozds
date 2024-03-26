set windows-shell := ["pwsh.exe", "-c"]

root := absolute_path('')
sln := absolute_path('ozds.sln')
gitignore := absolute_path('.gitignore')
prettierignore := absolute_path('.prettierignore')
jbcache := absolute_path('.jb/cache')
jbcleanuplog := absolute_path('.jb/cleanup.log')
jbinspectlog := absolute_path('.jb/inspect.log')
artifacts := absolute_path('artifacts')
appdata := absolute_path('App_Data')
servercsproj := absolute_path('src/Ozds.Server/Ozds.Server.csproj')
datacsproj := absolute_path('src/Ozds.Data/Ozds.Data.csproj')
fakecsproj := absolute_path('scripts/Ozds.Fake/Ozds.Fake.csproj')

default: prepare

prepare:
  dotnet tool restore
  prettier --version || npm install -g prettier
  # remark --version || npm install -g remark
  docker compose up -d

ci:
  dotnet tool restore
  prettier --version || npm install -g prettier
  # remark --version || npm install -g remark

dev *args:
  dotnet watch --project "{{servercsproj}}" {{args}}

fake *args:
  dotnet run --project "{{fakecsproj}}" {{args}}

format:
  prettier --write \
    --ignore-path "{{gitignore}}" \
    --ignore-path "{{prettierignore}}" \
    --cache --cache-strategy metadata \
    "{{root}}"

  dotnet jb cleanupcode "{{sln}}" \
    --no-build \
    --verbosity=ERROR \
    --caches-home="{{jbcache}}" \
    -o="{{jbcleanuplog}}" \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet format "{{sln}}" \
    --no-restore \
    --verbosity minimal \
    --severity info \
    --exclude '**/.git/**/*' \
    --exclude '**/.nuget/**/*' \
    --exclude '**/obj/**/*' \
    --exclude '**/bin/**/*'

lint:
  prettier --check \
    --ignore-path "{{gitignore}}" \
    --ignore-path "{{prettierignore}}" \
    --cache --cache-strategy metadata \
    "{{root}}"

  dotnet build "{{sln}}"

  dotnet jb inspectcode "{{sln}}" \
    --no-build \
    --verbosity=ERROR \
    --caches-home="{{jbcache}}" \
    -o="{{jbinspectlog}}" \
    --exclude='**/.git/**/*;**/.nuget/**/*;**/obj/**/*;**/bin/**/*'

  dotnet format "{{sln}}" \
    --verify-no-changes \
    --no-restore \
    --verbosity minimal \
    --severity info \
    --exclude '**/.git/**/*' \
    --exclude '**/.nuget/**/*' \
    --exclude '**/obj/**/*' \
    --exclude '**/bin/**/*'

test *args:
  dotnet test "{{sln}}" {{args}}

migrate name:
  dotnet ef \
    --startup-project "{{servercsproj}}" \
    --project "{{datacsproj}}" \
    migrations add \
    --output-dir Migrations \
    --namespace Ozds.Data.Migrations \
    "{{name}}"

publish *args:
  dotnet publish "{{sln}}" \
    --property PublishDir="{{artifacts}}" \
    --property ConsoleLoggerParameters=ErrorsOnly \
    --property IsWebConfigTransformDisabled=true \
    --property DebugType=None \
    --property DebugSymbols=false \
    --configuration Release \
    {{args}}

[confirm("This will clean app data and docker containers. Do you want to continue?")]
clean:
  rm -rf "{{appdata}}"

  docker compose down -v
  docker compose up -d

[confirm("This will clean app data, docker containers and dotnet artifacts. Do you want to continue?")]
purge:
  rm -rf "{{appdata}}"

  git clean -Xdf \
    -e !.vscode/ \
    -e !.vscode/** \
    -e !.vs/ \
    -e !.vs/** \
    -e !.idea/ \
    -e !.idea/** \
    -e !**/*.csproj.user \
    -e !App_Data/ \
    -e !.direnv/ \
    -e !.direnv/bin/
  dotnet tool restore
  dotnet restore

  docker compose down -v
  docker compose up -d
