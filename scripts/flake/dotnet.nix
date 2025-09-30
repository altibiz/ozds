{ ... }:

let
  csharp-ls = pkgs: pkgs.csharp-ls.overrideAttrs (
    final: prev:
      let
        sdk =
          with pkgs.dotnetCorePackages;
          combinePackages [
            sdk_8_0
            sdk_9_0
          ];
      in
      {
        buildInputs = (prev.buildInputs or [ ]) ++ [
          sdk
        ];

        nativeBuildInputs = (prev.nativeBuildInputs or [ ]) ++ [
          pkgs.makeWrapper
        ];

        postFixup = (prev.postFixup or "") + ''
          wrapProgram $out/bin/csharp-ls \
            --set DOTNET_ROOT ${sdk} \
            --prefix PATH : ${sdk}/bin
        '';
      }
  );

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
    pkgs.csharpier
    (csharp-ls pkgs)
    (sdk pkgs)
  ];

  flake.lib.dotnet.env = pkgs: {
    DOTNET_ROOT = "${sdk pkgs}/share/dotnet";
  };
}
