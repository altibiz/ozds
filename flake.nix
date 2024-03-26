{
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-23.11";
    utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, utils }:
    utils.lib.simpleFlake {
      inherit self nixpkgs;
      name = "altibiz-ozds";
      config = {
        allowUnfree = true;
      };
      overlay = (final: prev: {
        nodejs = prev.nodejs_20;
        dotnet-sdk = prev.dotnet-sdk_8;
      });
      shell = { pkgs }:
        pkgs.mkShell {
          packages = with pkgs; let
            usql = pkgs.writeShellApplication {
              name = "usql";
              runtimeInputs = [ pkgs.usql ];
              text = ''
                usql pg://ozds:ozds@localhost/ozds?sslmode=disable "$@"
              '';
            };
          in
          [
            # Nix
            nil
            nixpkgs-fmt

            # C#
            dotnet-sdk
            dotnet-runtime
            dotnet-aspnetcore
            omnisharp-roslyn
            netcoredbg

            # Misc
            usql
            just
            nodePackages.prettier
            # nodePackages.remark
            nodePackages.yaml-language-server
            nodePackages.vscode-langservers-extracted
            marksman
            taplo
          ];
        };
    };
}
