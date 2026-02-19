{ self, pkgs, rumor, ... }:

{
  seal.defaults.devShell = "dev";
  integrate.devShell = {
    nixpkgs.config = {
      allowUnfree = true;
    };

    devShell =
      pkgs.mkShell ({
        PGHOST = "localhost";
        PGPORT = "5432";
        PGDATABASE = "ozds";
        PGUSER = "ozds";
        PGPASSWORD = "ozds";

        LDAP_URI = "ldap://localhost:3890";
        LDAP_BASE = "dc=altibiz,dc=com";
        LDAP_BINDDN = "uid=admin,ou=people,dc=altibiz,dc=com";

        DOXYGEN_DOT_PATH = "${pkgs.graphviz}/bin/dot";
        DOXYGEN_PLANTUML_JAR_PATH = "${pkgs.plantuml}/lib/plantuml.jar";

        COMPOSE_PROFILES = "*";

        packages =
          let
            usqll = pkgs.writeShellApplication {
              name = "usqll";
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
          ] ++ (self.lib.dotnet.pkgs pkgs) ++ [
            omnisharp-roslyn
            rzls
            netcoredbg
            powershell

            # Markdown
            marksman
            markdownlint-cli
            nodePackages.markdown-link-check

            # PostgreSQL
            usqll
            postgresql_14
            mermerd
            sql-formatter
            sqls

            # Mailpit
            apacheHttpd

            # Spelling
            nodePackages.cspell

            # Scripts
            ollama
            s3cmd
            just
            nushell
            self.packages.${pkgs.system}.bundle
            fd
            ripgrep
            rumor.packages.${pkgs.system}.default
            vault
            nixos-generators
            nebula
            openssh
            sshpass
            deploy-rs
            usql
            openldap
          ] ++ lib.optionals
            (
              pkgs.hostPlatform.isLinux
                && pkgs.hostPlatform.isx86_64
            ) [
            libguestfs-with-appliance
          ] ++ [

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
      }
      // (self.lib.playwright.env pkgs.system)
      // (self.lib.dotnet.env pkgs));
  };
}
