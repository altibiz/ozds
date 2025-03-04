{ self, pkgs, ... }:

{
  integrate.devShell.devShell =
    pkgs.mkShell
      ({
        packages = with pkgs; [
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

          # Markdown
          markdownlint-cli
          nodePackages.markdown-link-check

          # Spelling
          nodePackages.cspell

          # Misc
          nodePackages.prettier
        ] ++ builtins.attrValues (self.lib.poetry.pkgs pkgs);
      } // self.lib.playwright.env pkgs);
}
