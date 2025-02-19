{ self, poetry2nix, ... }:

{
  flake.lib.poetry.pkgs = pkgs:
    let
      integratedPoetry2nix =
        poetry2nix.lib.mkPoetry2Nix { inherit pkgs; };

      mkEnvWrapper = env: name: pkgs.writeShellApplication {
        name = name;
        runtimeInputs = [ env ];
        text = ''
          export PYTHONPREFIX=${env}
          export PYTHONEXECUTABLE=${env}/bin/python

          # shellcheck disable=SC2125
          export PYTHONPATH=${env}/lib/**/site-packages

          ${name} "$@"
        '';
      };

      pythonEnv = integratedPoetry2nix.mkPoetryEnv {
        projectDir = self;
        preferWheels = true;
        checkGroups = [ ];
        overrides = integratedPoetry2nix.defaultPoetryOverrides.extend (final: prev: {
          pyright = prev.pyright.overridePythonAttrs (old: {
            postInstall = (old.postInstall or "") + ''
              wrapProgram $out/bin/pyright \
                --prefix PATH : ${pkgs.lib.makeBinPath [ pkgs.nodejs ]}
              wrapProgram $out/bin/pyright-langserver \
                --prefix PATH : ${pkgs.lib.makeBinPath [ pkgs.nodejs ]}
            '';
          });
        });
      };
    in
    {
      poetry = pkgs.poetry;
      env = pythonEnv;
      pyright = mkEnvWrapper pythonEnv "pyright";
      pyright-langserver = mkEnvWrapper pythonEnv "pyright-langserver";
    };
}
