{ pkgs, ... }:

{
  integrate.devShell.devShell =
    pkgs.mkShell {
      NIX_PATH = "nixpkgs=${pkgs.path}";

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
}
