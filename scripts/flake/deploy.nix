{ deploy-rs, lib, config, ... }:

let
  deployNodeSubmodule = {
    options.hostName = lib.mkOption {
      type = lib.types.str;
      description = lib.literalMD ''
        Deployment host name.
      '';
    };
    options.sshUser = lib.mkOption {
      type = lib.types.str;
      description = lib.literalMD ''
        Deployment SSH user.
      '';
    };
  };
in
{
  options.seal.deploy.nodes = lib.mkOption {
    type =
      lib.types.attrsOf
        (lib.types.submodule deployNodeSubmodule);
    default = { };
    description = lib.literalMD ''
      Extra data needed to create `deploy.nodes` flake output.
    '';
  };

  options.seal.defaults.nixosConfigurationsAsDeployNodes = lib.mkOption {
    type = lib.types.bool;
    default = true;
    description = lib.literalMD ''
      Convert all packages to apps.
    '';
  };

  options.propagate.deploy = lib.mkOption {
    type = lib.types.attrsOf
      (lib.types.attrsOf
        lib.types.raw);
    default = { };
    description = lib.literalMD ''
      Propagated `deploy.nodes` flake output.
    '';
  };

  config.propagate.deploy.nodes =
    let
      systems = builtins.attrNames deploy-rs.lib;
    in
    builtins.listToAttrs
      (lib.flatten
        (builtins.map
          ({ configuration, submodule }:
            let
              configurations =
                builtins.filter
                  ({ value, ... }: value != null)
                  (builtins.map
                    (system:
                      let
                        name = "${configuration}-${system}";
                      in
                      {
                        inherit configuration system name;
                        value =
                          if config.flake.nixosConfigurations ? name
                          then config.flake.nixosConfigurations.${name}
                          else null;
                      })
                    systems);
            in
            builtins.map
              ({ name, system, value, ... }: {
                inherit name;
                value = submodule // {
                  profiles.system = {
                    path = deploy-rs.lib.${system}.activate.nixos value;
                  };
                };
              })
              configurations)
          (lib.mapAttrsToList
            (name: value: {
              configuration = name;
              submodules = value;
            })
            config.seal.deploy.nodes)));
}
