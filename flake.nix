{
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.11";

    poetry2nix.url = "github:nix-community/poetry2nix";

    perch.url = "github:altibiz/perch/flake";
    perch.inputs.nixpkgs.follows = "nixpkgs";
  };

  outputs = { self, perch, ... } @inputs:
    perch.lib.flake.make {
      inherit inputs;
      root = ./.;
      prefix = "scripts/flake";
    };
}
