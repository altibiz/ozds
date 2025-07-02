{ ... }:

let
  sdk = pkgs: pkgs.dotnetCorePackages.combinePackages
    (with pkgs.dotnetCorePackages; [
      # vscode extension
      sdk_9_0_3xx
      # latest LTS
      sdk_8_0_3xx
    ]);
in
{
  flake.lib.dotnet.pkgs = pkgs: [
    (sdk pkgs)
  ];

  flake.lib.dotnet.env = pkgs: {
    DOTNET_ROOT = "${sdk pkgs}/share/dotnet";
  };
}
