{ self, pkgs, ... }:

{
  integrate.devShell.devShell =
    pkgs.mkShell
      ({
        PGHOST = "localhost";
        PGPORT = "5432";
        PGDATABASE = "ozds";
        PGUSER = "ozds";
        PGPASSWORD = "ozds";

        packages = with pkgs; [
          # Version Control
          git
          # FIXME: permission denied /var/cache/dvc
          # dvc-with-remotes

          # Scripts
          just
          nushell
          fd

          # Nix
          nixpkgs-fmt

          # C#
          dotnet-sdk
          dotnet-runtime
          dotnet-aspnetcore

          # PostgreSQL
          postgresql_14

          # Markdown
          markdownlint-cli
          nodePackages.markdown-link-check

          # Spelling
          nodePackages.cspell

          # Misc
          nodePackages.prettier
        ] ++ builtins.attrValues (self.lib.poetry.pkgs pkgs);
      } // self.lib.playwright.env pkgs.system);
}
