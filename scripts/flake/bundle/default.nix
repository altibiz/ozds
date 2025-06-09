{ pkgs, ... }:

{
  integrate.package.package = pkgs.nix-bundle.overrideAttrs (final: prev: {
    patches = (prev.patches or [ ]) ++ [
      ./pin-nixpkgs.patch
    ];
  });
}
