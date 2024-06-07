rec {
  description = "OZDS";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
    nixpkgs-stable.url = "github:NixOS/nixpkgs/release-23.05";

    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils, ... }:
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
      in
      {
        devShells.deploy = pkgs.mkShell {
          packages = with pkgs; [
            # Scripts
            just
            nushell

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

            # Markdown
            markdownlint-cli
            nodePackages.markdown-link-check

            # Spelling
            nodePackages.cspell
            hunspell
            hunspellDicts.hr-hr
            hunspellDicts.en-us-large

            # Misc
            nodePackages.prettier
            zip
            unzip
          ];
        };
        devShells.default = pkgs.mkShell {
          PGHOST = "localhost";
          PGPORT = "5432";
          PGDATABASE = "ozds";
          PGUSER = "ozds";
          PGPASSWORD = "ozds";

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

              mermerd = pkgs.writeShellApplication {
                name = "mermerd";
                runtimeInputs = [ pkgs.mermerd ];
                text = ''
                  mermerd \
                    --connectionString postgresql://ozds:ozds@localhost:5432/ozds \
                    "$@"
                '';
              };
            in
            with pkgs;
            [
              # Version Control
              git
              dvc-with-remotes

              # Nix
              nil
              nixpkgs-fmt

              # C#
              dotnet-sdk
              dotnet-runtime
              dotnet-aspnetcore
              omnisharp-roslyn
              netcoredbg

              # Markdown
              marksman
              markdownlint-cli
              nodePackages.markdown-link-check

              # PostgreSQL
              usql
              postgresql_14
              mermerd

              # Spelling
              nodePackages.cspell
              hunspell
              hunspellDicts.hr-hr
              hunspellDicts.en-us-large

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
              zip
              unzip
            ];
        };

        packages.default = pkgs.writeShellApplication {
          name = "ozds";
          runtimeInputs = [
            (pkgs.buildDotnetModule rec {
              pname = "ozds";
              version = "0.1.0";

              src = self;
              projectFile = "src/Ozds.Server/Ozds.Server.csproj";
              nugetDeps = ./deps.nix;
              executables = [ "Ozds.Server" ];
              makeWrapperArgs = [
                "--set DOTNET_CONTENTROOT ${placeholder "out"}/lib/${pname}"
              ];

              postPatch = ''
                rm -rf src/Ozds.Server/App_Data
              '';

              dotnet-sdk = pkgs.dotnet-sdk;
              dotnet-runtime = pkgs.dotnet-aspnetcore;

              meta = {
                description = description;
                homepage = "https://github.com/altibiz/ozds";
                license = pkgs.lib.licenses.mit;
              };
            })
          ];
          text = ''
            Ozds.Server "$@"
          '';
        };

        packages.default-docker = pkgs.dockerTools.buildImage {
          name = "altibiz/ozds";
          tag = "latest";
          created = "now";
          fromImage = (pkgs.dockerTools.buildImage {
            name = "altibiz/gns3-base";
            tag = "latest";
            created = "now";
            copyToRoot = with pkgs.dockerTools; [
              usrBinEnv
              binSh
              caCertificates
              fakeNss
            ];
            runAsRoot = ''
              mkdir -p /var/run
            '';
          });
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
