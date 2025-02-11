rec {
  description = "OZDS";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.11";

    flake-utils.url = "github:numtide/flake-utils";

    poetry2nix.url = "github:nix-community/poetry2nix";
  };

  outputs = { self, nixpkgs, flake-utils, ... } @rawInputs:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
          config = { allowUnfree = true; };
          overlays = [
            (final: prev: {
              nodejs = prev.nodejs_20;
              dotnet-sdk = prev.dotnet-sdk_8;
              dotnet-runtime = prev.dotnet-runtime_8;
              dotnet-aspnetcore = prev.dotnet-aspnetcore_8;
            })
          ];
        };

        mkEnvWrapper = env: name: pkgs.writeShellApplication {
          name = name;
          runtimeInputs = [ env ];
          text = ''
            export PYTHONPREFIX=${env}
            export PYTHONEXECUTABLE=${env}/bin/python

            # shellcheck disable=SC2125
            export PYTHONPATH=${env}/lib/**/site-packages

            ${name} "$@"
          '';
        };

        poetry2nix = rawInputs.poetry2nix.lib.mkPoetry2Nix { inherit pkgs; };

        pythonEnv = poetry2nix.mkPoetryEnv {
          projectDir = self;
          preferWheels = true;
          checkGroups = [ ];
          overrides = poetry2nix.defaultPoetryOverrides.extend (final: prev: {
            pyright = prev.pyright.overridePythonAttrs (old: {
              postInstall = (old.postInstall or "") + ''
                wrapProgram $out/bin/pyright \
                  --prefix PATH : ${pkgs.lib.makeBinPath [ pkgs.nodejs ]}
                wrapProgram $out/bin/pyright-langserver \
                  --prefix PATH : ${pkgs.lib.makeBinPath [ pkgs.nodejs ]}
              '';
            });
          });
        };

        playwright_browsers = pkgs.playwright-driver.browsers.override {
          withFirefox = false;
          withWebkit = false;
        };
      in
      {
        devShells.deploy = pkgs.mkShell {
          packages = with pkgs; [
            # Scripts
            just
            nushell
            nix-bundle

            # C#
            dotnet-sdk
            dotnet-runtime
            dotnet-aspnetcore
          ];
        };
        devShells.docs = pkgs.mkShell {
          packages = with pkgs; [
            # Scripts
            just
            nushell

            # C#
            dotnet-sdk
            dotnet-runtime
            dotnet-aspnetcore

            # Documentation
            mdbook
            openjdk
            plantuml
            graphviz
            mdbook-plantuml
            pandoc-plantuml-filter
          ];
        };
        devShells.check = pkgs.mkShell {
          PLAYWRIGHT_NODEJS_PATH = "${pkgs.nodejs}/bin/node";
          PLAYWRIGHT_BROWSERS_PATH = "${playwright_browsers}";

          packages = with pkgs; [
            # Scripts
            just
            nushell

            # Nix
            nixpkgs-fmt

            # C#
            dotnet-sdk
            dotnet-runtime
            dotnet-aspnetcore

            # Python
            poetry
            pythonEnv
            (mkEnvWrapper pythonEnv "pyright")
            (mkEnvWrapper pythonEnv "pyright-langserver")

            # Markdown
            markdownlint-cli
            nodePackages.markdown-link-check

            # Spelling
            nodePackages.cspell

            # Misc
            nodePackages.prettier
          ];
        };
        devShells.default = pkgs.mkShell rec {
          PGHOST = "localhost";
          PGPORT = "5432";
          PGDATABASE = "ozds";
          PGUSER = "ozds";
          PGPASSWORD = "ozds";

          DOXYGEN_DOT_PATH = "${pkgs.graphviz}/bin/dot";
          DOXYGEN_PLANTUML_JAR_PATH = "${pkgs.plantuml}/lib/plantuml.jar";

          COMPOSE_PROFILES = "*";

          PLAYWRIGHT_NODEJS_PATH = "${pkgs.nodejs}/bin/node";
          PLAYWRIGHT_BROWSERS_PATH = "${playwright_browsers}";

          packages =
            let
              usql = pkgs.writeShellApplication {
                name = "usql";
                runtimeInputs = [ pkgs.usql ];
                text = ''
                  usql \
                    pg://ozds:ozds@localhost/ozds?sslmode=disable \
                    "$@"
                '';
              };

              sqls = pkgs.writeShellApplication {
                name = "sqls";
                runtimeInputs = [ pkgs.sqls pkgs.git ];
                text = ''
                  cat | sqls -config "$(git rev-parse --show-toplevel)/.sqls.yaml" "$@"
                '';
              };

              sql-formatter = pkgs.writeShellApplication {
                name = "sql-formatter";
                runtimeInputs = [ pkgs.sql-formatter ];
                text = ''
                  cat | sql-formatter --config "$(git rev-parse --show-toplevel)/.sql-formatter.json" "$@"
                '';
              };

              mermerd = pkgs.writeShellApplication {
                name = "mermerd";
                runtimeInputs = [ pkgs.mermerd ];
                text = ''
                  mermerd \
                    --connectionString postgresql://ozds:ozds@localhost:5432/ozds \
                    "$@"
                '';
              };

              nushell = pkgs.writeShellApplication {
                name = "nu";
                runtimeInputs = [ pkgs.nushell ];
                text = ''
                  nu \
                    --plugins "[ ${pkgs.nushellPlugins.polars}/bin/nu_plugin_polars ]" \
                    "$@"
                '';
              };
            in
            with pkgs;
            [
              # Version Control
              git
              dvc-with-remotes
              delta

              # Nix
              nil
              nixpkgs-fmt

              # C#
              dotnet-sdk
              dotnet-runtime
              dotnet-aspnetcore
              omnisharp-roslyn
              netcoredbg
              powershell

              # Python
              poetry
              pythonEnv
              (mkEnvWrapper pythonEnv "pyright")
              (mkEnvWrapper pythonEnv "pyright-langserver")

              # Markdown
              marksman
              markdownlint-cli
              nodePackages.markdown-link-check

              # PostgreSQL
              usql
              postgresql_14
              mermerd
              sql-formatter
              sqls

              # MailHog
              apacheHttpd

              # Spelling
              nodePackages.cspell

              # Scripts
              just
              nushell

              # Documentation
              simple-http-server
              pandoc
              mdbook
              openjdk
              plantuml
              graphviz
              mdbook-plantuml
              pandoc-plantuml-filter

              # Misc
              nodePackages.prettier
              nodePackages.yaml-language-server
              nodePackages.vscode-langservers-extracted
              taplo
            ];
        };

        packages.default =
          pkgs.buildDotnetModule
            rec {
              pname = "ozds";
              version = "0.1.0";

              src = self;
              projectFile = "src/Ozds.Server/Ozds.Server.csproj";
              nugetDeps = ./deps.nix;
              executables = [ "Ozds.Server" ];
              makeWrapperArgs = [
                "--set DOTNET_CONTENTROOT ${placeholder "out"}/lib/${pname}"
              ];

              dotnet-sdk = pkgs.dotnet-sdk;
              dotnet-runtime = pkgs.dotnet-aspnetcore;

              meta = {
                description = description;
                homepage = "https://github.com/altibiz/ozds";
                license = pkgs.lib.licenses.mit;
              };
            };

        packages.docker = pkgs.dockerTools.buildImage
          {
            name = "altibiz/ozds";
            tag = "latest";
            created = "now";
            copyToRoot = pkgs.buildEnv {
              name = "image-root";
              paths = [ self.packages.${system}.default ];
              pathsToLink = [ "/bin" ];
            };
            config = {
              Cmd = [ "ozds" ];
            };
          };
      });
}
