{
  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-24.11";

    poetry2nix.url = "github:nix-community/poetry2nix";

    perch.url = "github:altibiz/perch/refs/tags/2.1.1";
    perch.inputs.nixpkgs.follows = "nixpkgs";

    sops-nix.url = "github:Mic92/sops-nix";
    sops-nix.inputs.nixpkgs.follows = "nixpkgs";

    nixos-hardware.url = "github:NixOS/nixos-hardware/master";

    deploy-rs.url = "github:serokell/deploy-rs";
    deploy-rs.inputs.nixpkgs.follows = "nixpkgs";

    rumor.url = "github:altibiz/rumor/refs/tags/1.1.3";
    rumor.inputs.nixpkgs.follows = "nixpkgs";
    rumor.inputs.perch.follows = "perch";
  };

  outputs = { self, perch, ... } @inputs:
    perch.lib.flake.make {
      inherit inputs;
      root = ./.;
      prefix = "scripts/flake";
    };

  nixConfig = {
    extra-substituters = [
      "s3://nix-binary-cache?endpoint=s3.lvm.altibiz.com"
    ];
    extra-trusted-public-keys = [
      "s3.lvm.altibiz.com:2joxncr8RIOfSZcVvt79MvvX3IA4ulUjdc2mkKUR1xc="
    ];
  };
}
