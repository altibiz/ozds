{ self, pkgs, rumor, ... }:

{
  seal.defaults.devShell = "dev";
  integrate.devShell.devShell =
    pkgs.mkShell ({
      PGHOST = "localhost";
      PGPORT = "5432";
      PGDATABASE = "ozds";
      PGUSER = "ozds";
      PGPASSWORD = "ozds";

      DOXYGEN_DOT_PATH = "${pkgs.graphviz}/bin/dot";
      DOXYGEN_PLANTUML_JAR_PATH = "${pkgs.plantuml}/lib/plantuml.jar";

      COMPOSE_PROFILES = "*";

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
          nix-bundle
          fd
          rumor.packages.${pkgs.system}.default
          nixos-generators

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
        ] ++ builtins.attrValues (self.lib.poetry.pkgs pkgs);
    } // self.lib.playwright.env pkgs);
}
