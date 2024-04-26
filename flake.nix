{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-23.11";
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

            # Misc
            nodePackages.prettier
            nodePackages.cspell
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

              # PostgreSQL
              usql
              postgresql_14
              mermerd

              # Scripts
              just
              nushell

              # Documentation
              doxygen
              simple-http-server

              # Misc
              nodePackages.prettier
              nodePackages.yaml-language-server
              nodePackages.vscode-langservers-extracted
              taplo
              marksman
              nodePackages.cspell
            ];
        };
      });
}
