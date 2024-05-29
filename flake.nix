{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
    nixpkgs-stable.url = "github:NixOS/nixpkgs/release-23.05";

    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
          config = { allowUnfree = true; };
          overlays = [
            (final: prev: {
              nodejs = prev.nodejs_20;
              dotnet-sdk = prev.dotnet-sdk_8;
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
          DOXYGEN_DOT_PATH = "${pkgs.graphviz}/bin/dot";
          DOXYGEN_PLANTUML_JAR_PATH = "${pkgs.plantuml}/lib/plantuml.jar";

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

            # Spelling
            nodePackages.cspell
            hunspell
            hunspellDicts.hr-hr
            hunspellDicts.en-us-large

            # Misc
            nodePackages.prettier
          ];
        };
        devShells.default = pkgs.mkShell {
          PGHOST = "localhost";
          PGPORT = "5432";
          PGDATABASE = "ozds";
          PGUSER = "ozds";
          PGPASSWORD = "ozds";

          DOXYGEN_DOT_PATH = "${pkgs.graphviz}/bin/dot";
          DOXYGEN_PLANTUML_JAR_PATH = "${pkgs.plantuml}/lib/plantuml.jar";

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
              marksman
            ];
        };
      });
}
