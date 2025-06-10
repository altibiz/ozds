{ self, pkgs, ... }:

{
  integrate.devShell.devShell =
    pkgs.mkShell ({
      NIX_PATH = "nixpkgs=${pkgs.path}";

      packages = with pkgs; [
        # Scripts
        just
        nushell
        self.packages.${pkgs.system}.bundle
        git
      ] ++ (self.lib.dotnet.pkgs pkgs);
    } // (self.lib.dotnet.env pkgs));
}
